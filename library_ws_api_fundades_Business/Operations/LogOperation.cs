using System.Data;
using System.Data.SqlClient;
using ws_api_fundades_Connections.Connections;
using ws_api_fundades_Entity.Constants;
using ws_api_fundades_Entity.Models.Log;

namespace ws_api_fundades_Business.Operations
{
    public class LogOperation
    {
        private readonly Conexion conexionBD = new Conexion();

        public async Task<int> InsertApiLogAsync(LogInsertApiModel model)
        {
            int logId = 0;

            try
            {
                using (SqlConnection cnx = new SqlConnection(conexionBD.ConexionBdUpc))
                {
                    using (SqlCommand cmd = new SqlCommand(ProcedureConst.SP_LOG_API, cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@operation", SqlDbType.NVarChar)).Value = "insert";
                        cmd.Parameters.Add(new SqlParameter("@api_name", SqlDbType.VarChar)).Value = model.ApiName;
                        cmd.Parameters.Add(new SqlParameter("@api_endpoint", SqlDbType.VarChar)).Value = model.ApiEndpoint;
                        cmd.Parameters.Add(new SqlParameter("@json_request", SqlDbType.VarChar)).Value = model.JsonRequest;
                        cmd.Parameters.Add(new SqlParameter("@fecha_creacion", SqlDbType.DateTime)).Value = model.FechaCreacion;

                        var outputIdParam = new SqlParameter("@id_log", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        cmd.Parameters.Add(outputIdParam);

                        await cnx.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                        logId = (int)outputIdParam.Value;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return logId;
        }

        public async Task<bool> UpdateApiLogAsync(LogUpdateApiModel model)
        {
            bool isUpdated = false;
            try
            {

                using (SqlConnection cnx = new SqlConnection(conexionBD.ConexionBdUpc))
                {
                    using (SqlCommand cmd = new SqlCommand(ProcedureConst.SP_LOG_API, cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@operation", SqlDbType.NVarChar)).Value = "update";
                        cmd.Parameters.Add(new SqlParameter("@id_log", SqlDbType.Int)).Value = model.IdLog;
                        cmd.Parameters.Add(new SqlParameter("@json_response", SqlDbType.VarChar)).Value = model.JsonResponse;
                        cmd.Parameters.Add(new SqlParameter("@fecha_respuesta", SqlDbType.DateTime)).Value = model.FechaRespuesta;
                        cmd.Parameters.Add(new SqlParameter("@code_status_response", SqlDbType.Int)).Value = model.CodeStatusResponse;
                        cmd.Parameters.Add(new SqlParameter("@Error", SqlDbType.VarChar)).Value = model.Error ?? (Object)DBNull.Value;

                        await cnx.OpenAsync();
                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        isUpdated = rowsAffected > 0;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return isUpdated;
        }
    }
}
