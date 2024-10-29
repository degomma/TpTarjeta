namespace tptarjeta;

using System;

class MainClass
{
    public static void Main(string[] args)
    {
        Tarjeta miTarjeta = ElegirTipoDeTarjeta();

        if (miTarjeta == null)
        {
            Console.WriteLine("Tipo de tarjeta no válido.");
            return;
        }

        while (true)
        {
            Console.WriteLine("1. Cargar saldo");
            Console.WriteLine("2. Pagar boleto");
            Console.WriteLine("3. Salir");
            Console.Write("Seleccione una opción: ");
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
                        Console.WriteLine("Monto no válido.");
                    }
                    break;

                case "2":
                    Console.WriteLine("Seleccione el tipo de colectivo:");
                    Console.WriteLine("1. Normal");
                    Console.WriteLine("2. Interurbano");
                    string tipoColectivo = Console.ReadLine();
                    Colectivo colectivo;

                    if (tipoColectivo == "1")
                    {
                        colectivo = new Colectivo();
                    }
                    else if (tipoColectivo == "2")
                    {
                        colectivo = new ColectivoInterurbano();
                    }
                    else
                    {
                        Console.WriteLine("Tipo de colectivo no válido.");
                        continue;
                    }

                    Boleto boleto = Boleto.EmitirSiHaySaldo(miTarjeta, colectivo, "Línea K");
                    if (boleto != null)
                    {
                        boleto.MostrarInfo();
                    }
                    break;

                case "3":
                    return;

                default:
                    Console.WriteLine("Opción no válida. Intente nuevamente.");
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
