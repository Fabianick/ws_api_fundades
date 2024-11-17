using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using ws_api_fundades_Entity.Models.Procedure;
using ws_api_fundades_Entity.Models.Request;
using ws_api_fundades_Services.Services;
using static ws_api_fundades_Entity.Examples.ExampleRequest.RequestExample;
using static ws_api_fundades_Entity.Examples.ExampleResponse.ResponseExample;

namespace ws_api_fundades.Controllers
{
    /// <summary>
    /// Controlador para puestos de trabajo
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class PuestoController : ControllerBase
    {
        private readonly IPuestoService _puestoS;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="puestoS"></param>
        public PuestoController(IPuestoService puestoS)
        {
            _puestoS = puestoS;
        }

        /// <summary>
        /// Metodo para registar puestos de trabajo
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Petición exitosa, crea el puesto.</response>
        /// <response code="400">Error de parámetros en la solicitud.</response>
        /// <response code="401">No tienes autorizacion para consumir este metodo.</response>
        /// <response code="404">El puesto ya se encuentra registrado.</response>
        /// <response code="500">Ocurrió un error en el servidor.</response>
        [ProducesResponseType(typeof(ModelProcedureResponse), 201)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), 400)]
        [ProducesResponseType(typeof(ModelProcedureResponse), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerRequestExample(typeof(MRCreatePuesto), typeof(CrearPuestoRequestExample))]
        [SwaggerResponseExample(201, typeof(PuestoCreateResponseExample))]
        [SwaggerResponseExample(400, typeof(PuestoErrorRequestResponseExample))]
        [SwaggerResponseExample(404, typeof(PuestoErrorCreateResponseExample))]
        [SwaggerResponseExample(500, typeof(ErrorInternalResponseExample))]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> createPuesto([FromBody] MRCreatePuesto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _puestoS.createPuesto(model);

                if (!response.codigoRespuesta.Equals(1))
                {
                    return NotFound(response);
                }

                return StatusCode(201, response);
            }
            catch (Exception)
            {
                //return StatusCode(500, "Ocurrio un error, comuniquese con sistemas.");
                throw;
            }

        }

        /// <summary>
        /// Metodo para desactivar puestos de trabajo
        /// </summary>
        /// <param name="puesto"></param>
        /// <returns></returns>
        /// <response code="200">Petición exitosa, puesto desactivado.</response>
        /// <response code="400">Error de parámetros en la solicitud.</response>
        /// <response code="401">No tienes autorizacion para consumir este metodo.</response>
        /// <response code="404">El puesto ya se encuentra desactivado.</response>
        /// <response code="500">Ocurrió un error en el servidor.</response>
        [ProducesResponseType(typeof(ModelProcedureResponse), 200)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), 400)]
        [ProducesResponseType(typeof(ModelProcedureResponse), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerResponseExample(200, typeof(PuestoDesactivadoResponseExample))]
        [SwaggerResponseExample(400, typeof(PuestoDesactivadoErrorRequestResponseExample))]
        [SwaggerResponseExample(404, typeof(PuestoDesactivadoErrorCreateResponseExample))]
        [SwaggerResponseExample(500, typeof(ErrorInternalResponseExample))]
        [HttpPost]
        [Route("desactivar")]
        public async Task<IActionResult> desactivarPuesto([FromQuery] string puesto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _puestoS.desactivarPuesto(puesto);

                if (!response.codigoRespuesta.Equals(1))
                {
                    return NotFound(response);
                }

                return StatusCode(200, response);
            }
            catch (Exception)
            {
                //return StatusCode(500, "Ocurrio un error, comuniquese con sistemas.");
                throw;
            }

        }

        /// <summary>
        /// Metodo para obtener la lista de puestos inactivos
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Petición exitosa, lista d puestos inactivos.</response>
        /// <response code="401">No tienes autorizacion para consumir este metodo.</response>
        /// <response code="404">La lista de puestos inactivos fue erronea.</response>
        /// <response code="500">Ocurrió un error en el servidor.</response>
        [ProducesResponseType(typeof(List<MPPuestosTrabajo>), 200)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), 400)]
        [ProducesResponseType(typeof(List<MPPuestosTrabajo>), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerResponseExample(200, typeof(PuestoListaInactivosResponseExample))]
        [SwaggerResponseExample(404, typeof(PuestoListInactivoErrorCreateResponseExample))]
        [SwaggerResponseExample(500, typeof(ErrorInternalResponseExample))]
        [HttpPost]
        [Route("listaInactivos")]
        public async Task<IActionResult> listaPuestosInactivos()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _puestoS.listaPuestosInactivos();

                if (response is null)
                {
                    return NotFound(response);
                }

                return StatusCode(200, response);
            }
            catch (Exception)
            {
                //return StatusCode(500, "Ocurrio un error, comuniquese con sistemas.");
                throw;
            }

        }

        /// <summary>
        /// Metodo para activar puestos de trabajo
        /// </summary>
        /// <param name="puesto"></param>
        /// <returns></returns>
        /// <response code="200">Petición exitosa, puesto activado.</response>
        /// <response code="400">Error de parámetros en la solicitud.</response>
        /// <response code="401">No tienes autorizacion para consumir este metodo.</response>
        /// <response code="404">El puesto ya se encuentra activado.</response>
        /// <response code="500">Ocurrió un error en el servidor.</response>
        [ProducesResponseType(typeof(ModelProcedureResponse), 200)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), 400)]
        [ProducesResponseType(typeof(ModelProcedureResponse), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerResponseExample(200, typeof(PuestoActivadoResponseExample))]
        [SwaggerResponseExample(400, typeof(PuestoActivadoErrorRequestResponseExample))]
        [SwaggerResponseExample(404, typeof(PuestoActivadoErrorCreateResponseExample))]
        [SwaggerResponseExample(500, typeof(ErrorInternalResponseExample))]
        [HttpPost]
        [Route("activar")]
        public async Task<IActionResult> activarPuesto([FromQuery] string puesto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _puestoS.activarPuesto(puesto);

                if (!response.codigoRespuesta.Equals(1))
                {
                    return NotFound(response);
                }

                return StatusCode(200, response);
            }
            catch (Exception)
            {
                //return StatusCode(500, "Ocurrio un error, comuniquese con sistemas.");
                throw;
            }

        }

        /// <summary>
        /// Metodo para listar a los postulantes según el puesto
        /// </summary>
        /// <param name="puesto"></param>
        /// <returns></returns>
        /// <response code="200">Petición exitosa, lista encontrada.</response>
        /// <response code="400">Error de parámetros en la solicitud.</response>
        /// <response code="401">No tienes autorizacion para consumir este metodo.</response>
        /// <response code="404">El listado sufrio un error.</response>
        /// <response code="500">Ocurrió un error en el servidor.</response>
        [ProducesResponseType(typeof(List<MPPostulantesxPuestos>), 200)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), 400)]
        [ProducesResponseType(typeof(List<MPPostulantesxPuestos>), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerResponseExample(200, typeof(PuestoListaPostulantesResponseExample))]
        [SwaggerResponseExample(404, typeof(PuestoListPostulantesErrorCreateResponseExample))]
        [SwaggerResponseExample(500, typeof(ErrorInternalResponseExample))]
        [HttpPost]
        [Route("listaPostulantesbyPuesto")]
        public async Task<IActionResult> listaPostulantesxPuestos([FromQuery] string puesto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _puestoS.listaPostulantesxPuestos(puesto);

                if (response is null)
                {
                    return NotFound(response);
                }

                return StatusCode(200, response);
            }
            catch (Exception)
            {
                //return StatusCode(500, "Ocurrio un error, comuniquese con sistemas.");
                throw;
            }

        }
    }
}
