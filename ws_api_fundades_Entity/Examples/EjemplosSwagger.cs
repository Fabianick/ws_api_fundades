using Swashbuckle.AspNetCore.Filters;


namespace ws_api_fundades_Entity.Examples
{
    public class EjemplosSwagger
    {
        public class RequestSwagger<T> : IExamplesProvider<T>
        {
            #nullable disable
            public virtual T GetExamples()
            {
                return default(T);
            }
            #nullable enable
        }

        public class ResponseSwagger<T> : IExamplesProvider<T>
        {
            #nullable disable
            public virtual T GetExamples()
            {
                return default(T);
            }
            #nullable enable
        }
    }
}
