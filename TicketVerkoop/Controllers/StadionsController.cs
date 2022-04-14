using Microsoft.AspNetCore.Mvc;
using TicketVerkoop.Domain.Entities;
using TicketVerkoop.Service.Interfaces;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers
{
    public class StadionsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IService<Stadion> _stadions;
        private IService<Vak> _vakken;


        public StadionsController(ILogger<HomeController> logger, IService<Stadion> stadionService, IService<Vak> vakkenService)
        {
            _logger = logger;
            _stadions = stadionService;
            _vakken = vakkenService;
        }

        public async Task<IActionResult> Index()
        {
            WedstrijdDataVM vmWedstrijden = new WedstrijdDataVM
            {
                stadions = await _stadions.GetAll(),
                vakken = await _vakken.GetAll(),
            };
            return View(vmWedstrijden);
        }
    }
}
