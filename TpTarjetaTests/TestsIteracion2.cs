using NUnit.Framework;
using System;
using NUnit.Framework.Internal;
using System.Diagnostics.Contracts;
using tptarjeta;
using ManejoDeTiempos;
using static tptarjeta.Tarjeta;

namespace TpTarjetaTests
{
    internal class TestsIteracion2
    {
        private Tarjeta tarjeta;
        private FranquiciaParcial FranquiciaParcial;
        private FranquiciaCompleta FranquiciaCompleta;
        private Colectivo colectivo;
        private TiempoFalso tiempoFalso;

        [SetUp]
        public void SetUp()
        {
            tiempoFalso = new TiempoFalso();
            tarjeta = new Tarjeta(0);
            FranquiciaParcial = new FranquiciaParcial(0);
            FranquiciaCompleta = new FranquiciaCompleta(0);
            colectivo = new Colectivo();
        }

        [Test]
        public void PagarConSaldoSuficiente()
        {
            tarjeta = new Tarjeta(10000);
            decimal saldoInicial = tarjeta.ConsultarSaldo();
            decimal tarifa = colectivo.Tarifa;

            Boleto boleto = Boleto.EmitirSiHaySaldo(tarjeta, colectivo, "Linea K", tiempoFalso.Now);
            decimal saldoActual = tarjeta.ConsultarSaldo();

            Assert.IsNotNull(boleto);
            Assert.That(saldoActual, Is.EqualTo(saldoInicial - tarifa));
        }

        [Test]
        public void PagarConSaldoInsuficiente()
        {
            tarjeta = new Tarjeta(100);
            decimal saldoInicial = tarjeta.ConsultarSaldo();

            Boleto boleto = Boleto.EmitirSiHaySaldo(tarjeta, colectivo, "Linea K", tiempoFalso.Now);
            decimal saldoActual = tarjeta.ConsultarSaldo();

            Assert.IsNull(boleto);
            Assert.That(saldoActual, Is.EqualTo(saldoInicial));
        }

        [Test]
        public void LimiteNegativo()
        {
            tarjeta = new Tarjeta(100);
            decimal tarifa = colectivo.Tarifa;

            Boleto boleto = Boleto.EmitirSiHaySaldo(tarjeta, colectivo, "Linea K", tiempoFalso.Now);
            decimal saldoActual = tarjeta.ConsultarSaldo();

            Assert.IsNull(boleto);
            Assert.GreaterOrEqual(saldoActual, -480);
        }

        [Test]
        public void SaldoAdeudado()
        {
            tarjeta = new Tarjeta(800);
            decimal tarifa = colectivo.Tarifa;

            Boleto boleto1 = Boleto.EmitirSiHaySaldo(tarjeta, colectivo, "Linea K", tiempoFalso.Now);
            tarjeta.CargarSaldo(2000);
            decimal saldoActual = tarjeta.ConsultarSaldo();

            Assert.IsNotNull(boleto1);
            Assert.That(saldoActual, Is.EqualTo(2000 - (tarifa - 800)));
        }

        [Test]
        [TestCase(-480)]
        [TestCase(0)]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(36000)]
        public void PagoFranquiciaCompleta(decimal monto)
        {
            FranquiciaCompleta = new FranquiciaCompleta(0);
            Boleto boleto = Boleto.EmitirSiHaySaldo(FranquiciaCompleta, colectivo, "Linea K", tiempoFalso.Now);
            decimal SaldoActual = FranquiciaCompleta.ConsultarSaldo();
            Assert.IsNotNull(boleto);
            Assert.That(SaldoActual, Is.EqualTo(0));
        }

        [Test]
        public void PagoFranquiciaParcial()
        {
            decimal tarifa = colectivo.Tarifa;
            FranquiciaParcial = new FranquiciaParcial(tarifa / 2);
            Boleto boleto = Boleto.EmitirSiHaySaldo(FranquiciaParcial, colectivo, "Linea K", tiempoFalso.Now);
            decimal SaldoActual = FranquiciaParcial.ConsultarSaldo();
            Assert.IsNotNull(boleto);
            Assert.That(SaldoActual, Is.EqualTo(0));
        }
    }
}
