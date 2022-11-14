using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSI2022.Business
{
    internal interface ISujetoReserva
    {
        void notificar(Turno turno, RecursoTecnologico recurso);
        void agregar( IObservadorReserva o);
        void quitar( IObservadorReserva o);
    }
}
