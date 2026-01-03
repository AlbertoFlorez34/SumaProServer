namespace SumaProServer.Models;

public class Suma
{
    public int Id { get; set; }
    public double Numero1 { get; set; }
    public double Numero2 { get; set; }
    public double Resultado { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
}