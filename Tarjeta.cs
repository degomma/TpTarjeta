namespace tptarjeta;

using System;

public class Tarjeta
{
    private decimal saldo;
    private const decimal limiteSaldo = 36000;
    private decimal saldoPendiente;
    private int viajesPorDia;
    private DateTime? ultimoViaje;

    public Tarjeta(decimal saldoInicial = 0)
    {
        saldo = saldoInicial;
        saldoPendiente = 0;
        viajesPorDia = 0;
        ultimoViaje = null;
    }

    public bool TieneSaldoSuficiente(decimal monto)
    {
        return saldo + saldoPendiente >= monto;
    }

    public void DescontarSaldo(decimal monto)
    {
        if (saldo >= monto)
        {
            saldo -= monto;
        }
        else if (saldo + saldoPendiente >= monto)
        {
            saldoPendiente -= (monto - saldo);
            saldo = 0;
        }
        else
        {
            Console.WriteLine("No hay saldo suficiente para realizar la operación.");
            return;
        }
        Console.WriteLine($"Se descontaron ${monto}. Saldo restante: ${saldo}, Saldo pendiente: ${saldoPendiente}");
    }

    public void CargarSaldo(decimal monto)
    {
        if (monto % 1000 == 0 && monto >= 2000)
        {
            if (saldo + saldoPendiente + monto <= limiteSaldo)
            {
                saldo += monto;
                Console.WriteLine($"Se cargaron ${monto}. Saldo actual: ${saldo}");
            }
            else
            {
                decimal cantidadAcreditada = limiteSaldo - saldo;
                saldo += cantidadAcreditada;
                saldoPendiente += (monto - cantidadAcreditada);
                Console.WriteLine($"Se cargaron ${cantidadAcreditada}. Saldo actual: ${saldo}, Saldo pendiente: ${saldoPendiente}");
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

    public decimal ConsultarSaldoPendiente()
    {
        return saldoPendiente;
    }

    public class FranquiciaParcial : Tarjeta
    {
        public FranquiciaParcial(decimal saldoInicial) : base(saldoInicial) { }
    }

    public class FranquiciaCompleta : Tarjeta
    {
        public FranquiciaCompleta(decimal saldoInicial) : base(saldoInicial) { }
    }
}
