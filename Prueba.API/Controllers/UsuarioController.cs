using Clases.BL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Xml.Linq;
using Prueba.API.Shared;
using Hospital.BL;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Prueba.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IConfiguration _configuration;

        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [Route("[action]")]
        [HttpPost]

        public async Task<ActionResult> Get([FromBody] Usuarios user)
        {
            var cadenaConexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["conexion_bd"];
            XDocument xmlPram = DBXmlMethods.GetXML(user);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(NameStoreProcedure.SPGetInfoUser, cadenaConexion, user.Transaccion, xmlPram.ToString());

            //List<Pacientes> listData = new List<Pacientes>();

            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    if (dsResultado.Tables[0].Rows.Count> 0)
                    {
                        Usuarios userTmp = new Usuarios();
                        userTmp.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["Id"]);
                        userTmp.Usuario = dsResultado.Tables[0].Rows[0]["Usuario"].ToString();
                        return Ok(JsonConvert.SerializeObject(CrearToken(userTmp)));
                    }
                    else
                    {
                        RespuestaSP objresponse = new RespuestaSP();
                        objresponse.Leyenda = "Error en las credenciales de acceso";
                        objresponse.Respuesta = "Error";
                        return BadRequest(objresponse);
                    }
                }
                catch (Exception ex)
                {
                    Console.Write("error");
                }
            }
            //return Ok(listData);
            return Ok();
        }

        private string CrearToken(Usuarios user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Usuario),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
