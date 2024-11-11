using ws_api_fundades_Business.Operations;
using ws_api_fundades_Entity.Models.Procedure;
using ws_api_fundades_Entity.Models.Request;
using ws_api_fundades_Services.Services;

namespace ws_api_fundades_Services.ServicesImpl
{
    public class UserServiceImpl : IUserService
    {
        private readonly UserOperation _userO = new UserOperation();
        public async Task<ModelProcedureResponse> createUser(MRCreateUser model)
        {
            return await _userO.createUser(model);
        }
    }
}
