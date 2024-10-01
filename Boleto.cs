namespace tptarjeta;

using System;

public class Boleto
{
    public decimal Tarifa { get; private set; }
    public DateTime FechaHora { get; private set; }

    public Boleto(decimal tarifa, DateTime fechaHora)
    {
        Tarifa = tarifa;
        FechaHora = fechaHora;
    }

    public void MostrarInfo()
    {
        Console.WriteLine($"Boleto - Tarifa: ${Tarifa}, Fecha/Hora: {FechaHora}");
    }

    public static Boleto EmitirSiHaySaldo(Tarjeta tarjeta, Colectivo colectivo)
    {
        if (tarjeta.TieneSaldoSuficiente(colectivo.Tarifa))
        {
            return new Boleto(colectivo.Tarifa, DateTime.Now);
        }
        else
        {
            Console.WriteLine("No se puede emitir boleto. Saldo insuficiente.");
            return null;
        }
    }
}
