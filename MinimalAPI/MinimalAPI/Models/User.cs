using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MinimalAPI.Models
{
    // Clase usuario
    public class User
    {
        [Key]
        public int idUsers { get; set; }
        public string? userEmail { get; set; }
        [IgnoreDataMember]
        public string? userPassword { get; set; }

    }
}
