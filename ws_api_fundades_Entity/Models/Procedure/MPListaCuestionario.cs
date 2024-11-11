namespace ws_api_fundades_Entity.Models.Procedure
{
    public class MPListaCuestionario
    {
        public required int id { get; set; }
        public required string pregunta { get; set; }
        public required string tipo { get; set; }
        public required string definicion { get; set; }
        public required int nroRespuesta { get; set; }
        public required string descripcion { get; set; }

    }
}
