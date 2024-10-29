namespace tptarjeta;

using System;

class MainClass
{
    public static void Main(string[] args)
    {
        Tarjeta miTarjeta = ElegirTipoDeTarjeta();

        if (miTarjeta == null)
        {
            Console.WriteLine("Tipo de tarjeta no valido.");
            return;
        }

        Colectivo colectivo = new Colectivo();

        while (true)
        {
            Console.WriteLine("1. Cargar saldo");
            Console.WriteLine("2. Pagar boleto");
            Console.WriteLine("3. Salir");
            Console.Write("Seleccione una opcion: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Ingrese el monto a cargar: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal montoCarga))
                    {
                        miTarjeta.CargarSaldo(montoCarga);
                    }
                    else
                    {
                        Console.WriteLine("Monto no valido.");
                    }
                    break;
                case "2":
                    Boleto boleto = Boleto.EmitirSiHaySaldo(miTarjeta, colectivo, "Línea K");
                    if (boleto != null)
                    {
                        boleto.MostrarInfo();
                    }
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Opción no valida. Intente nuevamente.");
                    break;
            }
        }
    }

    private static Tarjeta ElegirTipoDeTarjeta()
    {
        Console.WriteLine("1. Tarjeta Normal");
        Console.WriteLine("2. Franquicia Parcial");
        Console.WriteLine("3. Franquicia Completa");
        Console.Write("Seleccione el tipo de tarjeta: ");
        string tipo = Console.ReadLine();

        Console.Write("Ingrese el saldo inicial: ");
        decimal saldoInicial = decimal.Parse(Console.ReadLine());

        switch (tipo)
        {
            case "1":
                return new Tarjeta(saldoInicial);
            case "2":
                return new Tarjeta.FranquiciaParcial(saldoInicial);
            case "3":
                return new Tarjeta.FranquiciaCompleta(saldoInicial);
            default:
                return null;
        }
    }
}
