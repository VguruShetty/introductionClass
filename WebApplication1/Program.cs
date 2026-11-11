var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var a = string.empty;

app.MapGet("/", () => "Hello World!");

app.Run();
