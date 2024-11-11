using ws_api_fundades_Entity.Models.Procedure;
using ws_api_fundades_Entity.Models.Request;

namespace ws_api_fundades_Services.Services
{
    public interface IUserService
    {
        Task<ModelProcedureResponse> createUser(MRCreateUser model);
    }
}
