namespace tptarjeta;

using System;

class MainClass
{
    public static void Main(string[] args)
    {
        Tarjeta miTarjeta = new Tarjeta(100); 
        Colectivo colectivo = new Colectivo();

        if (miTarjeta.DescontarSaldo(colectivo.Tarifa))
        {
            Boleto boleto = colectivo.EmitirBoleto();
            boleto.MostrarInfo();
        }
        else
        {
            Console.WriteLine("No se puede emitir boleto. Saldo insuficiente.");
        }
    }
}
