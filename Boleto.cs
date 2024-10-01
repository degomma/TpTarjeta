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
        decimal tarifa = colectivo.Tarifa;

        if (tarjeta is FranquiciaParcial) // Aplicar descuento para Franquicia Parcial
        {
            tarifa /= 2; // El precio del boleto es la mitad
        }
        else if (tarjeta is FranquiciaCompleta) // No se descuenta nada para Franquicia Completa
        {
            tarifa = 0;
        }

        if (tarjeta.TieneSaldoSuficiente(tarifa))
        {
            tarjeta.DescontarSaldo(tarifa); // Descontar el monto de la tarjeta
            return new Boleto(tarifa, DateTime.Now);
        }
        else
        {
            Console.WriteLine("No se puede emitir boleto. Saldo insuficiente.");
            return null;
        }
    }
}
