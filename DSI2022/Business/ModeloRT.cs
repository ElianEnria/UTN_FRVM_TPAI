namespace DSI2022.Business {
	public class ModeloRT {
		private string nombre;
		private MarcaRT marca;

		public string Nombre { get => nombre; set => nombre = value; }
		public MarcaRT Marca { get => marca; set => marca = value; }

		public ModeloRT(string nombre, MarcaRT marca) {
			this.nombre = nombre;
			this.marca = marca;
		}

		internal string GetMarca() {
			return marca.GetNombre();
		}

		internal string GetNombre() {
			return nombre;
		}
	}
}
