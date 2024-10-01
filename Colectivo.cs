namespace tptarjeta;

using System;

public class Colectivo
{
    private const decimal tarifa = 940;

    public decimal Tarifa => tarifa;

    public Boleto EmitirBoleto()
    {
        return new Boleto(tarifa, DateTime.Now);
    }

  
    }

