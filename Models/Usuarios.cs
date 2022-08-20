using System.ComponentModel.DataAnnotations;

namespace EjercicioAPI.Models
{
    public class Usuarios
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
    }

    public class UserDetails
    {
        internal Result result;

        [Key]
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
    }

    public class Result
    {
        public bool result { get; set; }
        public string message { get; set; }
    }
}
