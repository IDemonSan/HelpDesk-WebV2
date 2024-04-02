using HelpDesk_WebV2.Models;
using HelpDesk_WebV2.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HelpDesk_WebV2.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public HomeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetEmployees();
            return View(employees);
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                var success = await _employeeService.Registrar(empleado);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(empleado);
        }

        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var success = await _employeeService.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Actualizar(int id)
        {
            var empleado = await _employeeService.ObtenerEmpleado(id);
            return View(empleado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Actualizar(Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                var success = await _employeeService.Actualizar(empleado);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(empleado);
        }

    }
}
