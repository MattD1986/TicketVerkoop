using Microsoft.AspNetCore.Mvc;

namespace TicketVerkoop.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
