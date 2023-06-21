namespace NetCoreYouTube.Models
{
    public class Usuario
    {
        public int Idusuario { get; set; }
        public string Nombreusuario { get; set; }
        public string Password { get; set; }    
        public string Rol { get; set; }

        public static List<Usuario> DB()
        {
            var list = new List<Usuario>()
            {
                new Usuario
                {
                    Idusuario = 1,
                    Nombreusuario = "Wilson",
                    Password = "123",
                    Rol = "Empleado"
                },
                new Usuario
                {
                    Idusuario = 2,
                    Nombreusuario = "Samuel",
                    Password = "123",
                    Rol = "asesor"
                },
                new Usuario
                {
                    Idusuario = 3,
                    Nombreusuario = "Luciana",
                    Password = "123",
                    Rol = "Empleado"
                },
                new Usuario
                {
                    Idusuario = 4,
                    Nombreusuario = "Paula",
                    Password = "123",
                    Rol = "Administrador"
                }

            };
            return list;
        }
    }
}
