using ws_api_fundades_Business.Operations;
using ws_api_fundades_Entity.Models.Procedure;
using ws_api_fundades_Entity.Models.Request;
using ws_api_fundades_Entity.Models.Response;
using ws_api_fundades_Services.Services;

namespace ws_api_fundades_Services.ServicesImpl
{
    public class CuestionarioServiceImpl : ICuestionarioService
    {
        private readonly CuestionarioOperation _cuestionarioO = new CuestionarioOperation();
        public async Task<List<MResponseCuestionario>> listaCuestionario()
        {
            return await _cuestionarioO.listaCuestionario();
        }

        public async Task<List<MPPuestosTrabajo>> listaPuestosTrabajo()
        {
            return await _cuestionarioO.listaPuestosTrabajo();
        }

        public async Task<MResResultado> crearPostulacion(MRCrearPostulacion model)
        {
            return await _cuestionarioO.crearPostulacion(model);
        }
    }
}
