namespace tptarjeta;

using System;

class MainClass
{
    public static void Main(string[] args)
    {
        Tarjeta miTarjeta = new Tarjeta.FranquiciaParcial(500); 
        Colectivo colectivo = new Colectivo();

        Boleto boleto = Boleto.EmitirSiHaySaldo(miTarjeta, colectivo, "Línea K");

        if (boleto != null)
        {
            boleto.MostrarInfo();
        }
    }
}
