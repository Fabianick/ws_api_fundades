namespace ws_api_fundades_Entity.Models.Log
{
    public class LogInsertApiModel
    {
        public required string ApiName { get; set; }
        public required string ApiEndpoint { get; set; }
        public required string JsonRequest { get; set; }
        public required DateTime FechaCreacion { get; set; }
    }

    public class LogUpdateApiModel
    {
        public required int IdLog { get; set; }
        public required string JsonResponse { get; set; }
        public required DateTime FechaRespuesta { get; set; }
        public required int CodeStatusResponse { get; set; }
        public required string? Error { get; set; }
    }
}
