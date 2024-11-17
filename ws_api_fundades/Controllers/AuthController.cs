using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ws_api_fundades_Entity.Models.Procedure;
using ws_api_fundades_Entity.Models.Request;
using ws_api_fundades_Entity.Models.Response;
using ws_api_fundades_Services.Services;
using static ws_api_fundades_Entity.Examples.ExampleRequest.RequestExample;
using static ws_api_fundades_Entity.Examples.ExampleResponse.ResponseExample;

namespace ws_api_fundades.Controllers
{
    /// <summary>
    /// Controller para manejar la autenticación.
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuthService _authS;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config"></param>
        /// <param name="authS"></param>
        public AuthController(IConfiguration config, IAuthService authS)
        {
            _config = config;
            _authS = authS;
        }

        /// <summary>
        /// Metodo para Logearte
        /// </summary>
        /// <param name="authRequest"></param>
        /// <returns></returns>
        /// <response code="200">Petición exitosa, retorna el token.</response>
        /// <response code="400">Error de parámetros en la solicitud.</response>
        /// <response code="404">El usuario no fue encontrado o la contraseña es incorrecta.</response>
        /// <response code="500">Ocurrió un error en el servidor.</response>
        [ProducesResponseType(typeof(ModelResponseAuth),200)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>),400)]
        [ProducesResponseType(typeof(ModelProcedureResponse),404)]
        [ProducesResponseType(typeof(string),500)]
        [SwaggerRequestExample(typeof(ModelRequestAuth),typeof(AuthRequestExample))]
        [SwaggerResponseExample(200,typeof(AuthResponseExample))]
        [SwaggerResponseExample(400,typeof(AuthErrorRequestResponseExample))]
        [SwaggerResponseExample(404,typeof(AuthErrorResponseExample))]
        [SwaggerResponseExample(500,typeof(ErrorInternalResponseExample))]
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] ModelRequestAuth authRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _authS.login(authRequest);

                if (!response.codigoRespuesta.Equals(1))
                    return NotFound(response);


                var token = GenerateJwtToken(authRequest.Username);
                return Ok(new ModelResponseAuth
                {
                    Token = token,
                    Rol = response.Rol ?? ""
                });
            }
            catch (Exception)
            {
                //return StatusCode(500, "Ocurrio un error, comuniquese con sistemas.");
                throw;
            }


        }

        private string GenerateJwtToken(string username)//, string role)
        {
            var jwtSettings = _config.GetSection("JwtSettings");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(ClaimTypes.Role, role) // Agregar el rol al token
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
