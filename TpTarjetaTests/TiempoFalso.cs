using System;

namespace ManejoDeTiempos
{
    public class TiempoFalso
    {
        private DateTime tiempo;

        public TiempoFalso()
        {
            tiempo = new DateTime(2024, 11, 11,13,45,00);
        }

        public DateTime Now
        {
            get { return tiempo; }
        }

        public void AgregarDias(int cantidad)
        {
            tiempo = tiempo.AddDays(cantidad);
        }

        public void AgregarMinutos(int cantidad)
        {
            tiempo = tiempo.AddMinutes(cantidad);
        }

        public void AgregarSegundos(int cantidad)
        {
            tiempo = tiempo.AddSeconds(cantidad);
        }

        public void EstablecerTiempo(DateTime nuevoTiempo)
        {
            tiempo = nuevoTiempo;
        }
    }
}
