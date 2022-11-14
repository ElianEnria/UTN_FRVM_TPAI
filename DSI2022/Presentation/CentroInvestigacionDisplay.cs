using DSI2022.Business;

namespace DSI2022.Presentation {
	public class CentroInvestigacionDisplay {
		public CentroInvestigacion from;
		public string nombre;
		public RecursoTecnologicoDisplay[] recursos;

		public CentroInvestigacionDisplay(CentroInvestigacion from, string nombre, RecursoTecnologicoDisplay[] recursos) {
			this.from = from;
			this.nombre = nombre;
			this.recursos = recursos;
		}
	}
}
