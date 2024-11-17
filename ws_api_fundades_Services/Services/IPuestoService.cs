using ws_api_fundades_Entity.Models.Procedure;
using ws_api_fundades_Entity.Models.Request;

namespace ws_api_fundades_Services.Services
{
    public interface IPuestoService
    {
        Task<ModelProcedureResponse> createPuesto(MRCreatePuesto model);
        Task<ModelProcedureResponse> desactivarPuesto(string puesto);
        Task<List<MPPuestosTrabajo>> listaPuestosInactivos();
        Task<ModelProcedureResponse> activarPuesto(string puesto);
        Task<List<MPPostulantesxPuestos>> listaPostulantesxPuestos(string puesto);
    }
}
