using DSI2022.Business;

namespace DSI2022.Presentation {
	public class RecursoTecnologicoDisplay {
		public string numero;
		public string marca;
		public string modelo;
		public string estado;
		public RecursoTecnologico from;

		public RecursoTecnologicoDisplay(RecursoTecnologico from, string numero, string marca, string modelo, string estado) {
			this.numero = numero;
			this.marca = marca;
			this.modelo = modelo;
			this.estado = estado;
			this.from = from;
		}
	}
}
