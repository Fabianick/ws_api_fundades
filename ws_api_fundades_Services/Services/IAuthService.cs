using ws_api_fundades_Entity.Models.Procedure;
using ws_api_fundades_Entity.Models.Request;

namespace ws_api_fundades_Services.Services
{
    public interface IAuthService
    {
        Task<ModelProcedureResponse> login(ModelRequestAuth model);
    }
}
