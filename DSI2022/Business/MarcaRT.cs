namespace DSI2022.Business {
	public class MarcaRT {
		private string nombre;

		public string Nombre { get => nombre; set => nombre = value; }

		public MarcaRT(string nombre) {
			this.nombre = nombre;
		}

		internal string GetNombre() {
			return nombre;
		}
	}
}
