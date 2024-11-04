namespace tptarjeta;

using System;
using System.Runtime.CompilerServices;
//using ManejoDeTiempos;
using Microsoft.VisualBasic;

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
        return saldo + 480 >= monto;
    }

    public void DescontarSaldo(decimal monto, DateTime fecha)
    {
        saldo -= monto;
        var saldoExceso = AgregarExceso(saldo, exceso);
        saldo = saldoExceso.Item1;
        exceso = saldoExceso.Item2;
        Console.WriteLine($"Se descontaron ${monto}. Saldo restante: ${saldo} Excedente: ${exceso} (pendiente de acreditación)");
        RegistrarViaje(fecha);
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

    public bool PuedeRealizarViaje(decimal tarifa, DateTime fecha)
    {
        if (this is FranquiciaParcial || this is FranquiciaCompleta)
        {
            if (!EstaEnHorarioPermitido(fecha) || fecha.DayOfWeek == DayOfWeek.Saturday || fecha.DayOfWeek == DayOfWeek.Sunday)
            {
                Console.WriteLine("Viaje no permitido fuera de horario o día habilitado para esta franquicia.");
                return false;
            }
        }

        if (this is FranquiciaParcial && fecha - ultimoViaje < TimeSpan.FromSeconds(300) || this is FranquiciaParcial && viajesPorDia >= 4)
        {
            return false;
        }

        if (this is FranquiciaCompleta && viajesPorDia >= maxViajesPorDia)
        {
            if (!TieneSaldoSuficiente(tarifa))
            {
                return false;
            }
        }

        return true;
    }

    public void RegistrarViaje(DateTime fecha)
    {
        ultimoViaje = fecha;
        viajesPorDia++;

        if (DateTime.Now.Month != inicioMes.Month)
        {
            viajesMensuales = 0;
            inicioMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        }
        viajesMensuales++;
    }

    private bool EstaEnHorarioPermitido(DateTime fecha)
    {
        int hora = fecha.Hour;
        return hora >= 6 && hora < 22;
    }



    public int ConsultarViajesMensuales()
    {
        return viajesMensuales;
    }

    public void EstablecerSaldo(decimal nuevoSaldo)
    {
        saldo = nuevoSaldo;
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
