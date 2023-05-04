using Prueba.API.Shared;
using Hospital.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Xml.Linq;
using Clases.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Prueba.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize]
    public class ClientesController : Controller
    {
        [Route("[action]")]
        [HttpPost]

        public async Task<ActionResult<Clientes>> GetClientes([FromBody] Clientes clientes)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlPram = DBXmlMethods.GetXML(clientes);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoreProcedure.SPGetInfo, cadenaConexion, clientes.Transaccion, xmlPram.ToString());

            List<Clientes> listData = new List<Clientes>();

            if (dsResultado.Tables.Count > 0)
            {
                try
                {   
                    foreach (DataRow row in dsResultado.Tables[0].Rows)
                    {
                        Clientes objResponse = new Clientes
                        {
                            Id = Convert.ToInt32(row["Id"]),
                            Cedula = row["Cedula"].ToString(),
                            Nombres = row["Nombres"].ToString(),
                            Apellidos = row["Apellidos"].ToString(),
                            FechaNacimiento = row["FechaNacimiento"].ToString(),
                            Direccion = row["Direccion"].ToString(),
                            Numero = row["Numero"].ToString(),
                            Correo = row["Correo"].ToString(),
                            Transaccion = clientes.Transaccion

                        };
                        listData.Add(objResponse);
                    }
                }
                catch (Exception ex)
                {
                    Console.Write("error");
                }
            }
            return Ok(listData);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<RespuestaSP>> SetClientes([FromBody] Clientes clientes)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlPram = DBXmlMethods.GetXML(clientes);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoreProcedure.SPSetInfo, cadenaConexion, clientes.Transaccion, xmlPram.ToString());

            RespuestaSP objResponse = new RespuestaSP();

            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    objResponse.Respuesta = dsResultado.Tables[0].Rows[0]["Respuesta"].ToString();
                    objResponse.Leyenda = dsResultado.Tables[0].Rows[0]["Leyenda"].ToString();
                }
                catch (Exception ex)
                {
                    objResponse.Respuesta = "Error";
                    objResponse.Leyenda = "No se inserto el usuario";
                }
            }
            return Ok(objResponse);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<RespuestaSP>> DelClientes([FromBody] Clientes clientes)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlPram = DBXmlMethods.GetXML(clientes);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoreProcedure.SPDelInfo, cadenaConexion, clientes.Transaccion, xmlPram.ToString());

            RespuestaSP objResponse = new RespuestaSP();

            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    objResponse.Respuesta = dsResultado.Tables[0].Rows[0]["Respuesta"].ToString();
                    objResponse.Leyenda = dsResultado.Tables[0].Rows[0]["Leyenda"].ToString();
                }
                catch (Exception ex)
                {
                    objResponse.Respuesta = "Error";
                    objResponse.Leyenda = "No se elimino el Usuario";
                }
            }
            return Ok(objResponse);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<RespuestaSP>> UpdClientes([FromBody] Clientes clientes)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlPram = DBXmlMethods.GetXML(clientes);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoreProcedure.SPUpdInfo, cadenaConexion, clientes.Transaccion, xmlPram.ToString());

            RespuestaSP objResponse = new RespuestaSP();

            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    objResponse.Respuesta = dsResultado.Tables[0].Rows[0]["Respuesta"].ToString();
                    objResponse.Leyenda = dsResultado.Tables[0].Rows[0]["Leyenda"].ToString();
                }
                catch (Exception ex)
                {
                    objResponse.Respuesta = "Error";
                    objResponse.Leyenda = "No se actualizo el nuevo Usuario";
                }
            }
            return Ok(objResponse);
        }
    }
}
