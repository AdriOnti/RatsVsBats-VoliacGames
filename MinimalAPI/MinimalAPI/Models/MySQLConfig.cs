using Microsoft.EntityFrameworkCore;

namespace MinimalAPI.Models
{
    public class MySQLConfig:DbContext
    {
        protected readonly IConfiguration Configuration;
        public string ConnectionString;

        /// <summary>
        /// Método que lee la configuracion de coneccion a nuestra bd del fichero appsettings
        /// </summary>
        /// <param name="configuration"></param>
        public MySQLConfig(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = configuration.GetConnectionString("WebApiDatabase");
        }
    }
}
