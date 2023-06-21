using System.Security.Claims;

namespace NetCoreYouTube.Models
{
    public class Jwt
    {
        public string Key  { get; set; }
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }


        public static dynamic validarToken(ClaimsIdentity identity)
        {
            try
            {
                if(identity.Claims.Count() == 0) 
                {
                    return new
                    {
                        success = false,
                        message = "Verificar Token",
                        result = ""
                    };
                }
                 var id = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;
                Usuario usuario = Usuario.DB().FirstOrDefault(x => x.Idusuario.ToString() == id.ToString());

                return new
                {
                    success = true,
                    message = "Exitoso",
                    result = usuario
                };

            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "Error: " + ex.Message.ToString(),
                    result = ""
                };
            
            }
        }


    }
}
