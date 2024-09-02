using ccore_api.Data;
using ccore_api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepositories(builder.Configuration);

var app = builder.Build();

app.MapAuthorEndPoints();
app.MapBooksEndpoint();

await app.Services.InitializedDBAsync();

app.Run();
