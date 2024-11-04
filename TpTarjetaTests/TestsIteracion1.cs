using NUnit.Framework;
using System;
using NUnit.Framework.Internal;
using System.Diagnostics.Contracts;
using tptarjeta;
using ManejoDeTiempos;
using static tptarjeta.Tarjeta;

namespace tptarjeta.Tests
{
    [TestFixture]
    public class TestsIteracion1
    {
        private Tarjeta tarjeta;
        private Colectivo colectivo;

        [SetUp]
        public void SetUp()
        {
            tarjeta = new Tarjeta(0);
            colectivo = new Colectivo();
        }

        [Test]
        [TestCase(2000)]
        [TestCase(3000)]
        [TestCase(4000)]
        [TestCase(5000)]
        [TestCase(6000)]
        [TestCase(7000)]
        [TestCase(8000)]
        [TestCase(9000)]
        public void CargarSaldo(decimal monto)
        {
            var saldoInicial = tarjeta.ConsultarSaldo();

            tarjeta.CargarSaldo(monto);
            var saldoActual = tarjeta.ConsultarSaldo();

            Assert.That(saldoActual, Is.EqualTo(saldoInicial + monto));
        }
    }
}
