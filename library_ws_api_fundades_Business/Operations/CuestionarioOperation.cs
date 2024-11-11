using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using ws_api_fundades_Connections.Connections;
using ws_api_fundades_Entity.Constants;
using ws_api_fundades_Entity.Models.Procedure;
using ws_api_fundades_Entity.Models.Request;
using ws_api_fundades_Entity.Models.Response;
using ws_api_fundades_Utils.ConvertUtils;

namespace ws_api_fundades_Business.Operations
{
    public class CuestionarioOperation
    {
        private readonly Conexion conexionBD = new Conexion();

        public async Task<List<MResponseCuestionario>> listaCuestionario()
        {
            var response = new List<MPListaCuestionario>();

            try
            {
                using (SqlConnection cnx = new SqlConnection(conexionBD.ConexionBdUpc))
                {
                    using(SqlCommand cmd = new SqlCommand(ProcedureConst.SP_LIST_CUESTIONARIO, cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        await cnx.OpenAsync();
                        
                        using (SqlDataReader dtr = await cmd.ExecuteReaderAsync())
                        {
                            if (dtr.HasRows)
                            {
                                while (await dtr.ReadAsync())
                                {
                                    var result = new MPListaCuestionario
                                    {
                                        id = dtr.GetInt32(0),
                                        pregunta = dtr.GetString(1),
                                        tipo = dtr.GetString(2),
                                        definicion = dtr.GetString(3),
                                        nroRespuesta = dtr.GetInt32(4),
                                        descripcion = dtr.GetString(5)
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
                throw new Exception("Error en listaCuestionario: " + ex.Message, ex);
            }
            return CuestionarioUtils.convertCuestionario(response);
        }
        
        public async Task<List<MPPuestosTrabajo>> listaPuestosTrabajo()
        {
            var response = new List<MPPuestosTrabajo>();

            try
            {
                using (SqlConnection cnx = new SqlConnection(conexionBD.ConexionBdUpc))
                {
                    using (SqlCommand cmd = new SqlCommand(ProcedureConst.SP_LIST_PUESTOTRABAJO, cnx) { CommandType = CommandType.StoredProcedure })
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

        public async Task<MResResultado> crearPostulacion(MRCrearPostulacion model)
        {
            var response = new MResResultado();
            try
            {
                using (SqlConnection cnx = new SqlConnection(conexionBD.ConexionBdUpc))
                {
                    await cnx.OpenAsync();

                    var postulacion = await registarPostulacion(cnx,model);
                    if (!postulacion.codigoRespuesta.Equals(1))
                        return response;

                    var resultado = await registarResultado(cnx,postulacion.postulacionId);
                    if (!resultado.codigoRespuesta.Equals(1))
                        return response;

                    response = new MResResultado
                    {
                        codigoRespuesta = resultado.codigoRespuesta,
                        resultado = resultado.mensajeRespuesta
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en crearPostulacion: " + ex.Message, ex);
            }
            return response;
        }
        private  async Task<MPRegistrarPostulacion> registarPostulacion(SqlConnection cnx,MRCrearPostulacion model)
        {
            var response = new MPRegistrarPostulacion();
            try
            {
                using (SqlCommand cmd = new SqlCommand(ProcedureConst.SP_CREAR_POSTULACION, cnx) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@Table", SqlDbType.Structured)).Value = TableConverter.ConvertToDataTable(model);

                    using (SqlDataReader dtr = await cmd.ExecuteReaderAsync())
                    {
                        if (dtr.HasRows)
                        {
                            while (await dtr.ReadAsync())
                            {
                                response = new MPRegistrarPostulacion
                                {
                                    codigoRespuesta = dtr.GetInt32(0),
                                    mensajeRespuesta = dtr.GetString(1),
                                    postulacionId = dtr.GetInt32(2)
                                };
                            }
                        }
                    }

                }
            }
            catch(Exception)
            {
                throw;
            }
            return response;
        }
        
        private  async Task<ModelProcedureResponse> registarResultado(SqlConnection cnx,long postulacionId)
        {
            var response = new ModelProcedureResponse();
            try
            {
                using (SqlCommand cmd = new SqlCommand(ProcedureConst.SP_EVALUAR_POSTULACION, cnx) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@postulacion_id", SqlDbType.Int)).Value = postulacionId;

                    var outputResultado = new SqlParameter("@Resultado", SqlDbType.NVarChar,50) { Direction = ParameterDirection.Output };
                    var outputCodigo = new SqlParameter("@CodigoRespuesta", SqlDbType.Int) { Direction = ParameterDirection.Output };

                    cmd.Parameters.Add(outputResultado);
                    cmd.Parameters.Add(outputCodigo);

                    await cmd.ExecuteReaderAsync();

                    response = new ModelProcedureResponse
                    {
                        codigoRespuesta = (int)outputCodigo.Value,
                        mensajeRespuesta = (string)outputResultado.Value
                    };

                }
            }
            catch(Exception)
            {
                throw;
            }
            return response;
        }
    }
}
