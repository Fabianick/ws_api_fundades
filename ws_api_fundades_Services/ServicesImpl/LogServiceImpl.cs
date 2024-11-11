using ws_api_fundades_Business.Operations;
using ws_api_fundades_Entity.Models.Log;
using ws_api_fundades_Services.Services;

namespace ws_api_fundades_Services.ServicesImpl
{
    public class LogServiceImpl : ILogService
    {
        private readonly LogOperation _logO = new LogOperation();

        public async Task<int> InsertApiLogAsync(LogInsertApiModel model)
        {
            return await _logO.InsertApiLogAsync(model);
        }

        public async Task<bool> UpdateApiLogAsync(LogUpdateApiModel model)
        {
            return await _logO.UpdateApiLogAsync(model);
        }
    }
}
