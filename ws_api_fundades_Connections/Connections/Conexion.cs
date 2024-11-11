using Microsoft.Extensions.Configuration;

namespace ws_api_fundades_Connections.Connections
{
    public class Conexion
    {
        public string? ConexionBdUpc = null;
        public Conexion()
        {
            var Configuration = GetConfiguration();
            ConexionBdUpc = Configuration.GetSection("ConnectionStrings").GetSection("ConexionBdUpc").Value;
        }

        private IConfigurationRoot GetConfiguration()
        {
            var wBuilder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true)
                    ;
            return wBuilder.Build();
        }
    }
}
