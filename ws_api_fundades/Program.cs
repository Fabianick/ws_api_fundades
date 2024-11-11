using ws_api_fundades.Middleware;
using ws_api_fundades.Extensions;

var builder = WebApplication.CreateBuilder(args);

//CAPA UTILS
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.AddBusiness();


var app = builder.Build();

//CAPA UTILS
app.ConfigureMiddleware(app.Environment);

// Configuración de middleware
app.UseMiddleware<ApiLogsMiddleware>();


app.MapControllers();

app.Run();
