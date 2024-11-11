namespace ws_api_fundades_Entity.Models.Response
{
    public class MResponseCuestionario
    {
        public required int id { get; set; }
        public required string pregunta { get; set; }
        public required string tipo { get; set; }
        public required string preguntaDescripcion { get; set; }
        public required List<MRCRespuesta> respuesta { get; set; }
    }

    public class MRCRespuesta
    {
        public required int nroRespuesta { get; set; }
        public required string respuestaDescripcion { get; set; }

    }
        
        
}
