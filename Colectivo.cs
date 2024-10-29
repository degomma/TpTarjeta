namespace tptarjeta;

public class Colectivo
{
    private const decimal tarifa = 1200;

    public virtual decimal Tarifa => tarifa;
}

public class ColectivoInterurbano : Colectivo
{
    private const decimal tarifaInterurbana = 2500;

    public override decimal Tarifa => tarifaInterurbana;
}
