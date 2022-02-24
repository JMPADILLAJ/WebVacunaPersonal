using farma_Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace farma_Web.Bussines
{
    public class Procesos
    {

        public List<Empleado> ListaEmpleados()
        {
            List<Empleado> ListaEmpleados = new List<Empleado>();

            //string url = "http://localhost:7010/get/empleados";  Para pruebas locales
            string url = "http://jmpadillaj-001-site1.dtempurl.com/get/empleados";

            WebRequest oRequest = WebRequest.Create(url);
            oRequest.Method = "Get";
            oRequest.ContentType = "application/json;charset=UTF-8";

            WebResponse oResponse = oRequest.GetResponse();
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var respuesta = oSR.ReadToEnd().Trim();
                ListaEmpleados = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Empleado>>(respuesta);

                return ListaEmpleados;
            }
        }

 
        public string insertarEmpleado(string nombres, string apellidos, int cedula, string correo)
        {

            //string url = "http://localhost:7010/insert"; Para pruebas locales
            string url = "http://jmpadillaj-001-site1.dtempurl.com/insert";

            WebRequest oRequest = WebRequest.Create(url);
            oRequest.Method = "Post";
            oRequest.ContentType = "application/json;charset=UTF-8";

            using (var oSW = new StreamWriter(oRequest.GetRequestStream()))
            {
               
                  string json = @"{""cedula"": """ + cedula + @""",
                                         ""nombres""   : """ + nombres + @""",
                                         ""apellidos""   : """ + apellidos + @""",
                                         ""correo""   : """ + correo + @"""}";

                oSW.Write(json);
                oSW.Flush();
                oSW.Close();
            }

            WebResponse oResponse = oRequest.GetResponse();
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var respuesta = oSR.ReadToEnd().Trim();
               
                return respuesta;
            }
        }


        public string  ListaEmpleadosTipoVacuna(string tipo_vacuna)
        {

            //var url = "http://localhost:7010/get/tipovacuna?tipo_vacuna=" + "'" + tipo_vacuna+ "'" ; Para pruebas locales
            var url = "http://jmpadillaj-001-site1.dtempurl.com/get/tipovacuna?tipo_vacuna=" + "'" + tipo_vacuna + "'";

            WebRequest oRequest = WebRequest.Create(url);
            oRequest.Method = "Get";
            oRequest.ContentType = "application/json;charset=UTF-8";

            WebResponse oResponse = oRequest.GetResponse();
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var respuesta = oSR.ReadToEnd().Trim();
                return respuesta;
            }
        }


        public string ListaEmpleadosEstadoVacuna(int estado_vacuna)
        {

            //var url = $"http://localhost:7010/get/estadovacuna?estado_vacuna={estado_vacuna}"; Para pruebas locales
            var url = $"http://jmpadillaj-001-site1.dtempurl.com/get/estadovacuna?estado_vacuna={estado_vacuna}";

            WebRequest oRequest = WebRequest.Create(url);
            oRequest.Method = "Get";
            oRequest.ContentType = "application/json;charset=UTF-8";

            WebResponse oResponse = oRequest.GetResponse();
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var respuesta = oSR.ReadToEnd().Trim();
                return respuesta;
            }
        }

        public string ListaEmpleadosFechaVacuna(DateTime fecha_vacuna)
        {
            //var url = $"http://localhost:7010/get/fechavacuna?fecha_vacuna={fecha_vacuna.ToString("MM/dd/yyyy")}"; Para pruebas locales
            var url = $"http://jmpadillaj-001-site1.dtempurl.com/get/fechavacuna?fecha_vacuna={fecha_vacuna.ToString("MM/dd/yyyy")}";
            
            WebRequest oRequest = WebRequest.Create(url);
            oRequest.Method = "Get";
            oRequest.ContentType = "application/json;charset=UTF-8";

            WebResponse oResponse = oRequest.GetResponse();
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var respuesta = oSR.ReadToEnd().Trim();
                return respuesta;
            }
        }


        public Empleado GetEmpleado(int cedula)
        {
            Empleado empleado = new Empleado();

            //string url = $"http://localhost:7010/get/empleado?cedula={cedula}"; Para pruebas locales
            string url = $"http://jmpadillaj-001-site1.dtempurl.com/get/empleado?cedula={cedula}";

            WebRequest oRequest = WebRequest.Create(url);
            oRequest.Method = "Get";
            oRequest.ContentType = "application/json;charset=UTF-8";

            WebResponse oResponse = oRequest.GetResponse();
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var respuesta = oSR.ReadToEnd().Trim();
                empleado = Newtonsoft.Json.JsonConvert.DeserializeObject<Empleado>(respuesta);

                return empleado;
            }
        }


        public string ActualizarEmpleado(UpdateEmpleado ue)
        {
            Empleado empleado = new Empleado();

            //string url = $"http://localhost:7010/update"; Para pruebas locales
            string url = $"http://jmpadillaj-001-site1.dtempurl.com/update";

            WebRequest oRequest = WebRequest.Create(url);
            oRequest.Method = "Post";
            oRequest.ContentType = "application/json;charset=UTF-8";


            using (var oSW = new StreamWriter(oRequest.GetRequestStream()))
            {
                string json = @"{""id_empleado"": """ + ue.id_empleado + @""",
                                         ""cedula""   : """ + ue.cedula + @""",
                                         ""nombres""   : """ + ue.nombres + @""",
                                         ""apellidos""   : """ + ue.apellidos + @""",
                                         ""correo""   : """ + ue.correo + @""",
                                         ""fecha_nacimiento""   : """ + ue.fecha_nacimiento + @""",
                                         ""direccion_domicilio""   : """ + ue.direccion_domicilio + @""",
                                         ""telefono""   : """ + ue.telefono + @""",
                                         ""estado_vacuna""   : """ + ue.estado_vacuna + @""",
                                         ""tipo_vacuna""   : """ + ue.tipo_vacuna + @""",
                                         ""fecha_vacuna""   : """ + ue.fecha_vacuna.ToString("MM/dd/yyyy") + @""",
                                         ""numero_dosis""   : """ + ue.numero_dosis + @"""}";

                oSW.Write(json);
                oSW.Flush();
                oSW.Close();
            }

            WebResponse oResponse = oRequest.GetResponse();
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var respuesta = oSR.ReadToEnd().Trim();
                return respuesta;
            }
        }
    }
}