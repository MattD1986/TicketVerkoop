using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketVerkoop.Domain.Entities;
using TicketVerkoop.Extensions;
using TicketVerkoop.Service.Interfaces;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers
{
    public class PloegenController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IService<Club> _clubs;
        private IService<Vak> _vakken;
        private IService<Competitie> _competities;

        public PloegenController(ILogger<HomeController> logger, IService<Club> clubService, IService<Vak> vakService, IService<Competitie> competitieService)
        {
            _logger = logger;
            _clubs = clubService;
            _vakken = vakService;
            _competities = competitieService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var competitie = await _competities.FindById(Convert.ToInt32(1));
            PloegVM vm = new()
            {
                clubId = id,
                competitie = competitie
            };

            return View(vm);
        }



        public async Task<IActionResult> ChooseAbo(int? id)
        {

            Club club = await _clubs.FindById(Convert.ToInt32(id));
            var competitie = await _competities.FindById(Convert.ToInt32(1));

            var vakken = await _vakken.GetAll();
            vakken = vakken.Where(v => v.StadionId == club.StadionId).ToList();

            PloegVM vm = new PloegVM
            {
                clubId = club.ClubId,
                competitie = competitie,
                vakkenList = new SelectList(vakken,"Id", "VakOmschrijving.Beschrijving"),
                clubNaam = club.Naam
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> ChooseAbo(PloegVM ploegVM, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int vakId = ploegVM.vakId;
            Vak vak = await _vakken.FindById(vakId);
            Club club = await _clubs.FindById(Convert.ToInt32(id));
            var competitie = await _competities.FindById(Convert.ToInt32(1));


            CartVM item = new CartVM
            {
                Id = club.ClubId,
                ClubNr = club.ClubId,
                NaamThuisPloeg = club.Naam,
                Aantal = 1,
                Prijs = (float)vak.Prijs * 8,
                vakId = vakId,
                AanmaakDatum = DateTime.Now,
                CompetitieId = competitie.Id
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
    }
}
