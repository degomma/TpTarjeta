using NUnit.Framework;
using System;
using NUnit.Framework.Internal;
using System.Diagnostics.Contracts;
using tptarjeta;
using ManejoDeTiempos;
using static tptarjeta.Tarjeta;

namespace TpTarjetaTests
{
    internal class TestsIteracion4
    {
        private Tarjeta tarjeta;
        private FranquiciaParcial FranquiciaParcial;
        private FranquiciaCompleta FranquiciaCompleta;
        private Colectivo colectivo;
        private TiempoFalso tiempoFalso;

        [SetUp]
        public void SetUp()
        {
            tarjeta = new Tarjeta(34000);
            FranquiciaParcial = new FranquiciaParcial(0);
            FranquiciaCompleta = new FranquiciaCompleta(0);
            colectivo = new Colectivo();
            tiempoFalso = new TiempoFalso();
        }

        [Test]
        public void UsoFrecuente()
        {
            tarjeta = new Tarjeta(10000);
            float tarifa = 1200;
            const float carga = 10000;
            for (int i = 0; i < 90; i++)
            {

                tarjeta.EstablecerSaldo(10000);
                if (i < 29)
                {
                    Boleto boleto = Boleto.EmitirSiHaySaldo(tarjeta, colectivo, "Linea K", tiempoFalso.Now);
                    Assert.That(boleto, Is.Not.Null);
                    Assert.That(tarjeta.ConsultarSaldo(), Is.EqualTo(carga - tarifa));
                }
                else if (i > 29 && i < 79)
                {
                    Boleto boleto = Boleto.EmitirSiHaySaldo(tarjeta, colectivo, "Linea K", tiempoFalso.Now);
                    Assert.That(boleto, Is.Not.Null);
                    Assert.That(tarjeta.ConsultarSaldo(), Is.EqualTo(carga - (0.8 * tarifa)));
                }
                else if (i >= 79 && i <= 80)
                {
                    Boleto boleto = Boleto.EmitirSiHaySaldo(tarjeta, colectivo, "Linea K", tiempoFalso.Now);
                    Assert.That(boleto, Is.Not.Null);
                    Assert.That(tarjeta.ConsultarSaldo(), Is.EqualTo(carga - (0.75 * tarifa)));
                }
                else
                {
                    Boleto boleto = Boleto.EmitirSiHaySaldo(tarjeta, colectivo, "Linea K", tiempoFalso.Now);
                    Assert.That(boleto, Is.Not.Null);
                    Assert.That(tarjeta.ConsultarSaldo(), Is.EqualTo(carga - tarifa));
                }
            }
        }
        [Test]
        public void DiasyHorasParcial()
        {
            FranquiciaParcial = new FranquiciaParcial(5000);

            tiempoFalso.EstablecerTiempo(new DateTime(2024, 11, 11, 7, 0, 0)); //lunes 7 am
            DateTime tiempoactual = tiempoFalso.Now;

            Boleto boleto1 = Boleto.EmitirSiHaySaldo(FranquiciaParcial, colectivo, "Linea K", tiempoactual);
            Assert.That(boleto1, Is.Not.Null);

            tiempoFalso.EstablecerTiempo(new DateTime(2024, 11, 11, 23, 0, 0)); //lunes 11 pm
            tiempoactual = tiempoFalso.Now;

            Boleto boleto2 = Boleto.EmitirSiHaySaldo(FranquiciaParcial, colectivo, "Linea K", tiempoactual);
            Assert.That(boleto2, Is.Null);

            tiempoFalso.EstablecerTiempo(new DateTime(2024, 11, 16, 7, 0, 0)); //sabado 7 am
            tiempoactual = tiempoFalso.Now;

            Boleto boleto3 = Boleto.EmitirSiHaySaldo(FranquiciaParcial, colectivo, "Linea K", tiempoactual);
            Assert.That(boleto3, Is.Null);
        }

        [Test]
        public void DiasyHorasCompleto()
        {
            FranquiciaCompleta= new FranquiciaCompleta(5000);

            tiempoFalso.EstablecerTiempo(new DateTime(2024, 11, 11, 7, 0, 0)); //lunes 7 am
            DateTime tiempoactual = tiempoFalso.Now;

            Boleto boleto1 = Boleto.EmitirSiHaySaldo(FranquiciaCompleta, colectivo, "Linea K", tiempoactual);
            Assert.That(boleto1, Is.Not.Null);

            tiempoFalso.EstablecerTiempo(new DateTime(2024, 11, 11, 23, 0, 0)); //lunes 11 pm
            tiempoactual = tiempoFalso.Now;

            Boleto boleto2 = Boleto.EmitirSiHaySaldo(FranquiciaCompleta, colectivo, "Linea K", tiempoactual);
            Assert.That(boleto2, Is.Null);

            tiempoFalso.EstablecerTiempo(new DateTime(2024, 11, 16, 7, 0, 0)); //sabado 7 am
            tiempoactual = tiempoFalso.Now;

            Boleto boleto3 = Boleto.EmitirSiHaySaldo(FranquiciaCompleta, colectivo, "Linea K", tiempoactual);
            Assert.That(boleto3, Is.Null);
        }

    }
}
