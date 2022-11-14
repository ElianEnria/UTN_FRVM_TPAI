namespace DSI2022.Business {
	public class RecursoTecnologico {
		private int id;
		private DateTime fechaAlta;
		private TipoRecursoTecnologico tipo;
		private List<HistorialEstado> historialEstados;
		private ModeloRT modelo;
		private Turno[] turnos;

		public int Id { get => id; set => id = value; }
		public DateTime FechaAlta { get => fechaAlta; set => fechaAlta = value; }
		public TipoRecursoTecnologico Tipo { get => tipo; set => tipo = value; }
		public List<HistorialEstado> HistorialEstados { get => historialEstados; set => historialEstados = value; }
		public ModeloRT Modelo { get => modelo; set => modelo = value; }
		public Turno[] Turnos { get => turnos; set => turnos = value; }

		public RecursoTecnologico(int id, DateTime fechaAlta, TipoRecursoTecnologico tipo, List<HistorialEstado> historialEstados, ModeloRT modelo, Turno[] turnos) {
			this.id = id;
			this.fechaAlta = fechaAlta;
			this.tipo = tipo;
			this.historialEstados = historialEstados;
			this.modelo = modelo;
			this.turnos = turnos;
		}

		internal string GetDatos() {
			return "modelo: " + modelo.GetNombre() + ", marca: " + modelo.GetMarca();
		}

		internal bool EsDeTipo(TipoRecursoTecnologico tipo) {
			return this.tipo.GetNombre() == tipo.GetNombre();
		}

		internal bool EsActivo() {
			return GetHistorialEstadoActual().EsActivo();
		}

		private HistorialEstado GetHistorialEstadoActual() {
			return historialEstados.Last();
		}

		internal string GetNumero() {
			return id.ToString();
		}

		internal string GetMarca() {
			return modelo.GetMarca();
		}

		internal string GetModelo() {
			return modelo.GetNombre();
		}

		internal string GetEstado() {
			return GetHistorialEstadoActual().GetEstado();
		}

		internal bool EsRecurso(RecursoTecnologico recurso) {
			return recurso == this;
		}

		internal Turno[] GetTurnos(DateTime desde) {
			List<Turno> turnosValidos = new List<Turno>();
			
			foreach (Turno turno in turnos) {
				if (turno.EsValidoEn(desde)) {
					turnosValidos.Add(turno);
				}
			}

			return turnosValidos.ToArray();
		}
	}
}

