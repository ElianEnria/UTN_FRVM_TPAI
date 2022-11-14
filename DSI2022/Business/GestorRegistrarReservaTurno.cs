using DSI2022.Persistence;
using DSI2022.Presentation;
using DSI2022.Security;
using System;

namespace DSI2022.Business {
	public class GestorRegistrarReservaTurno : ISujetoReserva {
		private PantallaReservarTurno pantalla;
		private TipoRecursoTecnologico[] tiposRecursoTecnologico;
		private CentroInvestigacion[] centrosInvestigacion;
		private Estado[] estados;
		private RecursoTecnologico recursoTecnologicoSeleccionado;

		//Aplicacion del patron obtenemos los observadores
		private List<IObservadorReserva> _observadores = new List<IObservadorReserva>()
		{
            new PantallaEmail()
			//Grcias a este patron. Podemos instanciar una nueva clase, de manera muy sencilla
		};
		// ir a la linea 146

        public GestorRegistrarReservaTurno(PantallaReservarTurno pantalla) {
			estados = Database.FetchEstados();
            tiposRecursoTecnologico = Database.FetchTiposRT();
			centrosInvestigacion = Database.FetchCentrosInvestigacion();
			this.pantalla = pantalla;
		}

		public void OpcionReservarTurno() {
			TipoRecursoTecnologico[] tiposDisponibles = GetTiposRT();
			pantalla.SolicitarTipoRT(tiposDisponibles);
		}

		public void SeleccionarTipoRT(TipoRecursoTecnologico seleccionado) {
			List<CentroInvestigacionDisplay> cIDisplay = new List<CentroInvestigacionDisplay>();

			foreach (CentroInvestigacion centroInvestigacion in centrosInvestigacion) {
				RecursoTecnologico[] rTSinBaja = BuscarRTSinBajaDeTipo(seleccionado, centroInvestigacion);

				cIDisplay.Add(GenerarCIDisplay(centroInvestigacion, rTSinBaja));
			}

			pantalla.SolicitarSeleccionRT(cIDisplay);
		}

		public void SeleccionarRecursoTecnologico(CentroInvestigacion cISeleccionado, RecursoTecnologico seleccionado) {
			if (VerificarPerteneceAlLogeado(cISeleccionado, seleccionado)) {
				Turno[] turnosValidos = seleccionado.GetTurnos(GetFechaHoraActual());

				turnosValidos = OrdernarTurnos(turnosValidos);

				TurnoDisplay[] displays = GetTurnosDisplay(turnosValidos, seleccionado);
				TurnoDiaDisplay[] diaTurnos = AgruparPorDia(displays);

				recursoTecnologicoSeleccionado = seleccionado;

				pantalla.SolicitarSeleccionTurno(diaTurnos);
			}
		}

		public void ReservarTurno(Turno turno) {
			Estado reservado = GetEstado("Reservado");
			turno.Reservar(reservado);
			string eMail = SessionManager.GetCientifico().GetEmail();
			Database.asd(centrosInvestigacion);
			generarNotificacionTurnoReserva(turno, recursoTecnologicoSeleccionado);
			FinCU();
		}

		private TipoRecursoTecnologico[] GetTiposRT() {
			return tiposRecursoTecnologico;
		}

		private CentroInvestigacionDisplay GenerarCIDisplay(CentroInvestigacion centroInvestigacion, RecursoTecnologico[] rTSinBaja) {
			string nombre = centroInvestigacion.GetNombre();
			RecursoTecnologicoDisplay[] rTDisplay = GetRTDisplay(rTSinBaja);
			CentroInvestigacionDisplay cIDisplay = new CentroInvestigacionDisplay(centroInvestigacion, nombre, rTDisplay);

			return cIDisplay;
		}

