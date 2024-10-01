namespace tptarjeta;

using System;

class MainClass
{
    public static void Main(string[] args)
    {
        Tarjeta miTarjeta = new Tarjeta(100); 
        Colectivo colectivo = new Colectivo();

        Boleto boleto = Boleto.EmitirSiHaySaldo(miTarjeta, colectivo);

        if (boleto != null)
        {
            boleto.MostrarInfo();
        }
    }
}
