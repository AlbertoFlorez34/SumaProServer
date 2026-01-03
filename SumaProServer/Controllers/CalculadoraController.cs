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