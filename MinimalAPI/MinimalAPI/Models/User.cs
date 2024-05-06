using System.Text.Json.Serialization;

namespace MinimalAPI.Models
{
    // Clase usuario
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }

    }
}
