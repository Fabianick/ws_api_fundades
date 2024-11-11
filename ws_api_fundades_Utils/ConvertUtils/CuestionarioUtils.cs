using Microsoft.Win32;
using ws_api_fundades_Entity.Models.Procedure;
using ws_api_fundades_Entity.Models.Response;

namespace ws_api_fundades_Utils.ConvertUtils
{
    public class CuestionarioUtils
    {
        public static List<MResponseCuestionario> convertCuestionario(List<MPListaCuestionario> listaCuestionario)
        {
            var lista = new List<MResponseCuestionario>();

            var cuestionariosAgrupados = listaCuestionario.GroupBy(x => x.id);

            foreach(var cuestionarioGroup in cuestionariosAgrupados)
            {
                var first = cuestionarioGroup.FirstOrDefault();

                var response = new MResponseCuestionario
                {
                    id = cuestionarioGroup.Key,
                    pregunta = first?.pregunta ?? "",
                    tipo = first?.tipo ?? "",
                    preguntaDescripcion = first?.definicion ?? "",
                    respuesta = new List<MRCRespuesta>()
                };


                foreach (var registro in cuestionarioGroup)
                {
                    var respuestaCuestioanrio = new MRCRespuesta
                    {
                        nroRespuesta = registro.nroRespuesta,
                        respuestaDescripcion = registro.descripcion
                    };
                    response.respuesta.Add(respuestaCuestioanrio);


                }

                lista.Add(response);
            }

            return lista;
        }
    }
}
