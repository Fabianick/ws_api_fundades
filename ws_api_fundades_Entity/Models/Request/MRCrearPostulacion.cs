using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ws_api_fundades_Entity.Models.Request
{
    public class MRCrearPostulacion
    {
        [Required]
        public required string usuario { get; set; }
        [JsonIgnore]
        public DateTime fechaPostulacion { get;  set; } = DateTime.Now;
        [Required]
        public required string puesto { get; set; }
        [Required]
        public required List<MRRespuestas> respuestas { get; set; }

    }

    public class MRRespuestas
    {
        [Required]
        public required string pregunta { get; set; }
        [Required]
        public required string tipoPregunta { get; set; }
        [Required] 
        public required int respuesta { get; set; }
    }
}
