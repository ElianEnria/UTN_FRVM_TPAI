using System.Text.Json.Serialization;

namespace DSI2022.Business {
	public class CentroInvestigacion {
        [JsonPropertyName("nombre")]
        private string nombre;
		private string siglas;
		private string direccion;
		private string edificio;
		private string piso;
		private string coordenadas;
		private string eMail;
		private string numeroResolucionCreacion;
		private DateTime fechaResolucionCreacion;
		private int reglamento;
		private DateTime fechaAlta;
		[JsonIgnore]
		private DateTime? fechaBaja;
		private string motivoBaja;
		private RecursoTecnologico[] recursosTecnologicos;
		private AsignacionCientificoDelCI[] cientificos;

		public CentroInvestigacion(string nombre, string siglas, string direccion, string edificio, string piso,
			string coordenadas, string eMail, string numeroResolucionCreacion, DateTime fechaResolucionCreacion,
			int reglamento, DateTime fechaAlta, DateTime fechaBaja, string motivoBaja, RecursoTecnologico[] recursosTecnologicos, AsignacionCientificoDelCI[] cientificos) {

			this.nombre = nombre;
			this.siglas = siglas;
			this.direccion = direccion;
			this.edificio = edificio;
			this.piso = piso;
			this.coordenadas = coordenadas;
			this.eMail = eMail;
			this.numeroResolucionCreacion = numeroResolucionCreacion;
			this.fechaResolucionCreacion = fechaResolucionCreacion;
			this.reglamento = reglamento;
			this.fechaAlta = fechaAlta;
			this.fechaBaja = fechaBaja;
			this.motivoBaja = motivoBaja;
			this.recursosTecnologicos = recursosTecnologicos;
			this.cientificos = cientificos;
		}

		public string Nombre { get => nombre; set => nombre = value; }
		public string Siglas { get => siglas; set => siglas = value; }
		public string Direccion { get => direccion; set => direccion = value; }
		public string Edificio { get => edificio; set => edificio = value; }
		public string Piso { get => piso; set => piso = value; }
		public string Coordenadas { get => coordenadas; set => coordenadas = value; }
		public string EMail { get => eMail; set => eMail = value; }
		public string NumeroResolucionCreacion { get => numeroResolucionCreacion; set => numeroResolucionCreacion = value; }
		public DateTime FechaResolucionCreacion { get => fechaResolucionCreacion; set => fechaResolucionCreacion = value; }
		public int Reglamento { get => reglamento; set => reglamento = value; }
		public DateTime FechaAlta { get => fechaAlta; set => fechaAlta = value; }
		public DateTime FechaBaja { get; set ; }

		public string MotivoBaja { get => motivoBaja; set => motivoBaja = value; }
		public RecursoTecnologico[] RecursosTecnologicos { get => recursosTecnologicos; set => recursosTecnologicos = value; }
		public AsignacionCientificoDelCI[] Cientificos { get => cientificos; set => cientificos = value; }

		internal RecursoTecnologico[] BuscarRTDeTipo(TipoRecursoTecnologico tipo) {
			List<RecursoTecnologico> validos = new List<RecursoTecnologico>();

			foreach (RecursoTecnologico recursoTecnologico in recursosTecnologicos) {
				if (recursoTecnologico.EsDeTipo(tipo) && recursoTecnologico.EsActivo()) {
					validos.Add(recursoTecnologico);
				}
			}

			return validos.ToArray();
		}

		internal bool ContieneRecurso(RecursoTecnologico recurso) {
			foreach (RecursoTecnologico rt in recursosTecnologicos) {
				if (rt.EsRecurso(recurso)) {
					return true;
				}
			}

			return false;
		}

		internal bool TrabajaCientifico(PersonalCientifico logeado) {
			foreach (AsignacionCientificoDelCI asignacion in cientificos) {
				if (asignacion.EsCientifico(logeado)) {
					return true;
				}
			}

			return false;
		}

		internal string GetNombre() {
			return Nombre;
		}
	}
}
