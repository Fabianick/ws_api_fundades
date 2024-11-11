using System.Text;
using System.Text.Json;
using ws_api_fundades_Entity.Models.Log;
using ws_api_fundades_Services.Services;

namespace ws_api_fundades.Middleware
{
    /// <summary>
    /// Clase para guardar todas las transacciones http en el log de base de datos
    /// </summary>
    public class ApiLogsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogService _logService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logService"></param>
        public ApiLogsMiddleware(RequestDelegate next, ILogService logService)
        {
            _next = next;
            _logService = logService;
        }

        /// <summary>
        /// Registar logs
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            // Leer la solicitud
            var request = context.Request;
            var requestBodyContent = await ReadRequestBodyAsync(request);

            // Insertar log inicial
            var logId = await _logService.InsertApiLogAsync(new LogInsertApiModel
            {
                ApiName = request.Method,
                ApiEndpoint = request.Path,
                JsonRequest = requestBodyContent,
                FechaCreacion = DateTime.Now
            });

            // Guardar la respuesta original para luego capturar el contenido de respuesta
            var originalBodyStream = context.Response.Body;

            try
            {
                // Crear un MemoryStream para capturar la respuesta
                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    // Llamar al siguiente middleware en la cadena
                    await _next(context);

                    // Leer el contenido de la respuesta
                    var responseBodyContent = await ReadResponseBodyAsync(context.Response);
                    var statusCode = context.Response.StatusCode;

                    // Actualizar log con la respuesta
                    await _logService.UpdateApiLogAsync(new LogUpdateApiModel
                    {
                        IdLog = logId,
                        JsonResponse = responseBodyContent,
                        FechaRespuesta = DateTime.Now,
                        CodeStatusResponse = statusCode,
                        Error = statusCode >= 400 ? "Error en la solicitud" : null
                    });

                    // Escribir el contenido de la respuesta en el flujo original
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            catch (Exception ex)
            {
                // Restaurar el flujo de respuesta original antes de escribir en él
                context.Response.Body = originalBodyStream;

                // Limpiar la respuesta antes de escribir el mensaje de error
                context.Response.Clear();
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                // En caso de error, actualizar el log con el mensaje de error
                await _logService.UpdateApiLogAsync(new LogUpdateApiModel
                {
                    IdLog = logId,
                    JsonResponse = string.Empty,
                    FechaRespuesta = DateTime.Now,
                    CodeStatusResponse = 500,
                    Error = ex.Message
                });

                var errorResponse = new { message = "Ocurrió un error en el servidor. Comuníquese con sistemas." };
                var jsonResponse = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(jsonResponse);

            }
            finally
            {
                // Restaurar el flujo de la respuesta original
                context.Response.Body = originalBodyStream;
            }
        }

        private async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            request.EnableBuffering();
            using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, leaveOpen: true))
            {
                var body = await reader.ReadToEndAsync();
                request.Body.Position = 0;
                return body;
            }
        }

        private async Task<string> ReadResponseBodyAsync(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }
    }
}
