using NUnit.Framework;
using System;
using NUnit.Framework.Internal;
using System.Diagnostics.Contracts;
using tptarjeta;
using ManejoDeTiempos;
using static tptarjeta.Tarjeta;

namespace TpTarjetaTests
{
    internal class TestsIteracion3
    {
        private Tarjeta tarjeta;
        private FranquiciaParcial FranquiciaParcial;
        private FranquiciaCompleta FranquiciaCompleta;
        private Colectivo colectivo;
        private TiempoFalso tiempoFalso;

        [SetUp]
        public void SetUp()
        {
            tarjeta = new Tarjeta(0);
            FranquiciaParcial = new FranquiciaParcial(0);
            FranquiciaCompleta = new FranquiciaCompleta(0);
            colectivo = new Colectivo();
            tiempoFalso = new TiempoFalso();
        }

        [Test]
        public void MinutosEntreViajes()
        {
            FranquiciaParcial = new FranquiciaParcial(5000);
            DateTime tiempoactual = tiempoFalso.Now;

            Boleto boleto1 = Boleto.EmitirSiHaySaldo(FranquiciaParcial, colectivo, "Linea K", tiempoactual);
            Assert.That(boleto1, Is.Not.Null);

            tiempoactual = tiempoFalso.Now;
            Boleto boleto2 = Boleto.EmitirSiHaySaldo(FranquiciaParcial, colectivo, "Linea K", tiempoactual);
            Assert.That(boleto2, Is.Null);

            tiempoFalso.AgregarMinutos(5);
            tiempoactual = tiempoFalso.Now;

            Boleto boleto3 = Boleto.EmitirSiHaySaldo(FranquiciaParcial, colectivo, "Linea K", tiempoactual);
            Assert.That(boleto3, Is.Not.Null);
        }



        [Test]
        public void ViajesPorDiaParcial()
        {
            FranquiciaParcial = new FranquiciaParcial(5000);
            Boleto boleto1 = Boleto.EmitirSiHaySaldo(FranquiciaParcial, colectivo, "Linea K", tiempoFalso.Now);
            Assert.That(boleto1, Is.Not.Null);
            tiempoFalso.AgregarMinutos(5);
            Boleto boleto2 = Boleto.EmitirSiHaySaldo(FranquiciaParcial, colectivo, "Linea K", tiempoFalso.Now);
            Assert.That(boleto2, Is.Not.Null);
            tiempoFalso.AgregarMinutos(5);
            Boleto boleto3 = Boleto.EmitirSiHaySaldo(FranquiciaParcial, colectivo, "Linea K", tiempoFalso.Now);
            Assert.That(boleto3, Is.Not.Null);
            tiempoFalso.AgregarMinutos(5);
            Boleto boleto4 = Boleto.EmitirSiHaySaldo(FranquiciaParcial, colectivo, "Linea K", tiempoFalso.Now);
            Assert.That(boleto4, Is.Not.Null);
            tiempoFalso.AgregarMinutos(5);
            Boleto boleto5 = Boleto.EmitirSiHaySaldo(FranquiciaParcial, colectivo, "Linea K", tiempoFalso.Now);
            Assert.That(boleto5, Is.Null);
        }

        [Test]
        public void ViajesGratuitosDiarios()
        {
            FranquiciaCompleta = new FranquiciaCompleta(5000);
            decimal tarifa = colectivo.Tarifa;

            Boleto boleto1 = Boleto.EmitirSiHaySaldo(FranquiciaCompleta, colectivo, "Linea K", tiempoFalso.Now);
            decimal SaldoActual1 = FranquiciaCompleta.ConsultarSaldo();
            Assert.That(boleto1, Is.Not.Null);
            Assert.That(SaldoActual1, Is.EqualTo(5000));

            Boleto boleto2 = Boleto.EmitirSiHaySaldo(FranquiciaCompleta, colectivo, "Linea K", tiempoFalso.Now);
            decimal SaldoActual2 = FranquiciaCompleta.ConsultarSaldo();
            Assert.That(boleto2, Is.Not.Null);
            Assert.That(SaldoActual2, Is.EqualTo(5000));

            Boleto boleto3 = Boleto.EmitirSiHaySaldo(FranquiciaCompleta, colectivo, "Linea K", tiempoFalso.Now);
            decimal SaldoActual3 = FranquiciaCompleta.ConsultarSaldo();
            Assert.That(boleto3, Is.Not.Null);
            Assert.That(SaldoActual3, Is.EqualTo(5000 - tarifa));
        }

        [Test]
        public void SaldoMaximo()
        {
            Tarjeta tarjeta = new Tarjeta(34000);
            decimal SaldoInicial = tarjeta.ConsultarSaldo();
            decimal carga = 8000;
            tarjeta.CargarSaldo(carga);
            decimal limite = 36000;
            decimal SaldoActual = tarjeta.ConsultarSaldo();
            decimal Excedente = tarjeta.ConsultarExceso();
            Assert.That(SaldoActual, Is.EqualTo(limite));
            Assert.That(Excedente, Is.EqualTo(SaldoInicial + carga - limite));
        }
    }
}

