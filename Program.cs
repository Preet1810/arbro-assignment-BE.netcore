using StudentStore;
using StudentStore.Data;
using StudentStore.Endpoints;


var builder = WebApplication.CreateBuilder(args);

var connStr = builder.Configuration.GetConnectionString("StudentStore");

builder.Services.AddSqlite<StudentStoreContext>(connStr);

var app = builder.Build();

app.MapStudentsEndpoints();

app.MigrateDb();

app.Run();
