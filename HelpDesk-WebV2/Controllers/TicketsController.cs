using HelpDesk_WebV2.Models;
using HelpDesk_WebV2.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk_WebV2.Controllers
{
    public class TicketsController : Controller
    {
        private readonly TicketsService _ticketsService;

        public TicketsController(TicketsService ticketsService)
        {
            _ticketsService = ticketsService;
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketsService.GetTicketsAsync();
            return View(tickets);
        }
    }
}