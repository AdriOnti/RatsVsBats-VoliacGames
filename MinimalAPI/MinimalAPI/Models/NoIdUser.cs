using System.Text.Json.Serialization;

namespace MinimalAPI.Models
{
    // Clase usuario sin ID
    public class NoIdUser
    {
        public string email { get; set; }
        public string password { get; set; }

    }
}
