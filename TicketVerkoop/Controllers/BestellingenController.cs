using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_FullStack.Areas.Identity.Data;
using System.Security.Claims;
using TicketVerkoop.Domain.Entities;
using TicketVerkoop.Service.Interfaces;
using TicketVerkoop.ViewModels;
using static TicketVerkoop.Util.Mail.EmailSender;

namespace TicketVerkoop.Controllers
{
    public class BestellingenController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IService<Order> _orders;
        private IService<Ticket> _tickets;
        private IService<Abonnement> _abonnementen;
        private IService<Vak> _vakken;
        private IService<VakOmschrijving> _vakOmschrijving;
        private IService<Club> _clubs;
        private IService<Plaat> _plaats;
        private IService<Wedstrijd> _wedstrijden;

        public BestellingenController(ILogger<HomeController> logger, IService<Order> orderService, IService<Ticket> ticketService, IService<Plaat> plaats,
            IService<Abonnement> abonnementService, IService<Vak> vakken, IService<VakOmschrijving> vakOmschrijving, IService<Club> clubService, IService<Wedstrijd> wedstrijdService)
        {
            _logger = logger;
            _orders = orderService;
            _tickets = ticketService;
            _abonnementen = abonnementService;
            _vakken = vakken;
            _vakOmschrijving = vakOmschrijving;
            _clubs = clubService;
            _plaats = plaats;
            _wedstrijden = wedstrijdService;
        }
        public async Task<IActionResult> Index()
        {
            string? userID = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var abonnementLijst = new List<Abonnement>();
            var ticketLijst = new List<Ticket>();


            foreach(Abonnement abonnement in await _abonnementen.GetAll())
            {
                if(abonnement.Order.ClientId == userID)
                {
                    int vakId = abonnement.VakId;
                    Vak vak = await _vakken.FindById(vakId);
                    int vaknr = vak.VakOmschrijvingId;
                    VakOmschrijving vakOmschrijving = await _vakOmschrijving.FindById(vaknr);
                    abonnement.Vak.VakOmschrijving = vakOmschrijving;

                    abonnementLijst.Add(abonnement);
                }
            }

            foreach(Ticket ticket in await _tickets.GetAll())
            {
                if(ticket.Order.ClientId == userID)
                {
                    int vakId = ticket.Plaats.VakId;
                    Vak vak = await _vakken.FindById(vakId);
                    ticket.Plaats.Vak = vak;

                    int vaknr = ticket.Plaats.Vak.VakOmschrijvingId;
                    VakOmschrijving vakOmschrijving = await _vakOmschrijving.FindById(vaknr);
                    ticket.Plaats.Vak.VakOmschrijving = vakOmschrijving;

                    int clubIdThuis = ticket.Wedstrijd.ThuisPloeg;
                    Club clubThuis = await _clubs.FindById(clubIdThuis);
                    ticket.Wedstrijd.ThuisPloegNavigation = clubThuis;

                    int clubIdUit = ticket.Wedstrijd.UitPloeg;
                    Club clubUit = await _clubs.FindById(clubIdUit);
                    ticket.Wedstrijd.UitPloegNavigation = clubUit;
                    
                    ticketLijst.Add(ticket);

                }
            }

            BestellingenVM vm = new BestellingenVM
            {
                abonnementen = abonnementLijst,
                Tickets = ticketLijst
            };

            return View(vm);
        }

  
        public async Task<IActionResult> Annuleer(int? Id)
        {
            Ticket ticket = await _tickets.FindById(Convert.ToInt32(Id));
            Wedstrijd wedstrijd = await _wedstrijden.FindById(ticket.WedstrijdId);
            Plaat plaats = await _plaats.FindById(Convert.ToInt32(ticket.PlaatsId));

            if(wedstrijd.Datum.Date <= DateTime.Now.AddDays(7).Date)
            {
                _tickets.Delete(ticket);
                _plaats.Delete(plaats);
                TempData["succes"] = "uw ticket werd succesvol geannuleerd. Er worden 15€ kosten in rekening gebracht";
            } else
            {
                _tickets.Delete(ticket);
                _plaats.Delete(plaats);
                TempData["succes"] = "uw ticket werd succesvol geannuleerd.";
            }

            return RedirectToAction("Index");
        }
    }
}
