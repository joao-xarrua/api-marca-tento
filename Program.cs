using MarcaTento.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    }) // adiciona os controladores
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // previne os ciclos de acontecer
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault; // não vai renderizar objetos nulos
    }); // altera serialização padrão do system.text.json
builder.Services.AddDbContext<MarcaTentoDataContext>(); // adiciona o contexto do banco de dados

var app = builder.Build();

app.MapControllers();

app.Run();
