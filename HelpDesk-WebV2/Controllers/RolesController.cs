using HelpDesk_WebV2.Models;
using HelpDesk_WebV2.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk_WebV2.Controllers
{
    public class RolController : Controller
    {
        private readonly RolesService _rolService;

        public RolController(RolesService rolService)
        {
            _rolService = rolService;
        }

        public async Task<IActionResult> Index()
        {
            var rol = await _rolService.GetRolAsync();
            return View(rol);
        }
    }
}