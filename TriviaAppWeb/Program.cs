using TriviaAppBL.Interfaces;
using TriviaAppInfrastructure.ExternalServices;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment.EnvironmentName;
var allowedOrigin = environment == "Development"
    ? builder.Configuration["AllowedOrigins:Local"]
    : builder.Configuration["AllowedOrigins:Production"];


// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(allowedOrigin)
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddHttpClient<IServicio_APITrivia, Servicio_APITrivia>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//para hosting en produccion
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
