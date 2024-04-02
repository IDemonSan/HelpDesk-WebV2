using HelpDesk_WebV2.Models;
using HelpDesk_WebV2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk_WebV2.Controllers
{
    public class AccountController : Controller
    {

        private readonly AuthService _authService;

        public AccountController(AuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var token = await _authService.Login(model.Usuario, model.Contrasena);

            if (!string.IsNullOrEmpty(token))
            {
                // Almacenar el token en la sesión
                HttpContext.Session.SetString("JwtToken", token);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Credenciales inválidas");
            return View();
        }

    }
}
