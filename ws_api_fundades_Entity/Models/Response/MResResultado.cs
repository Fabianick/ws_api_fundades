using System.Text.Json.Serialization;

namespace ws_api_fundades_Entity.Models.Response
{
    public class MResResultado
    {
        [JsonIgnore]
        public long codigoRespuesta { get; set; }
        public string? resultado { get; set; }
    }
}
