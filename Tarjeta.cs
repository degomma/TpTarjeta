namespace tptarjeta;

using System;

public class Tarjeta
{
    private decimal saldo;
    private const decimal limiteSaldo = 36000;
    private int viajesPorDia;
    private DateTime ultimoViaje;
    private const int maxViajesPorDia = 2;
    private decimal exceso;
    private int viajesMensuales;
    private DateTime inicioMes;

    public Tarjeta(decimal saldoInicial = 0)
    {
        saldo = saldoInicial;
        viajesPorDia = 0;
        viajesMensuales = 0;
        inicioMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        ultimoViaje = DateTime.MinValue;
    }

    public bool TieneSaldoSuficiente(decimal monto)
    {
        return saldo >= monto;
    }

    public void DescontarSaldo(decimal monto)
    {
        saldo -= monto;
        var saldoExceso = AgregarExceso(saldo, exceso);
        saldo = saldoExceso.Item1;
        exceso = saldoExceso.Item2;
        Console.WriteLine($"Se descontaron ${monto}. Saldo restante: ${saldo} Excedente: ${exceso} (pendiente de acreditación)");
        RegistrarViaje();
    }

    public void CargarSaldo(decimal monto)
    {
        if (monto < 2000 || monto > 9000 || monto % 1000 != 0)
        {
            Console.WriteLine("Monto no válido para carga.");
            return;
        }
        if (monto + saldo > limiteSaldo)
        {
            exceso = (monto + saldo) - limiteSaldo;
            saldo = limiteSaldo;
            Console.WriteLine($"Se cargaron hasta el límite. Saldo actual: ${saldo}. Excedente: ${exceso} (pendiente de acreditación).");
        }
        else
        {
            saldo += monto;
            Console.WriteLine($"Se cargaron ${monto}. Saldo actual: ${saldo}");
        }
    }

    public decimal ConsultarSaldo()
    {
        return saldo;
    }

    public decimal ConsultarExceso()
    {
        return exceso;
    }

    public Tuple<decimal, decimal> AgregarExceso(decimal saldo, decimal exceso)
    {
        if ((saldo + exceso) <= limiteSaldo)
        {
            saldo += exceso;
            exceso = 0;
        }
        else
        {
            exceso = (saldo + exceso) - limiteSaldo;
            saldo = limiteSaldo;
        }
        return new Tuple<decimal, decimal>(saldo, exceso);
    }

    public bool PuedeRealizarViaje()
    {
        if (this is FranquiciaParcial || this is FranquiciaCompleta)
        {
            if (!EstaEnHorarioPermitido() || !EsDiaPermitido())
            {
                Console.WriteLine("Viaje no permitido fuera de horario o día habilitado para esta franquicia.");
                return false;
            }
        }

        if (this is FranquiciaParcial && DateTime.Now - ultimoViaje < TimeSpan.FromSeconds(300))
        {
            return false;
        }

        if (this is FranquiciaCompleta && viajesPorDia >= maxViajesPorDia)
        {
            return false;
        }

        return true;
    }

    public void RegistrarViaje()
    {
        ultimoViaje = DateTime.Now;
        viajesPorDia++;

        if (DateTime.Now.Month != inicioMes.Month)
        {
            viajesMensuales = 0;
            inicioMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        }
        viajesMensuales++;
    }

    private bool EstaEnHorarioPermitido()
    {
        int hora = DateTime.Now.Hour;
        return hora >= 6 && hora < 22;
    }

    private bool EsDiaPermitido()
    {
        DayOfWeek dia = DateTime.Now.DayOfWeek;
        return dia >= DayOfWeek.Monday && dia <= DayOfWeek.Friday;
    }

    public int ConsultarViajesMensuales()
    {
        return viajesMensuales;
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
