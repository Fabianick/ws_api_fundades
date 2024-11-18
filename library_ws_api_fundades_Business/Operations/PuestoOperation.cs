using System.Data.SqlClient;
using System.Data;
using ws_api_fundades_Connections.Connections;
using ws_api_fundades_Entity.Constants;
using ws_api_fundades_Entity.Models.Procedure;
using ws_api_fundades_Entity.Models.Response;
using ws_api_fundades_Utils.ConvertUtils;
using ws_api_fundades_Entity.Models.Request;

namespace ws_api_fundades_Business.Operations
{
    public class PuestoOperation
    {
        private readonly Conexion conexionBD = new Conexion();

        public async Task<ModelProcedureResponse> createPuesto(MRCreatePuesto model)
        {
            var response = new ModelProcedureResponse();

            try
            {
                using (SqlConnection cnx = new SqlConnection(conexionBD.ConexionBdUpc))
                {
                    using (SqlCommand cmd = new SqlCommand(ProcedureConst.SP_CREAR_PUESTO, cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = model.NombrePuesto;
                        cmd.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar)).Value = model.DescripcionPuesto;

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
                throw new Exception("Error en createPuesto: " + ex.Message, ex);
            }

            return response;
        }
        
        public async Task<ModelProcedureResponse> desactivarPuesto(string puesto)
        {
            var response = new ModelProcedureResponse();

            try
            {
                using (SqlConnection cnx = new SqlConnection(conexionBD.ConexionBdUpc))
                {
                    using (SqlCommand cmd = new SqlCommand(ProcedureConst.SP_DESACTIVAR_PUESTO, cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = puesto;

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
                throw new Exception("Error en desactivarPuesto: " + ex.Message, ex);
            }

            return response;
        }

        public async Task<List<MPPuestosTrabajo>> listaPuestosInactivos()
        {
            var response = new List<MPPuestosTrabajo>();

            try
            {
                using (SqlConnection cnx = new SqlConnection(conexionBD.ConexionBdUpc))
                {
                    using (SqlCommand cmd = new SqlCommand(ProcedureConst.SP_LIST_PUESTO_INACTIVO, cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        await cnx.OpenAsync();

                        using (SqlDataReader dtr = await cmd.ExecuteReaderAsync())
                        {
                            if (dtr.HasRows)
                            {
                                while (await dtr.ReadAsync())
                                {
                                    var result = new MPPuestosTrabajo
                                    {
                                        NombrePuesto = dtr.GetString(0),
                                        DescripcionPuesto = dtr.GetString(1)
                                    };
                                    response.Add(result);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en listaPuestosTrabajo: " + ex.Message, ex);
            }

            return response;
        }

        public async Task<ModelProcedureResponse> activarPuesto(string puesto)
        {
            var response = new ModelProcedureResponse();

            try
            {
                using (SqlConnection cnx = new SqlConnection(conexionBD.ConexionBdUpc))
                {
                    using (SqlCommand cmd = new SqlCommand(ProcedureConst.SP_ACTIVAR_PUESTO, cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = puesto;

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
                throw new Exception("Error en desactivarPuesto: " + ex.Message, ex);
            }

            return response;
        }

        public async Task<List<MPPostulantesxPuestos>> listaPostulantesxPuestos(string puesto)
        {
            var response = new List<MPPostulantesxPuestos>();

            try
            {
                using (SqlConnection cnx = new SqlConnection(conexionBD.ConexionBdUpc))
                {
                    using (SqlCommand cmd = new SqlCommand(ProcedureConst.SP_LIST_POSTULANTES_PUESTO, cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = puesto;

                        await cnx.OpenAsync();

                        using (SqlDataReader dtr = await cmd.ExecuteReaderAsync())
                        {
                            if (dtr.HasRows)
                            {
                                while (await dtr.ReadAsync())
                                {
                                    var result = new MPPostulantesxPuestos
                                    {
                                        usuario = dtr.GetString(0),
                                        resultdo = dtr.GetString(1),
                                        email = dtr.GetString(2)
                                    };
                                    response.Add(result);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en listaPostulantesxPuestos: " + ex.Message, ex);
            }

            return response;
        }
    }
}
