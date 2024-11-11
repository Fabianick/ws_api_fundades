using ws_api_fundades_Entity.Models.Procedure;
using ws_api_fundades_Entity.Models.Request;
using ws_api_fundades_Entity.Models.Response;

namespace ws_api_fundades_Services.Services
{
    public interface ICuestionarioService
    {
        Task<List<MResponseCuestionario>> listaCuestionario();

        Task<List<MPPuestosTrabajo>> listaPuestosTrabajo();
        Task<MResResultado> crearPostulacion(MRCrearPostulacion model);
    }
}