		private RecursoTecnologicoDisplay[] GetRTDisplay(RecursoTecnologico[] rTSinBaja) {
			List<RecursoTecnologicoDisplay> recursos = new List<RecursoTecnologicoDisplay>();

			foreach (RecursoTecnologico seleccionado in rTSinBaja) {
				RecursoTecnologicoDisplay rTDisplay = new RecursoTecnologicoDisplay(
						from: seleccionado,
						numero: seleccionado.GetNumero(),
						marca: seleccionado.GetMarca(),
						modelo: seleccionado.GetModelo(),
						estado: seleccionado.GetEstado()
					);
				recursos.Add(rTDisplay);
			}

			return recursos.ToArray();
		}

		private RecursoTecnologico[] BuscarRTSinBajaDeTipo(TipoRecursoTecnologico tipo, CentroInvestigacion centroInvestigacion) {
			List<RecursoTecnologico> encontrados = new List<RecursoTecnologico>();

			encontrados.AddRange(centroInvestigacion.BuscarRTDeTipo(tipo));

			return encontrados.ToArray();
		}

		private TurnoDiaDisplay[] AgruparPorDia(TurnoDisplay[] turnos) {
			List<TurnoDiaDisplay> turnoDias = new List<TurnoDiaDisplay>();

			DateOnly date = turnos[0].GetFromDate();
			TurnoDiaDisplay currentTDD = new TurnoDiaDisplay(date);

			foreach (TurnoDisplay turno in turnos) {
				if (date == turno.GetFromDate())
					currentTDD.Add(turno);
				else {
					date = turno.GetFromDate();
					turnoDias.Add(currentTDD);
					currentTDD = new TurnoDiaDisplay(date);
				}
			}

			turnoDias.Add(currentTDD);
			return turnoDias.ToArray();
		}

		private TurnoDisplay[] GetTurnosDisplay(Turno[] turnos, RecursoTecnologico rt) {
			List<TurnoDisplay> ret = new List<TurnoDisplay>();

			foreach (Turno turno in turnos) {
				TurnoDisplay turnoDisplay = new TurnoDisplay(turno, rt);
				ret.Add(turnoDisplay);
			}

			return ret.ToArray();
		}

		private Turno[] OrdernarTurnos(Turno[] turnosValidos) {
			List<Turno> turnos = new List<Turno>(turnosValidos);
			turnos.Sort();

			return turnos.ToArray();
		}


		//EVENTO GET ESTADO Actuliza el estado del turno. Es nuestro metodo de enganche el cual al cambiar de estado genera que debamos notificar a los observadores
		private Estado GetEstado(string nombre) {
			foreach (Estado estado in estados) {
				if (estado.GetNombre() == nombre)
					return estado;
			}

			return null;
		}


        private DateTime GetFechaHoraActual()
        {
            return DateTime.Now;
        }

		// va a llamar al metodo Notificar 
        public void generarNotificacionTurnoReserva(Turno turno, RecursoTecnologico recurso)
        {
            notificar(turno, recurso);
        }


		//Demas Metodos del Analisis 
        private bool VerificarPerteneceAlLogeado(CentroInvestigacion centroInvestigacion, RecursoTecnologico recurso) {
			PersonalCientifico logeado = SessionManager.GetCientifico();

			//return centroInvestigacion.TrabajaCientifico(logeado) && centroInvestigacion.ContieneRecurso(recurso);
			return true;
		}
		

		// Aplicacion del patron 
        void ISujetoReserva.agregar(IObservadorReserva o)
        {
            _observadores.Add(o);
        }

        void ISujetoReserva.quitar(IObservadorReserva o)
        {
            _observadores.Remove(o);
        }

		// El turno contiene de forma interna la fecha y la hora
        public void notificar(Turno turno, RecursoTecnologico recurso)
        {
			//Mientras existan clases concretas y subscriptores. El metodo ira notificando a las correspondientes
			
            foreach (var observador in _observadores)
            {
                observador.actualizar(turno, recurso);
            }
        }
		private void FinCU() {
			MessageBox.Show("Fin CU");
		}
    }
}
