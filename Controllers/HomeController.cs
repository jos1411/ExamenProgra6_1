using System;
using System.Data;
using System.Diagnostics;
using Clinica_Dental.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clinica_Dental.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AccesoDatos _accesoDatos;

        public HomeController(ILogger<HomeController> logger, AccesoDatos accesoDatos)
        {
            _logger = logger;
            _accesoDatos = accesoDatos;
        }

        // Acción principal: carga la vista Index y las citas del día.
        public IActionResult Index()
        {
            // Se obtiene el listado de citas para la fecha de hoy.
            DataTable dtCitas = _accesoDatos.ObtenerCitasDelDia(DateTime.Today);
            ViewBag.CitasDelDia = dtCitas;
            return View();
        }

        // Registrar un nuevo paciente (CREATE).
        [HttpPost]
        public IActionResult RegistrarPaciente(Paciente nuevoPaciente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _accesoDatos.AgregarPaciente(nuevoPaciente);
                    TempData["Mensaje"] = "Paciente registrado correctamente.";
                }
                else
                {
                    TempData["Mensaje"] = "Error: Datos de paciente inválidos.";
                }
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error al registrar paciente: " + ex.Message;
            }
            return RedirectToAction("Index");
        }

        // Actualizar un paciente existente (UPDATE).
        [HttpPost]
        public IActionResult ActualizarPaciente(Paciente paciente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje;
                    bool actualizado = _accesoDatos.ActualizarPaciente(paciente, out mensaje);
                    TempData["Mensaje"] = actualizado ? "Paciente actualizado correctamente." : "Error al actualizar paciente: " + mensaje;
                }
                else
                {
                    TempData["Mensaje"] = "Error: Datos de paciente inválidos.";
                }
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error al actualizar paciente: " + ex.Message;
            }
            return RedirectToAction("Index");
        }

        // Eliminar un paciente (DELETE).
        [HttpPost]
        public IActionResult EliminarPaciente(int idPaciente)
        {
            try
            {
                string mensaje;
                bool eliminado = _accesoDatos.EliminarPaciente(idPaciente, out mensaje);
                TempData["Mensaje"] = eliminado ? "Paciente eliminado correctamente." : "Error al eliminar paciente: " + mensaje;
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error al eliminar paciente: " + ex.Message;
            }
            return RedirectToAction("Index");
        }

        // Registrar una nueva cita (CREATE).
        [HttpPost]
        public IActionResult InsertarCita(Cita nuevaCita)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _accesoDatos.InsertarCita(nuevaCita);
                    TempData["Mensaje"] = "Cita registrada correctamente.";
                }
                else
                {
                    TempData["Mensaje"] = "Error: Datos de cita inválidos.";
                }
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error al registrar cita: " + ex.Message;
            }
            return RedirectToAction("Index");
        }

        // Buscar un paciente por cédula (READ).
        [HttpPost]
        public IActionResult BuscarPaciente(string Cedula)
        {
            try
            {
                Paciente paciente = _accesoDatos.ObtenerPacientePorCedula(Cedula);
                if (paciente != null)
                {
                    // Devuelve la vista Index con el paciente en el modelo (para, por ejemplo, mostrar el formulario de cita).
                    return View("Index", paciente);
                }
                else
                {
                    TempData["Mensaje"] = "Paciente no encontrado. Por favor, regístrelo.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error al buscar paciente: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // Acción de Error.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
