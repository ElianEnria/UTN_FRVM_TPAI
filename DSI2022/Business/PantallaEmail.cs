using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSI2022.Business
{
    public class PantallaEmail : IObservadorReserva
    {
        //Metodo polimorfico
        public void actualizar(Turno turno, RecursoTecnologico recurso)
        {
            EnviarNotificacion(turno, recurso);
        }
        //Metodo propio de la clase concreta
        private void EnviarNotificacion(Turno turno, RecursoTecnologico recursoTecnologico)
        {
            MessageBox.Show("Enviando notificacion para el turno " + turno.FechaInicio + " del recurso tecnologico " + recursoTecnologico.Tipo.Nombre);
        }
    }
}
