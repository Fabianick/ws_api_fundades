using ws_api_fundades_Entity.Models.Log;

namespace ws_api_fundades_Services.Services
{
    public interface ILogService
    {
        Task<int> InsertApiLogAsync(LogInsertApiModel model);
        Task<bool> UpdateApiLogAsync(LogUpdateApiModel model);
    }
}
