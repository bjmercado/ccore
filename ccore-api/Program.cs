using System.Text.Json.Serialization;
using ccore_api.Data;
using ccore_api.Endpoints;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

//Configure JSON options to handle circular references
builder.Services.Configure<JsonOptions>(options => {
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddRepositories(builder.Configuration);

var app = builder.Build();

app.MapAuthorEndPoints();
app.MapBooksEndpoint();

await app.Services.InitializedDBAsync();

app.Run();
