namespace MinimalAPI.Models
{
    public class Profile
    {
        // Clase perfil
        public int id { get; set; }
        public string nickname { get; set; }
        public string location { get; set; }
        public int completedMissions { get; set; }
        public int completedBranches { get; set; }
        public int points { get; set; }

    }
}
