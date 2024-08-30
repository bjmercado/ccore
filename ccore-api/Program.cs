using ccore_api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapAuthorEndPoints();
app.MapBooksEndpoint();

app.Run();
