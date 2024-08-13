using ccore_api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapUserEndPoints();

app.Run();
