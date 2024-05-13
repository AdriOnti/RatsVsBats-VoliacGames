using System.Text.Json.Serialization;

namespace MinimalAPI.Models
{
    // Clase usuario sin ID
    public class NoIdUser
    {
        public string? userEmail { get; set; }
        public string? userPassword { get; set; }

    }
}
