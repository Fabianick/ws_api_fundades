using System.Data;
using System.Data.SqlClient;
using ws_api_fundades_Connections.Connections;
using ws_api_fundades_Entity.Constants;
using ws_api_fundades_Entity.Models.Procedure;
using ws_api_fundades_Entity.Models.Request;

namespace ws_api_fundades_Business.Operations
{
    public class UserOperation
    {
        private readonly Conexion conexionBD = new Conexion();


        public async Task<ModelProcedureResponse> createUser(MRCreateUser model)
        {
            var response = new ModelProcedureResponse();

            try
            {
                using (SqlConnection cnx = new SqlConnection(conexionBD.ConexionBdUpc))
                {
                    using (SqlCommand cmd = new SqlCommand(ProcedureConst.SP_CREAR_USUARIO, cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@usuario", SqlDbType.VarChar)).Value = model.usuario;
                        cmd.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar)).Value = model.password;
                        cmd.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar)).Value = model.nombres;
                        cmd.Parameters.Add(new SqlParameter("@apellido", SqlDbType.VarChar)).Value = model.apellidos;
                        cmd.Parameters.Add(new SqlParameter("@correo", SqlDbType.VarChar)).Value = model.email ?? (object)DBNull.Value;
                        cmd.Parameters.Add(new SqlParameter("@ncelular", SqlDbType.VarChar)).Value = model.telefono ?? (object)DBNull.Value;
                        cmd.Parameters.Add(new SqlParameter("@rol", SqlDbType.Int)).Value = model.rol;

                        await cnx.OpenAsync();

                        using (SqlDataReader dtr = await cmd.ExecuteReaderAsync())
                        {
                            if (dtr.HasRows)
                            {
                                while (await dtr.ReadAsync())
                                {
                                    response = new ModelProcedureResponse
                                    {
                                        codigoRespuesta = dtr.GetInt32(0),
                                        mensajeRespuesta = dtr.GetString(1)
                                    };
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en createUser: " + ex.Message, ex);
            }

            return response;
        }

        
    }
}
