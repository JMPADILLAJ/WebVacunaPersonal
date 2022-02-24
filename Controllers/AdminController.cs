using System;
using System.Collections.Generic;
using System.Web.Mvc;
using farma_Web.Bussines;
using farma_Web.Models;

namespace farma_Web.Controllers
{
    public class AdminController : Controller
    {

        Procesos p = new Procesos();
        Validate_CI v = new Validate_CI();
        // GET: Admin
        public ActionResult IndexAdmin(string json="", string mensaje = "")
        {

            ViewBag.Mensaje = mensaje;
            if (json=="")
            {
                ViewBag.listaEmpleados = p.ListaEmpleados();
            }
            else
            {
                ViewBag.listaEmpleados = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Empleado>>(json);
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult AgregarEmpleado(string nombres, string apellidos, int cedula, string correo)
        {

            if (v.VerificaIdentificacion(cedula.ToString()))
            {
                var r = p.insertarEmpleado(nombres, apellidos, cedula, correo);

                if (r == "0")
                {
                    return RedirectToAction("IndexAdmin", "Admin", new { mensaje = "Empleado insertado correctamente." });
                }
                else if (r == "1")
                {
                    return RedirectToAction("IndexAdmin", "Admin", new { mensaje = "Ya existe un  usuario con Cédula: " + cedula });
                }
                else
                {
                    return RedirectToAction("IndexAdmin", "Admin", new { mensaje = "No fue posible insertar empleado. " });
                }

            }
            else
            {
                return RedirectToAction("IndexAdmin", "Admin", new { mensaje = "No fue posible insertar empleado, número de cédula inválido " });
            }
           
           
        }

         [HttpPost]
        public ActionResult ListaEmpleadosTipoVacuna(string tipo_vacuna)
        {
            var r = p.ListaEmpleadosTipoVacuna(tipo_vacuna);           
            return RedirectToAction("IndexAdmin", "Admin", new { json=r });
          
        }


        [HttpPost]
        public ActionResult ListaEmpleadosEstadoVacuna(int  estado_vacuna)
        {
            var r = p.ListaEmpleadosEstadoVacuna(estado_vacuna);
            return RedirectToAction("IndexAdmin", "Admin", new {json = r });

        }


        [HttpPost]
        public ActionResult ListaEmpleadosFechaVacuna(DateTime fecha_vacuna)
        {
            var r = p.ListaEmpleadosFechaVacuna(fecha_vacuna);
            return RedirectToAction("IndexAdmin", "Admin", new { json = r });

        }

    }
}