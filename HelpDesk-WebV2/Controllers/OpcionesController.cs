using HelpDesk_WebV2.Models;
using HelpDesk_WebV2.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk_WebV2.Controllers
{
    public class OpcionesController : Controller
    {
        private readonly OpcionesService _opcionesService;

        public OpcionesController(OpcionesService opcionesService)
        {
            _opcionesService = opcionesService;
        }

        public async Task<IActionResult> Index()
        {
            var opciones = await _opcionesService.GetOpcionesAsync();
            return View(opciones);
        }
    }
}