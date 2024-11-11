using ws_api_fundades_Entity.Models.Procedure;
using ws_api_fundades_Entity.Models.Response;
using static ws_api_fundades_Entity.Examples.EjemplosSwagger;

namespace ws_api_fundades_Entity.Examples.ExampleResponse
{
    public class ResponseExample
    {
        public class AuthResponseExample : ResponseSwagger<ModelResponseAuth>
        {
            public override ModelResponseAuth GetExamples()
            {
                return new ModelResponseAuth
                {
                    Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." 
                };
            }
        }
        
        public class ErrorInternalResponseExample : ResponseSwagger<object>
        {
            public override object GetExamples()
            {
                return new 
                {
                    message = "Ocurrió un error en el servidor. Comuníquese con sistemas."
                };
            }
        }

        public class AuthErrorRequestResponseExample : ResponseSwagger<Dictionary<string, string[]>>
        {
            public override Dictionary<string, string[]> GetExamples()
            {
                return new Dictionary<string, string[]>
                {
                    { "Username", new[] { "El campo Username es requerido." } },
                    { "Password", new[] { "El campo Password es requerido." } }
                };
            }
        }

        public class AuthErrorResponseExample : ResponseSwagger<ModelProcedureResponse>
        {
            public override ModelProcedureResponse GetExamples()
            {
                return new ModelProcedureResponse
                {
                    codigoRespuesta = 0,
                    mensajeRespuesta = "El usuario no esta registrado."
                };
            }
        }

        public class UserCreateResponseExample : ResponseSwagger<ModelProcedureResponse>
        {
            public override ModelProcedureResponse GetExamples()
            {
                return new ModelProcedureResponse
                {
                    codigoRespuesta = 1,
                    mensajeRespuesta = "Usuario fue creado safistactoriamente."
                };
            }
        }

        public class UserErrorRequestResponseExample : ResponseSwagger<Dictionary<string, string[]>>
        {
            public override Dictionary<string, string[]> GetExamples()
            {
                return new Dictionary<string, string[]>
                {
                    { "usuario", new[] { "El campo usuario es requerido." } },
                    { "email", new[] { "El campo email es requerido." } }
                };
            }
        }

        public class UserErrorCreateResponseExample : ResponseSwagger<ModelProcedureResponse>
        {
            public override ModelProcedureResponse GetExamples()
            {
                return new ModelProcedureResponse
                {
                    codigoRespuesta = 0,
                    mensajeRespuesta = "El usuario ya existe."
                };
            }
        }

        public class CuestionarioListaResponseExample : ResponseSwagger<List<MResponseCuestionario>>
        {
            public override List<MResponseCuestionario> GetExamples()
            {
                return new List<MResponseCuestionario>
                {
                    new MResponseCuestionario
                    {
                        id = 1,
                        pregunta = "¿Cómo calificarías tu visión?",
                        tipo = "Trabajador",
                        preguntaDescripcion = "Pregunta sobre la calidad de visión del trabajador.",
                        respuesta = new List<MRCRespuesta>
                        {
                            new MRCRespuesta { nroRespuesta = 1, respuestaDescripcion = "Buena" },
                            new MRCRespuesta { nroRespuesta = 2, respuestaDescripcion = "Regular" },
                            new MRCRespuesta { nroRespuesta = 3, respuestaDescripcion = "Mala" }
                        }
                    },
                    new MResponseCuestionario
                    {
                        id = 2,
                        pregunta = "¿Cómo calificarías tu audición?",
                        tipo = "Trabajador",
                        preguntaDescripcion = "Pregunta sobre la calidad de audición del trabajador.",
                        respuesta = new List<MRCRespuesta>
                        {
                            new MRCRespuesta { nroRespuesta = 1, respuestaDescripcion = "Excelente" },
                            new MRCRespuesta { nroRespuesta = 2, respuestaDescripcion = "Moderada" },
                            new MRCRespuesta { nroRespuesta = 3, respuestaDescripcion = "Pobre" }
                        }
                    }
                };
            }
        }

        public class CuestionarioListaErrorResponseExample : ResponseSwagger<string>
        {
            public override string GetExamples()
            {
                return "No se encontraron preguntas.";
            }
        }

        public class PuestoListaResponseExample : ResponseSwagger<List<MPPuestosTrabajo>>
        {
            public override List<MPPuestosTrabajo> GetExamples()
            {
                return new List<MPPuestosTrabajo>
                {
                    new MPPuestosTrabajo
                    {
                        NombrePuesto = "Developer",
                        DescripcionPuesto = "Encargado de desarrollar los aplicativos internos de la empresa."
                    },
                    new MPPuestosTrabajo
                    {
                        NombrePuesto = "Designer",
                        DescripcionPuesto = "Encargado de diseñar mockups y mejorar la experiencia de usuario."
                    }
                };
            }
        }

        public class PuestoListaErrorResponseExample : ResponseSwagger<string>
        {
            public override string GetExamples()
            {
                return "No se encontraron puestos.";
            }
        }

        public class ResultadoResponseExample : ResponseSwagger<MResResultado>
        {
            public override MResResultado GetExamples()
            {
                return new MResResultado
                {
                    codigoRespuesta = 1,
                    resultado = "Aprobado."
                };
            }
        }

        public class ResultadoRequestResponseExample : ResponseSwagger<Dictionary<string, string[]>>
        {
            public override Dictionary<string, string[]> GetExamples()
            {
                return new Dictionary<string, string[]>
                {
                    { "usuario", new[] { "El campo usuario es requerido." } },
                    { "puesto", new[] { "El campo puesto es requerido." } }
                };
            }
        }

        public class ResultadoErrorResponseExample : ResponseSwagger<MResResultado>
        {
            public override MResResultado GetExamples()
            {
                return new MResResultado
                {
                    codigoRespuesta = 0,
                    resultado = "Error al procesar el resultado."
                };
            }
        }
    }
}
