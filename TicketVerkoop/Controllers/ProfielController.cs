using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_FullStack.Areas.Identity.Data;
using System.Security.Claims;
using TicketVerkoop.Domain.Entities;
using TicketVerkoop.Service.Interfaces;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers
{
    public class ProfielController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public ProfielController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            string? userID = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userID);

            ProfielVM vm = new ProfielVM
            {
                Email = user.Email,
                Name = user.Naam,
                Adres = user.Adres,
                Postcode = user.Postcode,
                Woonplaats = user.Woonplaats,
            };
            
            return View(vm);
        }
    


    [HttpPost]
    [Authorize]
        public async Task<IActionResult> Index(ProfielVM vM)
        {
            if (ModelState.IsValid)
            {
                string? userID = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _userManager.FindByIdAsync(userID);


                user.Email = vM.Email;
                user.Adres = vM.Adres;
                user.Postcode = vM.Postcode;
                user.Woonplaats = vM.Woonplaats;

                await _userManager.UpdateAsync(user);

            }

            TempData["succes"] = "uw gegevens werden succesvol bijgewerkt";
            return RedirectToAction("Index");
        }

    }

}
