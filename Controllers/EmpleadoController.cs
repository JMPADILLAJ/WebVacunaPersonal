using farma_Web.Bussines;
using farma_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace farma_Web.Controllers
{
    public class EmpleadoController : Controller
    {
        Procesos p = new Procesos();
        // GET: Empleado
        public ActionResult IndexEmpleado(int id, string mensaje ="")
        {
            ViewBag.Mensaje = mensaje;
            ViewBag.Empleado = p.GetEmpleado(id);
            return View();
        }

        [HttpPost]
        public ActionResult ActualizarEmpleado(int id_empleado, string nombres, string apellidos, string cedula, 
        string correo, string fecha_nacimiento, string direccion_domicilio, string telefono, int estado_vacuna,
        string tipo_vacuna, DateTime fecha_vacuna, int numero_dosis)
        {
            UpdateEmpleado empleado = new UpdateEmpleado() {
            id_empleado= id_empleado,
            nombres=nombres,
            apellidos=apellidos,
            cedula=Int32.Parse(cedula),
            correo=correo,
            fecha_nacimiento=fecha_nacimiento,
            direccion_domicilio=direccion_domicilio,
            telefono=telefono,
            estado_vacuna=estado_vacuna,
            tipo_vacuna=tipo_vacuna,
            fecha_vacuna=fecha_vacuna,
            numero_dosis=numero_dosis
            };

           

            var r = p.ActualizarEmpleado(empleado); ;

            if (r == "0")
            {
                return RedirectToAction("IndexEmpleado", "Empleado", new { id = cedula, mensaje = "Empleado actualizado correctamente." });
            }
            else if (r == "1")
            {
                return RedirectToAction("IndexEmpleado", "Empleado", new { id= cedula, mensaje = "Error no fue posible la actualización." });
            }
          
            return View();
        }
    }
}