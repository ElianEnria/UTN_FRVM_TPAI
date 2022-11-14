namespace DSI2022.Business {
	public class PersonalCientifico {
		private Usuario usuario;
		private string legajo;
		private string nombre;
		private string apellido;
		private int numeroDNI;
		private string eMailInstitucional;
		private string eMailPersonal;
		private string telefonoCelular;

		public Usuario Usuario { get => usuario; set => usuario = value; }
		public string Legajo { get => legajo; set => legajo = value; }
		public string Nombre { get => nombre; set => nombre = value; }
		public string Apellido { get => apellido; set => apellido = value; }
		public int NumeroDNI { get => numeroDNI; set => numeroDNI = value; }
		public string EMailInstitucional { get => eMailInstitucional; set => eMailInstitucional = value; }
		public string EMailPersonal { get => eMailPersonal; set => eMailPersonal = value; }
		public string TelefonoCelular { get => telefonoCelular; set => telefonoCelular = value; }

		public PersonalCientifico(Usuario usuario, string legajo, string nombre, string apellido, int numeroDNI, string eMailInstitucional, string eMailPersonal, string telefonoCelular) {
			this.usuario = usuario;
			this.legajo = legajo;
			this.nombre = nombre;
			this.apellido = apellido;
			this.numeroDNI = numeroDNI;
			this.eMailInstitucional = eMailInstitucional;
			this.eMailPersonal = eMailPersonal;
			this.telefonoCelular = telefonoCelular;
		}

		internal bool EsCientifico(PersonalCientifico logeado) {
			return logeado.legajo == this.legajo;
		}

		internal string GetEmail() {
			return this.eMailPersonal;
		}
	}
}
