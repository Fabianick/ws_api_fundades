using System.Data.SqlClient;
using System.Data;
using ws_api_fundades_Connections.Connections;
using ws_api_fundades_Entity.Constants;
using ws_api_fundades_Entity.Models.Procedure;
using ws_api_fundades_Entity.Models.Request;

namespace ws_api_fundades_Business.Operations
{
    public class AuthOperation
    {
        private readonly Conexion conexionBD = new Conexion();

        public async Task<MPAuthResponse> login(ModelRequestAuth model)
        {
            var response = new MPAuthResponse();

            try
            {
                using (SqlConnection cnx = new SqlConnection(conexionBD.ConexionBdUpc))
                {
                    using (SqlCommand cmd = new SqlCommand(ProcedureConst.SP_LOGIN_USUARIO, cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@usuario", SqlDbType.VarChar)).Value = model.Username;
                        cmd.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar)).Value = model.Password;

                        await cnx.OpenAsync();

                        using (SqlDataReader dtr = await cmd.ExecuteReaderAsync())
                        {
                            if (dtr.HasRows)
                            {
                                while (await dtr.ReadAsync())
                                {
                                    response = new MPAuthResponse
                                    {
                                        codigoRespuesta = dtr.GetInt32(0),
                                        mensajeRespuesta = dtr.GetString(1)      ,
                                        Rol = dtr.GetString(2)
                                    };
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en login: " + ex.Message, ex);
            }

            return response;
        }
    }
}
