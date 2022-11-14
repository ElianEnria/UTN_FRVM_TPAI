using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSI2022.Business
{
    interface IObservadorReserva
    {
        //Metodo Polimorfico
        void actualizar(Turno turno, RecursoTecnologico recurso);
    }
}
