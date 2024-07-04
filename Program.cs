using StudentStore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapStudentsEndpoints();

app.Run();
