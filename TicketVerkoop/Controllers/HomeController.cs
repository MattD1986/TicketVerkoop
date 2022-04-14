using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TicketVerkoop.Domain.Entities;
using TicketVerkoop.Models;
using TicketVerkoop.Service.Interfaces;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IService<Club> _clubs;

        public HomeController(ILogger<HomeController> logger, IService<Club> clubService)
        {
            _logger = logger;
            _clubs = clubService;
        }

        public async Task<IActionResult> Index()
        {
            ClubVM vmClubs = new ClubVM
            {
                clubs = await _clubs.GetAll(),
            };
            return View(vmClubs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
