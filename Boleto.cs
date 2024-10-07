namespace tptarjeta;

using System;

public class Boleto
{
    public decimal Tarifa { get; private set; }
    public DateTime FechaHora { get; private set; }
    public string TipoTarjeta { get; private set; }
    public string LineaColectivo { get; private set; }
    public decimal TotalAbonado { get; private set; }
    public decimal SaldoRestante { get; private set; }
    public string IDTarjeta { get; private set; }
    public string Descripcion { get; private set; }

    public Boleto(decimal tarifa, DateTime fechaHora, Tarjeta tarjeta, string lineaColectivo)
    {
        Tarifa = tarifa;
        FechaHora = fechaHora;
        TipoTarjeta = tarjeta.GetType().Name;
        LineaColectivo = lineaColectivo;
        TotalAbonado = tarifa;
        SaldoRestante = tarjeta.ConsultarSaldo();
        IDTarjeta = tarjeta.GetHashCode().ToString();
        Descripcion = GenerarDescripcion(tarifa, tarjeta);
    }

    public void MostrarInfo()
    {
        Console.WriteLine($"Boleto - Tarifa: ${Tarifa}, Fecha/Hora: {FechaHora}");
        Console.WriteLine();
        Console.WriteLine($"Tipo de tarjeta: {TipoTarjeta}");
        Console.WriteLine($"Línea de colectivo: {LineaColectivo}");
        Console.WriteLine($"Total abonado: ${TotalAbonado}");
        Console.WriteLine($"Saldo restante: ${SaldoRestante}");
        Console.WriteLine($"ID de tarjeta: {IDTarjeta}");
        Console.WriteLine(Descripcion);
    }

    private string GenerarDescripcion(decimal tarifa, Tarjeta tarjeta)
    {
        if (tarjeta.ConsultarSaldo() < 0)
        {
            decimal saldoAdeudado = Math.Abs(tarjeta.ConsultarSaldo());
            return $"Abona saldo ${saldoAdeudado}";
        }
        return "Pago realizado sin saldo negativo.";
    }

    public static Boleto EmitirSiHaySaldo(Tarjeta tarjeta, Colectivo colectivo, string lineaColectivo)
    {
        decimal tarifa = colectivo.Tarifa;

        if (tarjeta is Tarjeta.FranquiciaParcial)
        {
            tarifa /= 2;
        }
        else if (tarjeta is Tarjeta.FranquiciaCompleta)
        {
            tarifa = 0;
        }

        if (tarjeta.PuedeRealizarViaje(tarjeta is Tarjeta.FranquiciaCompleta))
        {
            if (tarjeta.TieneSaldoSuficiente(tarifa))
            {
                tarjeta.DescontarSaldo(tarifa);
                return new Boleto(tarifa, DateTime.Now, tarjeta, lineaColectivo);
            }
            else
            {
                Console.WriteLine("No se puede emitir boleto. Saldo insuficiente.");
                return null;
            }
        }
        else
        {
            Console.WriteLine("No se puede emitir boleto. Restricciones de viaje no cumplidas.");
            return null;
        }
    }

}
