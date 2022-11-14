namespace DSI2022.Business {
	public class Turno : IComparable<Turno> {
		private DateTime fechaInicio;
		private DateTime fechaFin;
		private List<HistorialEstado> historialEstados;

		public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
		public DateTime FechaFin { get => fechaFin; set => fechaFin = value; }
		public List<HistorialEstado> HistorialEstados { get => historialEstados; set => historialEstados = value; }

		public Turno(DateTime fechaInicio, DateTime fechaFin, List<HistorialEstado> historialEstados) {
			this.fechaInicio = fechaInicio;
			this.fechaFin = fechaFin;
			this.historialEstados = historialEstados;
		}

		internal DateTime GetFrom() {
			return fechaInicio;
		}

		internal string GetEstado() {
			return GetHistorialEstadoActual().GetEstado();
		}

		private HistorialEstado GetHistorialEstadoActual() {
			return historialEstados.Last();
		}

		internal DateTime GetTo() {
			return fechaFin;
		}

		public int CompareTo(Turno other) {
			if (other.fechaInicio < fechaInicio)
				return 1;
			if (other.fechaInicio > fechaInicio)
				return -1;
			return 0;
		}

		internal bool EsValidoEn(DateTime en) {
			return fechaInicio > en;
		}

		internal void Reservar(Estado reservado) {
			SetEstado(reservado);
		}

		private void SetEstado(Estado reservado) {
			historialEstados.Add(new HistorialEstado(reservado));
		}
	}
}
