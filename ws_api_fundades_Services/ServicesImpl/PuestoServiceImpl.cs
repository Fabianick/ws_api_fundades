using System.Reflection;
using ws_api_fundades_Business.Operations;
using ws_api_fundades_Entity.Models.Procedure;
using ws_api_fundades_Entity.Models.Request;
using ws_api_fundades_Services.Services;

namespace ws_api_fundades_Services.ServicesImpl
{
    public class PuestoServiceImpl : IPuestoService
    {
        private readonly PuestoOperation _puestoO = new PuestoOperation();
        public async Task<ModelProcedureResponse> createPuesto(MRCreatePuesto model)
        {
            return await _puestoO.createPuesto(model);
        }

        public async Task<ModelProcedureResponse> desactivarPuesto(string puesto)
        {
            return await _puestoO.desactivarPuesto(puesto);
        }

        public async Task<List<MPPuestosTrabajo>> listaPuestosInactivos()
        {
            return await _puestoO.listaPuestosInactivos();
        }

        public async Task<ModelProcedureResponse> activarPuesto(string puesto)
        {
            return await _puestoO.activarPuesto(puesto);
        }

        public async Task<List<MPPostulantesxPuestos>> listaPostulantesxPuestos(string puesto)
        {
            return await _puestoO.listaPostulantesxPuestos(puesto);
        }
    }
}
