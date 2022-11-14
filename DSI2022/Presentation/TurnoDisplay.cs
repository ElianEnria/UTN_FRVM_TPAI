using DSI2022.Business;

namespace DSI2022.Presentation {
	public class TurnoDisplay {
		public enum Estado { DISPONIBLE, PENDIENTE_CONFIRMACION, RESERVADO }

		public DateTime from;
		public DateTime to;
		public Estado estado;
		public Turno parent;
		public RecursoTecnologico rt;

		public TurnoDisplay(Turno turno, RecursoTecnologico rt) {
			this.from = turno.GetFrom();
			this.to = turno.GetTo();
			this.parent = turno;
			this.rt = rt;
			string estado = turno.GetEstado();

			switch (estado) {
				case "Disponible": this.estado = Estado.DISPONIBLE; break;
				case "PendienteConfirmacion": this.estado = Estado.PENDIENTE_CONFIRMACION; break;
				case "Reservado": this.estado = Estado.RESERVADO; break;
			}
		}

		internal Estado GetEstado() {
			return estado;
		}

		internal bool EstaDisponible() {
			return estado == Estado.DISPONIBLE;
		}

		internal DateOnly GetFromDate() {
			return new DateOnly(from.Year, from.Month, from.Day);
		}

		public string GetFullDateString() {
			return from.ToString("g");
		}

		public override string ToString() {
			return from.ToString("t") + " - " + to.ToString("t");
		}

		internal Turno GetTurno() {
			return this.parent;
		}

		internal string GetDatosRT() {
			return rt.GetDatos();
		}
	}
}
