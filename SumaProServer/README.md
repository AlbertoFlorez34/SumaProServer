Esta es una API robusta construida con **.NET 8** que permite realizar operaciones de suma, devolviendo el resultado y persistiendo cada cálculo en una base de datos **SQL Server**.

## 🚀 Características
* **Endpoint de Suma:** Recibe parámetros numéricos y genera el resultado.
* **Persistencia:** Uso de **Entity Framework Core** para guardar el historial.
* **Base de Datos:** Configurado para SQL Server (LocalDB).
* **Documentación:** Interfaz de Swagger integrada para pruebas rápidas.

## 🛠️ Tecnologías utilizadas
* ASP.NET Core Web API
* Entity Framework Core (SQL Server)
* Swagger / OpenAPI

## 📋 Estructura del Proyecto
```text
SumaProServer/
├── Controllers/     # Lógica de los Endpoints
├── Data/            # Contexto de Base de Datos y Configuración de EF
├── Models/          # Entidades (Tablas) de la Base de Datos
├── Program.cs       # Configuración y arranque de la aplicación
└── appsettings.json # Cadena de conexión a SQL Server


Elige ASP.NET Core Web API.

Nombre del proyecto: SumaProServer

instalar enity
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer

Microsoft.EntityFrameworkCore.Tools


2. crear el modelo

Models/Suma.cs (La tabla)
Crea una carpeta Models y añade esta clase:

C#

namespace SumaProServer.Models;

public class Suma
{
    public int Id { get; set; }
    public double Numero1 { get; set; }
    public double Numero2 { get; set; }
    public double Resultado { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
}


3.crear la conexion

Data/AppDbContext.cs (La conexión)
Crea una carpeta Data y añade esta clase:

C#

using Microsoft.EntityFrameworkCore;
using SumaProServer.Models;

namespace SumaProServer.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Suma> Sumas => Set<Suma>();
}


4. crear el controlador
Controllers/CalculadoraController.cs (El Endpoint)
En la carpeta Controllers, crea este archivo:

C#

using Microsoft.AspNetCore.Mvc;
using SumaProServer.Data;
using SumaProServer.Models;

namespace SumaProServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalculadoraController : ControllerBase
{
    private readonly AppDbContext _context;

    public CalculadoraController(AppDbContext context) => _context = context;

    [HttpPost("sumar")]
    public async Task<IActionResult> PostSuma(double n1, double n2)
    {
        var nuevaSuma = new Suma { Numero1 = n1, Numero2 = n2, Resultado = n1 + n2 };
        _context.Sumas.Add(nuevaSuma);
        await _context.SaveChangesAsync();
        return Ok(nuevaSuma);
    }
}

5. Configurar el SQL Server (appsettings.json)
Abre el archivo appsettings.json y pega esto (asegúrate de que el nombre del servidor sea el tuyo, normalmente es (localdb)\\mssqllocaldb):

JSON

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SumaDbPro;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": { ... }
}


6. El "Cerebro" (Program.cs)
Borra todo y deja el Program.cs así:

C#

using Microsoft.EntityFrameworkCore;
using SumaProServer.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Conexión a SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();

7.Crear la Base de Datos (Último paso)
Abre la Consola de Administrador de Paquetes (Abajo en Visual Studio) y escribe estos dos:

Add-Migration Inicial

Update-Database

8. f5


🧪 Ejemplo de Uso
Realiza una petición POST a /api/Calculadora/sumar?n1=10&n2=20.

Respuesta (JSON):

JSON

{
  "id": 1,
  "numero1": 10.0,
  "numero2": 20.0,
  "resultado": 30.0,
  "fecha": "2026-01-02T19:29:00"
}