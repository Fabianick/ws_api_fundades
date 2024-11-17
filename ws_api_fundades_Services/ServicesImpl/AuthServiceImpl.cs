using ws_api_fundades_Business.Operations;
using ws_api_fundades_Entity.Models.Procedure;
using ws_api_fundades_Entity.Models.Request;
using ws_api_fundades_Services.Services;

namespace ws_api_fundades_Services.ServicesImpl
{
    public class AuthServiceImpl : IAuthService
    {
        private readonly AuthOperation _authO = new AuthOperation();

        public async Task<MPAuthResponse> login(ModelRequestAuth model)
        {
            return await _authO.login(model);
        }
    }
}
