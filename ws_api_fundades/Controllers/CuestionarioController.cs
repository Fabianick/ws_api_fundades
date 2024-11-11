using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using ws_api_fundades_Entity.Models.Procedure;
using ws_api_fundades_Entity.Models.Request;
using ws_api_fundades_Entity.Models.Response;
using ws_api_fundades_Services.Services;
using static ws_api_fundades_Entity.Examples.ExampleRequest.RequestExample;
using static ws_api_fundades_Entity.Examples.ExampleResponse.ResponseExample;

namespace ws_api_fundades.Controllers
{
    /// <summary>
    /// Contorllador para los cuestionarios
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Authorize]
    public class CuestionarioController : ControllerBase
    {
        private readonly ICuestionarioService _cuestionarioS;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cuestionarioS"></param>
        public CuestionarioController(ICuestionarioService cuestionarioS)
        {
            _cuestionarioS = cuestionarioS;
        }

        /// <summary>
        /// Metodo para obtener las preguntas del formulario
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Petición exitosa, retorna una lista con las preguntas y respuestas del formulario.</response>
        /// <response code="401">No tienes autorizacion para consumir este metodo.</response>
        /// <response code="404">No se encontraron preguntas.</response>
        /// <response code="500">Ocurrió un error en el servidor.</response>
        [ProducesResponseType(typeof(List<MResponseCuestionario>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerResponseExample(200, typeof(CuestionarioListaResponseExample))]
        [SwaggerResponseExample(404, typeof(CuestionarioListaErrorResponseExample))]
        [SwaggerResponseExample(500, typeof(ErrorInternalResponseExample))]
        [HttpGet("listaCuestionario")]
        public async Task<IActionResult> listaCuestionario()
        {
            try
            {
                var response = await _cuestionarioS.listaCuestionario();

                if (!response.Any())
                    return NotFound("No se encontraron preguntas.");

                return Ok(response);
            }
            catch (Exception)
            {
                //return StatusCode(500, "Ocurrio un error, comuniquese con sistemas.");
                throw;
            }

        }


        /// <summary>
        /// Metodo para obtener los puestos de trabajo activos para el cuestionario
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Petición exitosa, retorna una lista con las preguntas y respuestas del formulario.</response>
        /// <response code="401">No tienes autorizacion para consumir este metodo.</response>
        /// <response code="404">No se encontraron preguntas.</response>
        /// <response code="500">Ocurrió un error en el servidor.</response>
        [ProducesResponseType(typeof(List<MPPuestosTrabajo>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerResponseExample(200, typeof(PuestoListaResponseExample))]
        [SwaggerResponseExample(404, typeof(CuestionarioListaErrorResponseExample))]
        [SwaggerResponseExample(500, typeof(ErrorInternalResponseExample))]
        [HttpGet("listaPuestos")]
        public async Task<IActionResult> listaPuestosTrabajo()
        {
            try
            {
                var response = await _cuestionarioS.listaPuestosTrabajo();

                if (!response.Any())
                    return NotFound();

                return Ok(response);
            }
            catch (Exception)
            {
                //return StatusCode(500, "Ocurrio un error, comuniquese con sistemas.");
                throw;
            }

        }

        /// <summary>
        /// Metodo para registrar las respuestas del formulario y obtener el resultado
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Petición exitosa, retorna el resultado del formulario.</response>
        /// <response code="400">Error de parámetros en la solicitud.</response>
        /// <response code="401">No tienes autorizacion para consumir este metodo.</response>
        /// <response code="404">El usuario ya se encuentra registrado.</response>
        /// <response code="500">Ocurrió un error en el servidor.</response>
        [ProducesResponseType(typeof(MResResultado), 200)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), 400)]
        [ProducesResponseType(typeof(MResResultado), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerRequestExample(typeof(MRCrearPostulacion), typeof(CrearPostulacionRequestExample))]
        [SwaggerResponseExample(200, typeof(ResultadoResponseExample))]
        [SwaggerResponseExample(400, typeof(ResultadoRequestResponseExample))]
        [SwaggerResponseExample(404, typeof(ResultadoErrorResponseExample))]
        [SwaggerResponseExample(500, typeof(ErrorInternalResponseExample))]
        [HttpPost("registrarCuestionario")]
        public async Task<IActionResult> registrarCuestionario([FromBody] MRCrearPostulacion request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _cuestionarioS.crearPostulacion(request);

                if (!response.codigoRespuesta.Equals(1))
                    return NotFound(response);

                return Ok(response);
            }
            catch (Exception)
            {
                //return StatusCode(500, "Ocurrio un error, comuniquese con sistemas.");
                throw;
            }

        }

    }
}
