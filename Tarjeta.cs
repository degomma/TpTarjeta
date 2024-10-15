namespace tptarjeta;

using System;

public class Tarjeta
{
    private decimal saldo;
    private const decimal limiteSaldo = 36000;
    private int viajesPorDia;
    private DateTime ultimoViaje;
    private const int maxViajesPorDia = 2;
    private Decimal exceso;
    
    public Tarjeta(decimal saldoInicial = 0)
    {
        saldo = saldoInicial;
        viajesPorDia = 0;
        ultimoViaje = DateTime.MinValue;
    }

    public bool TieneSaldoSuficiente(decimal monto)
    {
        return saldo >= monto;
    }

    public void DescontarSaldo(decimal monto)
    {
        saldo -= monto;
        var saldoexceso=AgregarExceso(saldo,exceso);
        saldo=saldoexceso.Item1;
        exceso=saldoexceso.Item2;
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

    public Tuple<decimal, decimal> AgregarExceso(decimal saldo, decimal exceso){
        if((saldo+exceso)<=limiteSaldo){
            saldo+=exceso;
            exceso=0;
        }
        else{
            exceso=(saldo+exceso)-limiteSaldo;
            saldo=limiteSaldo;
        }
        var tuple = new Tuple<decimal, decimal>(saldo,exceso);
        return tuple;
    }
    
  
    public bool PuedeRealizarViaje()
    {
        if (this is FranquiciaParcial){
            if (DateTime.Now - ultimoViaje < TimeSpan.FromSeconds(300))
            {
                return false;
            }
        
            else{
                return true;
            }
            }
        if (this is FranquiciaCompleta){
            if(viajesPorDia>=maxViajesPorDia){
                return false;
            }
            else{
                return true;
            }
        }
        else{
        return true;
            }
    }

    public void RegistrarViaje()
    {
        ultimoViaje = DateTime.Now;
        viajesPorDia++;
    }

    public class FranquiciaParcial : Tarjeta
    {
        public FranquiciaParcial(decimal saldoInicial) : base(saldoInicial) {}
    }

    public class FranquiciaCompleta : Tarjeta
    {
        public FranquiciaCompleta(decimal saldoInicial) : base(saldoInicial) {}
    }
}
