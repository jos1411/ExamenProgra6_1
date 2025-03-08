using System.Diagnostics;
using Clinica_Dental.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clinica_Dental.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AccesoDatos _accesoDatos; // Se inyecta AccesoDatos

        public HomeController(ILogger<HomeController> logger, AccesoDatos accesoDatos)
        {
            _logger = logger;
            _accesoDatos = accesoDatos; // Se asigna la instancia de AccesoDatos
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Aquí puedes agregar un método para registrar paciente
        [HttpPost]
        public IActionResult RegistrarPaciente(Pacientes nuevoPaciente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _accesoDatos.AgregarPaciente(nuevoPaciente); // Llamada al método para agregar paciente
                    TempData["Mensaje"] = "Paciente registrado correctamente."; // Mensaje de éxito
                }
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = $"Error al registrar paciente: {ex.Message}"; // Mensaje de error
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
