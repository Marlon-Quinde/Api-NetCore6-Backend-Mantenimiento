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
    public class PruebaController : Controller
    {
        [Route("[action]")]
        [HttpPost]

        public async Task<ActionResult<UsuariosDato>> GetUsuarios([FromBody] Usuarios usuarios)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlPram = DBXmlMethods.GetXML(usuarios);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoreProcedure.SPGetInfo, cadenaConexion, usuarios.Transaccion, xmlPram.ToString());

            List<Usuarios> listData = new List<Usuarios>();

            if (dsResultado.Tables.Count > 0)
            {
                try
                {   
                    foreach (DataRow row in dsResultado.Tables[0].Rows)
                    {
                        Usuarios objResponse = new Usuarios
                        {
                            Id = Convert.ToInt32(row["Id"]),
                            Nombres = row["Nombres"].ToString(),
                            Apellidos = row["Apellidos"].ToString(),
                            Usuario = row["Usuario"].ToString(),
                            Password = row["Password"].ToString(),
                            Correo = row["Correo"].ToString(),
                            Transaccion = usuarios.Transaccion

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
        public async Task<ActionResult<RespuestaSP>> SetUsuarios([FromBody] Usuarios usuarios)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlPram = DBXmlMethods.GetXML(usuarios);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoreProcedure.SPSetInfo, cadenaConexion, usuarios.Transaccion, xmlPram.ToString());

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
        public async Task<ActionResult<RespuestaSP>> DelUsuarios([FromBody] Usuarios usuarios)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlPram = DBXmlMethods.GetXML(usuarios);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoreProcedure.SPDelInfo, cadenaConexion, usuarios.Transaccion, xmlPram.ToString());

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
        public async Task<ActionResult<RespuestaSP>> UpdUsuarios([FromBody] Usuarios usuarios)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlPram = DBXmlMethods.GetXML(usuarios);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoreProcedure.SPUpdInfo, cadenaConexion, usuarios.Transaccion, xmlPram.ToString());

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

        [Route("[action]")]
        [HttpPost]

        public async Task<ActionResult<Perfiles>> GetPerfiles([FromBody] Perfiles perfiles)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlPram = DBXmlMethods.GetXML(perfiles);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoreProcedure.SPGetInfo, cadenaConexion, perfiles.Transaccion, xmlPram.ToString());

            List<Perfiles> listData = new List<Perfiles>();

            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    foreach (DataRow row in dsResultado.Tables[0].Rows)
                    {
                        Perfiles objResponse = new Perfiles
                        {
                            Id = Convert.ToInt32(row["id"]),
                            Descripcion = row["descripcion"].ToString(),
                            Transaccion = perfiles.Transaccion

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
        public async Task<ActionResult<RespuestaSP>> SetPerfiles([FromBody] Perfiles perfiles)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlPram = DBXmlMethods.GetXML(perfiles);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoreProcedure.SPSetInfo, cadenaConexion, perfiles.Transaccion, xmlPram.ToString());

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
                    objResponse.Leyenda = "No se inserto el nuevo perfil";
                }
            }
            return Ok(objResponse);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<RespuestaSP>> DelPerfiles([FromBody] Perfiles perfiles)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlPram = DBXmlMethods.GetXML(perfiles);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoreProcedure.SPDelInfo, cadenaConexion, perfiles.Transaccion, xmlPram.ToString());

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
                    objResponse.Leyenda = "No se elimino el Perfil";
                }
            }
            return Ok(objResponse);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<RespuestaSP>> UpdPerfiles([FromBody] Perfiles perfiles)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlPram = DBXmlMethods.GetXML(perfiles);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoreProcedure.SPUpdInfo, cadenaConexion, perfiles.Transaccion, xmlPram.ToString());

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
                    objResponse.Leyenda = "No se modifico el perfil";
                }
            }
            return Ok(objResponse);
        }

    }
}
