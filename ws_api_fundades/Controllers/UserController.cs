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
    /// Controlador para usuarios
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        /// Metodo para registar usuarios
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Petición exitosa, crea el usuario.</response>
        /// <response code="400">Error de parámetros en la solicitud.</response>
        /// <response code="404">El usuario ya se encuentra registrado.</response>
        /// <response code="500">Ocurrió un error en el servidor.</response>
        [ProducesResponseType(typeof(ModelProcedureResponse), 201)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), 400)]
        [ProducesResponseType(typeof(ModelProcedureResponse), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerRequestExample(typeof(MRCreateUser),typeof(CrearUsuarioRequestExample))]
        [SwaggerResponseExample(201, typeof(UserCreateResponseExample))]
        [SwaggerResponseExample(400, typeof(UserErrorRequestResponseExample))]
        [SwaggerResponseExample(404, typeof(UserErrorCreateResponseExample))]
        [SwaggerResponseExample(500, typeof(ErrorInternalResponseExample))]
        [HttpPost]
        [Route("create")]
        [AllowAnonymous]
        public async Task<IActionResult> createUser([FromBody] MRCreateUser model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _userService.createUser(model);

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
    }
}
