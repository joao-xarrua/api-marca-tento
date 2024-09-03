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
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault; // n�o vai renderizar objetos nulos
    }); // altera serializa��o padr�o do system.text.json

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
    options.AddPolicy(name: MyAllowSpecificOrigins,
    policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://www.apirequest.io")
            // essas duas linhas s�o para poder fazer metodos al�m do GET
            .AllowAnyHeader() 
            .AllowAnyMethod();
    }
));


builder.Services.AddDbContext<MarcaTentoDataContext>(); // adiciona o contexto do banco de dados

var app = builder.Build();

// precisa adicionar esse bol�o de coisa at� o "UseAuthorization" para remover o CORS e permitir algumas fontes
//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
