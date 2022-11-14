namespace DSI2022.Business {
	public class Estado {
		private string nombre;

        public string Nombre { get => nombre; set => nombre = value; }

        public Estado(string nombre) {
			this.nombre = nombre;
		}

		internal bool EsActivo() {
			return nombre != "DeBajaTecnica" && nombre != "DeBajaDefinitiva";
		}

		internal string GetNombre() {
			return nombre;
		}
	}
}
