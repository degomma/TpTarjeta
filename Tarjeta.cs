namespace tptarjeta;

using System;

public class Tarjeta
{
    private decimal saldo;
    private const decimal limiteSaldo = 9900;

    public Tarjeta(decimal saldoInicial = 0)
    {
        saldo = saldoInicial;
    }

    public bool TieneSaldoSuficiente(decimal monto)
    {
        return saldo >= monto;
    }



    public void CargarSaldo(decimal monto)
    {
        if (monto % 1000 == 0 && monto >= 2000 && monto <= 9000)
        {
            if (saldo + monto <= limiteSaldo)
            {
                saldo += monto;
                Console.WriteLine($"Se cargaron ${monto}. Saldo actual: ${saldo}");
            }
            else
            {
                Console.WriteLine("El monto excede el límite de la tarjeta.");
            }
        }
        else
        {
            Console.WriteLine("Monto no válido para carga.");
        }
    }

    public decimal ConsultarSaldo()
    {
        return saldo;
    }
}
