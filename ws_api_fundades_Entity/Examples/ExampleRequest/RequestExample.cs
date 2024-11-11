
using ws_api_fundades_Entity.Models.Request;
using static ws_api_fundades_Entity.Examples.EjemplosSwagger;

namespace ws_api_fundades_Entity.Examples.ExampleRequest
{
    public class RequestExample
    {
        public class AuthRequestExample : RequestSwagger<ModelRequestAuth>
        {

            public override ModelRequestAuth GetExamples()
            {
                return new ModelRequestAuth
                {
                    Username = "usuario",
                    Password = "contraseña"
                };
            }
        }
        
        public class CrearPostulacionRequestExample : RequestSwagger<MRCrearPostulacion>
        {

            public override MRCrearPostulacion GetExamples()
            {
                return new MRCrearPostulacion
                {
                    usuario = "usuario",
                    fechaPostulacion = DateTime.Now,
                    puesto = "Psicólogo de Desarrollo Infantil",
                    respuestas = new List<MRRespuestas>
                    {
                        new MRRespuestas
                        {
                            pregunta = "1. Visión",
                            tipoPregunta = "Trabajador",
                            respuesta = 4
                        },
                        new MRRespuestas
                        {
                            pregunta = "2. Audición",
                            tipoPregunta = "Trabajador",
                            respuesta = 3
                        }
                    }
                };
            }
        }
        
        public class CrearUsuarioRequestExample : RequestSwagger<MRCreateUser>
        {

            public override MRCreateUser GetExamples()
            {
                return new MRCreateUser
                {
                    usuario = "usuario",
                    password = "contraseña",
                    nombres = "testing",
                    apellidos = "testeo",
                    email = "testing@gmail.com",
                    telefono = "999999999",
                    rol = 1
                };
            }
        }
    }
}
