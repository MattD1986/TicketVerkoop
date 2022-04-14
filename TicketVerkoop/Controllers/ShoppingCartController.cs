using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Project_FullStack.Areas.Identity.Data;
using System.Security.Claims;
using TicketVerkoop.Domain.Entities;
using TicketVerkoop.Extensions;
using TicketVerkoop.Service.Interfaces;
using TicketVerkoop.Util.Mail;
using TicketVerkoop.ViewModels;
using static TicketVerkoop.Util.Mail.EmailSender;

namespace TicketVerkoop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IService<Order> _orders;
        private IService<Ticket> _tickets;
        private IService<Abonnement> _abonnementen;
        private IService<Plaat> _plaatsen;
        private IService<Wedstrijd> _wedstrijden;
        private IService<Vak> _vakken;
        private IService<VakOmschrijving> _vakOmschrijving;
        private IService<Club> _clubs;
        private readonly IEmailSend _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;


        public ShoppingCartController(ILogger<HomeController> logger, IService<Order> orderService, IService<Ticket> ticketService, IService<Club> clubService,
                                        IService<Abonnement> abonnementService, IService<Plaat> plaatsService, IService<Wedstrijd> wedstrijdService, 
                                        IService<Vak> vakkenService, IService<VakOmschrijving> vakOmschrijvingService, IEmailSend emailSend, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _orders = orderService;
            _tickets = ticketService;
            _abonnementen = abonnementService;
            _plaatsen = plaatsService;
            _wedstrijden = wedstrijdService;
            _vakken = vakkenService;
            _vakOmschrijving = vakOmschrijvingService;
            _clubs = clubService;
            _emailSender = emailSend;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            ShoppingCartVM? cartList = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");


            float totaal = 0;
            if (cartList != null) { 
                foreach (CartVM cartItem in cartList.Cart)
                {
                    totaal += cartItem.Prijs * cartItem.Aantal;
                }
                cartList.totaalPrijs = totaal;
            }
            
            return View(cartList);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Payment()
        {
            ShoppingCartVM? cartList
              = HttpContext.Session
              .GetObject<ShoppingCartVM>("ShoppingCart");
            //  opvragen UserID
            string? userID = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userID);

            try
            {
                Order order = new Order();

                order.ClientId = userID;
                order.AankoopDatum = DateTime.Now;
                await _orders.Add(order);

                foreach (CartVM? cart in cartList.Cart)
                {
                    var lijstPlaatsen = new List<Plaat>();
                    Vak vak = await _vakken.FindById(cart.vakId);

                    var lijstWedstrijdenVanClub = await _wedstrijden.GetAll();
                    lijstWedstrijdenVanClub = lijstWedstrijdenVanClub.Where(w => w.ThuisPloegNavigation.ClubId == cart.ClubNr).ToList();

                    foreach (Plaat p in await _plaatsen.GetAll())
                    {
                        if (p.VakId == cart.vakId && p.WedstrijdId == cart.WedstrijdNr)
                        {
                            lijstPlaatsen.Add(p);
                        }

                        foreach (Wedstrijd wedstrijd in lijstWedstrijdenVanClub)
                        {
                            if(p.VakId == cart.vakId && p.WedstrijdId == wedstrijd.Id)
                            {
                                lijstPlaatsen.Add(p);
                            }
                        }
                    }

                    if (cart.WedstrijdNr != 0)
                    {
                        //process tickets
                    int aantalVerwerkt = 0;
                            while (aantalVerwerkt < cart.Aantal)
                            {
                                Ticket ticket = new Ticket();
                                ticket.WedstrijdId = cart.WedstrijdNr;
                                ticket.AankoopDatum = DateTime.Now;
                                ticket.OrderId = order.Id;
                                ticket.Prijs = cart.Prijs;
                                ticket.Wedstrijd = await _wedstrijden.FindById(ticket.WedstrijdId);
                               
                                Plaat plaats = new Plaat();
                                plaats.VakId = cart.vakId;
                                plaats.Vak = await _vakken.FindById(plaats.VakId);
                                plaats.WedstrijdId = cart.WedstrijdNr;
                                plaats.Stoelnr = GetVrijeStoel(lijstPlaatsen, cart.WedstrijdNr, cart.vakId);

                                await _plaatsen.Add(plaats);
                                ticket.PlaatsId = plaats.Id;
                                ticket.Plaats = await _plaatsen.FindById(ticket.PlaatsId);
                                await _tickets.Add(ticket);
                                lijstPlaatsen.Add(plaats);      //nodig om ervoor te zorgen dat ie de volgende keer weet dat er een nieuwe plaats bij is

                                aantalVerwerkt++;

                            var vakomschrijving = await _vakOmschrijving.FindById(plaats.Vak.VakOmschrijvingId);
                            string email = user.Email;
                            string subject = "Uw ticket in order " + order.Id + " op TicketNation";
                            string heading = "bedankt voor uw aankoop bij TicketNation";
                            string ticketString = "@\n @\n Deze afdruk geeft recht op één toegang tot " + ticket.Wedstrijd.ThuisPloegNavigation.Naam + " - " + ticket.Wedstrijd.UitPloegNavigation.Naam + " op " + ticket.Wedstrijd.Datum.Date.ToString("dd/MM/yyyy") + " om " + ticket.Wedstrijd.Tijd.ToString(@"hh\:mm") + "." +
                                                "@\nUw gereserveerde stoel is nr " + ticket.Plaats.Stoelnr + " in het vak: " + vakomschrijving.Beschrijving + "." +
                                                "@\nGeniet van de wedstrijd!";
                            string message = heading + ticketString;
                            _emailSender.SendEmailAsync(email, subject, message);
                        }
                    }
                    else
                    {
                        //process abo's                            
                        Abonnement abo = new Abonnement();                            
                        abo.ClubId = cart.ClubNr;   
                        abo.Club = await _clubs.FindById(abo.ClubId);
                        abo.AankoopDatum = DateTime.Now;                            
                        abo.OrderId = order.Id;                            
                        abo.VakId= cart.vakId;
                        abo.Vak = await _vakken.FindById(abo.VakId);
                        abo.Prijs = cart.Prijs;                            
                        int stoelnr = GetVrijeStoelAbo(lijstPlaatsen, lijstWedstrijdenVanClub, cart.vakId);
                           
                        foreach (Wedstrijd w in lijstWedstrijdenVanClub)     
                        {                               
                            Plaat plaats = new Plaat();                                
                            plaats.VakId = cart.vakId;                                
                            plaats.WedstrijdId = w.Id;                               
                            plaats.Stoelnr = stoelnr;                               
                            await _plaatsen.Add(plaats);                               
                            lijstPlaatsen.Add(plaats);   //nodig om ervoor te zorgen dat ie de volgende keer weet dat er een nieuwe plaats bij is                                
                        }
                            
                        abo.StoelNr = stoelnr;
                        await _abonnementen.Add(abo);
                        var vakomschrijving = await _vakOmschrijving.FindById(abo.Vak.VakOmschrijvingId);
                        string email = user.Email;
                        string subject = "Uw abonnement in order " + order.Id + " op TicketNation";
                        string heading = "bedankt voor uw aankoop bij TicketNation";
                        string aboString = "@\n @\n Hierbij bevestigen we uw abonnement voor " + abo.Club.Naam + " voor dit seizoen." +
                                            "@\n@\n U kan dit seizoen plaatsnemen op stoelnummer " + abo.StoelNr + " in het vak: " + vakomschrijving.Beschrijving + "." ;
                        string message = heading + aboString;
                        _emailSender.SendEmailAsync(email, subject, message);
                    }
                }
            }
            catch (Exception ex)
            {
            }

            HttpContext.Session.Clear();
            TempData["succes"] = "uw tickets werden gereserveerd. U vindt deze terug in uw overzicht van bestellingen";
            return RedirectToAction("Index","Bestellingen");
        }

        public int GetVrijeStoel(List<Plaat> lijst, int wedstrijdId, int vakId)
        {
            int stoelnr = 1;
            var tempList = lijst.Where(p => p.VakId == vakId && p.WedstrijdId == wedstrijdId).OrderBy(p => p.Stoelnr).ToList();

            foreach(var plaats in tempList)
            {
                if(plaats.Stoelnr == stoelnr)
                {
                    stoelnr++;
                }
            }
            return stoelnr;
        }

        public int GetVrijeStoelAbo(List<Plaat> lijstPlaatsen, IEnumerable<Wedstrijd> lijstWedstrijden, int vakId)
        {
            var tempList = new List<Plaat>();
            int stoelnr =0;

            foreach(Wedstrijd wedstrijd in lijstWedstrijden)
            {
                foreach (Plaat plaat in lijstPlaatsen)
                {
                    if(plaat.WedstrijdId == wedstrijd.Id && plaat.VakId == vakId)
                    {
                        tempList.Add((plaat));
                    }
                }
            }

            if (tempList.Count() == 0)
            {
                stoelnr = 1;
            } else if (tempList[tempList.Count - 1].Stoelnr + 1 > stoelnr)
            {
                stoelnr = tempList[tempList.Count - 1].Stoelnr + 1;
            }
            return stoelnr;
        }


        public IActionResult Delete(int? cartId)
        {
            if (cartId == null)
            {
                return NotFound();
            }
            ShoppingCartVM? cartList
              = HttpContext.Session
              .GetObject<ShoppingCartVM>("ShoppingCart");

            CartVM? itemToRemove =
                cartList?.Cart?.FirstOrDefault(r => r.Id == cartId);

            if (itemToRemove != null)
            {
                cartList?.Cart?.Remove(itemToRemove);
                HttpContext.Session.SetObject("ShoppingCart", cartList);

            }

            TempData["succes"] = "het item werd succesvol uit het winkelmandje verwijderd";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Increment(int cartId)
        {
            if (cartId == null)
            {
                return NotFound();
            }
            ShoppingCartVM? cartList
              = HttpContext.Session
              .GetObject<ShoppingCartVM>("ShoppingCart");

            CartVM? itemToIncrement =
                            cartList?.Cart?.FirstOrDefault(r => r.Id == cartId);

            Vak vak = await _vakken.FindById(itemToIncrement.vakId);

            var lijstPlaatsen = await _plaatsen.GetAll();
            lijstPlaatsen = lijstPlaatsen.Where(p => p.VakId == itemToIncrement.vakId && p.WedstrijdId == itemToIncrement.WedstrijdNr).ToList();

            if (lijstPlaatsen.Count() +1 < vak.Aantal)
            {

                if (itemToIncrement.Aantal == 4)
                {
                    HttpContext.Session.SetObject("ShoppingCart", cartList);
                }
                else
                {
                    itemToIncrement.Aantal++;
                    HttpContext.Session.SetObject("ShoppingCart", cartList);
                }

                return RedirectToAction("Index");
            } else
            {
                TempData["fout"] = "Er zijn geen tickets meer beschikbaar in het gekozen vak";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Decrement(int cartId)
        {
            if (cartId == null)
            {
                return NotFound();
            }
            ShoppingCartVM? cartList
              = HttpContext.Session
              .GetObject<ShoppingCartVM>("ShoppingCart");

            CartVM? itemToDecrement =
                            cartList?.Cart?.FirstOrDefault(r => r.Id == cartId);

            if (itemToDecrement.Aantal <= 1)
            {
                cartList?.Cart.Remove(itemToDecrement);
                TempData["succes"] = "het item werd succesvol uit het winkelmandje verwijderd";
                HttpContext.Session.SetObject("ShoppingCart", cartList);
            }
            else
            {
                itemToDecrement.Aantal--;
                HttpContext.Session.SetObject("ShoppingCart", cartList);
            }

            return RedirectToAction("Index");
        }


        
    }
}
