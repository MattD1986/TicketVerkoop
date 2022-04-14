using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using TicketVerkoop.Domain.Entities;
using TicketVerkoop.Extensions;
using TicketVerkoop.Service.Interfaces;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers
{
    public class WedstrijdenController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IService<Wedstrijd> _wedstrijden;
        private IService<Club> _clubs;
        private IService<Vak> _vakken;
        private IService<Order> _orders;
        private IService<Ticket> _tickets;
        private IService<Abonnement> _abonnementen;
        private IService<Plaat> _plaatsen;




        public WedstrijdenController(ILogger<HomeController> logger, IService<Wedstrijd> wedstrijdService, IService<Club> clubService, 
                                        IService<Vak> vakkenService, IService<Order> orderService, IService<Ticket> ticketService, IService<Abonnement> abonnementService, IService<Plaat> plaatsService)
        {
            _logger = logger;
            _wedstrijden = wedstrijdService;
            _clubs = clubService;
            _vakken = vakkenService;
            _orders = orderService;
            _tickets = ticketService;
            _abonnementen = abonnementService;
            _plaatsen = plaatsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var wedstrijdLijst = await _wedstrijden.GetAll();
            wedstrijdLijst = wedstrijdLijst.Where(w => w.Datum.Date >= DateTime.Now.Date);

            WedstrijdDataVM vm = new WedstrijdDataVM
            {
                clubList = new SelectList(await _clubs.GetAll(), "ClubId", "Naam"),
                wedstrijden = wedstrijdLijst

            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(WedstrijdDataVM WedstrijdDataVM)
        {
            int choosenClub = WedstrijdDataVM.clubId;
            int choosenStadion = WedstrijdDataVM.stadionId;
          
            var wedstrijden = await _wedstrijden.GetAll();
            var wedstrijdenVanClub = wedstrijden.Where(w => (w.ThuisPloegNavigation.ClubId == choosenClub || w.UitPloegNavigation.ClubId == choosenClub || w.ThuisPloegNavigation.StadionId == choosenStadion)
                                                            && w.Datum.Date >= DateTime.Now.Date).ToList();

            WedstrijdDataVM vm = new WedstrijdDataVM
            {
                clubList = new SelectList(await _clubs.GetAll(), "ClubId", "Naam"),
                wedstrijden = wedstrijdenVanClub
            };
            return View(vm);
        }

        public async Task<IActionResult> ChooseTickets(int? id)
        {
            Wedstrijd w = await _wedstrijden.FindById(Convert.ToInt32(id));

            var stadion = w.ThuisPloegNavigation.StadionId;

            var vakken = await _vakken.GetAll();
            vakken = vakken.Where(v => v.StadionId == stadion);


            WedstrijdDataVM vm = new WedstrijdDataVM
            {
                datum = w.Datum,
                uur = w.Tijd,
                WestrijdId = (int)id,
                thuisploeg = w.ThuisPloegNavigation.Naam,
                uitploeg = w.UitPloegNavigation.Naam,
                stadion = w.ThuisPloegNavigation.Stadion.Naam,
                aantal = 1,
                stadionId = w.ThuisPloegNavigation.StadionId,
                vakkenList = new SelectList(vakken, "Id", "VakOmschrijving.Beschrijving"),
            };

            return View(vm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> ChooseTickets(WedstrijdDataVM vm, int? id)   //id = wedstrijdId gekozen wedstrijd
        {
            if (id == null || vm == null)
            {
                return NotFound();
            }

            //int vakId = vm.vakId;
            Vak vak = await _vakken.FindById(vm.vakId);
            Wedstrijd wedstrijd = await _wedstrijden.FindById(Convert.ToInt32(id));
            string? userID = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var aangekochteTickets = await _tickets.GetAll();

            var aangekochteTicketsLijst = aangekochteTickets.Where(t => t.Order.ClientId == userID && t.Wedstrijd.Datum == wedstrijd.Datum && t.WedstrijdId != id).ToList();
            var ticketsVoorWedstrijd = aangekochteTickets.Where(t => t.Order.ClientId == userID && t.WedstrijdId == id).ToList();

            if (aangekochteTicketsLijst.Count() == 0 && ticketsVoorWedstrijd.Count() < 4) {

                var lijstPlaatsen = await _plaatsen.GetAll();
                lijstPlaatsen = lijstPlaatsen.Where(p => p.VakId == vm.vakId && p.WedstrijdId == id).ToList();

                if (lijstPlaatsen.Count() < vak.Aantal)
                {
                    CartVM item = new CartVM
                    {
                        Id = wedstrijd.Id,
                        WedstrijdNr = wedstrijd.Id,
                        wedstrijdDatum = wedstrijd.Datum, 
                        Aantal = 1,
                        vakNr = 0,
                        Prijs = (float)vak.Prijs,
                        AanmaakDatum = DateTime.Now,
                        NaamThuisPloeg = wedstrijd.ThuisPloegNavigation.Naam,
                        NaamUitPloeg = wedstrijd.UitPloegNavigation.Naam,
                        Stadion = wedstrijd.ThuisPloegNavigation.Stadion.Naam,
                        vakId = vm.vakId,
                    };

                    ShoppingCartVM? shopping;

                    if (HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart") != null)
                    {
                        shopping = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
                    }
                    else
                    {
                        shopping = new ShoppingCartVM();
                        shopping.Cart = new List<CartVM>();
                    }
                    shopping.Cart.Add(item);

                    HttpContext.Session.SetObject("ShoppingCart", shopping);

                    return RedirectToAction("Index", "ShoppingCart");
                }
                else
                {
                    TempData["fout"] = "Er zijn geen tickets meer beschikbaar in het gekozen vak";
                    return RedirectToAction("ChooseTickets");
                }
            }
            else
            {
                if (ticketsVoorWedstrijd.Count() >= 4)
                {
                    TempData["fout"] = "U bezit reeds 4 tickets voor deze wedstrijd";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["fout"] = "U bezit reeds tickets voor een wedstrijd op deze dag";
                    return RedirectToAction("Index");
                }
            }

        }

    }
}
