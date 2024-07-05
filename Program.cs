using StudentStore;
using StudentStore.Data;
using StudentStore.Endpoints;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

var connStr = builder.Configuration.GetConnectionString("StudentStore");

builder.Services.AddSqlite<StudentStoreContext>(connStr);

// Configure JSON options to handle cycles
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

app.MapStudentsEndpoints();

await app.MigrateDbAsync();

app.Run();
