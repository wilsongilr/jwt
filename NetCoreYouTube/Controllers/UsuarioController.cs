using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NetCoreYouTube.Models;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace NetCoreYouTube.Controllers
{
    [ApiController]
    [Route("Usuario")]
    public class UsuarioController : ControllerBase 
    {

        public IConfiguration _configuration;

        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration; 

        }

        [HttpPost]
        [Route("Login")]
       public dynamic IniciarSesion([FromBody] Object optData)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(optData.ToString());

            string user = data.Nombreusuario.ToString();
            string psw = data.Password.ToString();

            Usuario usuario = Usuario.DB().Where(x => x.Nombreusuario == user && x.Password == psw).FirstOrDefault();

            if(usuario == null)
            {
                return new
                {
                    success = false,
                    message = "Credenciales Incorretas",
                    result = ""
                };
            }

            //Sirve para tomar los datos de JWT del appsettings
            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();


            //Valores que vamos a almacenar en nuetro token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", usuario.Idusuario.ToString()),
                new Claim("usuario", usuario.Nombreusuario)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: singIn
                );
            return new
            {
                success = true,
                message = "exitoso",
                result = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
