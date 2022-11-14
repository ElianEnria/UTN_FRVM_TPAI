namespace DSI2022.Business {
	public class TipoRecursoTecnologico {
		private string nombre;
		private string descripcion;

		public string Nombre { get => nombre; set => nombre = value; }
		public string Descripcion { get => descripcion; set => descripcion = value; }

		public TipoRecursoTecnologico(string nombre, string descripcion) {
			this.Nombre = nombre;
			this.descripcion = descripcion;
		}

		internal string GetNombre() {
			return Nombre;
		}
	}
}
