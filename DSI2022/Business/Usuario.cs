namespace DSI2022.Business {
	public class Usuario {
		private string clave;
		private string nombre;
		private bool habilitado;

		public string Clave { get => clave; set => clave = value; }
		public string Nombre { get => nombre; set => nombre = value; }
		public bool Habilitado { get => habilitado; set => habilitado = value; }

		public Usuario(string clave, string nombre, bool habilitado) {
			this.clave = clave;
			this.nombre = nombre;
			this.habilitado = habilitado;
		}
	}
}
