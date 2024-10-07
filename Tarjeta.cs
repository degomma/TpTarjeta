namespace tptarjeta;

using System;

public class Tarjeta
{
    private decimal saldo;
    private const decimal limiteSaldo = 9900;
    private int viajesPorDia; 
    private DateTime? ultimoViaje; 

    public Tarjeta(decimal saldoInicial = 0)
    {
        saldo = saldoInicial;
        viajesPorDia = 0;
        ultimoViaje = null;
    }

    public bool TieneSaldoSuficiente(decimal monto)
    {
        return saldo >= monto;
    }

    public void DescontarSaldo(decimal monto)
    {
        saldo -= monto;
        Console.WriteLine($"Se descontaron ${monto}. Saldo restante: ${saldo}");
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

    public bool PuedeRealizarViaje(bool esFranquiciaCompleta)
    {
        if (this is FranquiciaParcial)
        {
            if (viajesPorDia < 4)
            {
                if (ultimoViaje.HasValue && (DateTime.Now - ultimoViaje.Value).TotalMinutes < 5)
                {
                    return false;
                }
                viajesPorDia++;
                ultimoViaje = DateTime.Now;
                return true;
            }
        }
        else if (esFranquiciaCompleta && viajesPorDia < 2)
        {
            viajesPorDia++;
            return true;
        }

        return false; 
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
