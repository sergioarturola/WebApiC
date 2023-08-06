using Microsoft.EntityFrameworkCore;
using WebApi1.Data;

/*
 * RECUERDA QUE "Program.cs" ES EL ARCHIVO PRINCIPAL Y ES DONDE
 * ARRANCA EL PROGRAMA ADEMAS ES DONDE SE CONFIGURA EL PROGRAMA
 */

//objeto que vamos a configurar
var builder = WebApplication.CreateBuilder(args);

////ademas de agrgegar los controladores de la carpeta controllers agregamos el 
///NewtonsoftJson que instalamos desde nuget
builder.Services.AddControllers().AddNewtonsoftJson();

//le agregamos el servicio (libreria) de Swagger para los endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//agregando un SERVICIO: vamos a relacionar nuestra cadena de conexion (del archivo aspsettings.json) con el modelo
builder.Services.AddDbContext<AplicationDboContext>(option =>
{
    //le especificamos el motor de la base de datos, en este caso la de "sql server"
    //asi como le pasamos la cadena de conexion con ayuda del objeto builder
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//ya configurado el objeto builder ahora creamos nuestra "app"
var app = builder.Build();

// Configure the HTTP request pipeline.
//SI ESTAMOS EN MODO DESAROLLO ENTONCES:
if (app.Environment.IsDevelopment())
{
    //QUE USE SWAGGER
    app.UseSwagger();
    app.UseSwaggerUI();
}

//para que use HTTPS
app.UseHttpsRedirection();

//para que use autenticaciones
app.UseAuthorization();

//para mapear los controladores de la API
app.MapControllers();

app.Run();//con esto corre la app
