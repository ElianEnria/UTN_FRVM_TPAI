using DSI2022.Business;

namespace DSI2022.Security {
	static class SessionManager {
		private static AsignacionCientificoDelCI loggedIn;

		public static void Login(AsignacionCientificoDelCI usuario) {
			loggedIn = usuario;
		}

		public static void Logout() {
			loggedIn = null;
		}

		public static Usuario GetUsuario() {
			return loggedIn.Cientifico.Usuario;
		}

		public static AsignacionCientificoDelCI GetAsignacionCientifico() {
			return loggedIn;
		}

		public static bool IsAuthorized(Usuario usuario) {
			return usuario.Habilitado;
		}

		internal static PersonalCientifico GetCientifico() {
			return loggedIn.Cientifico;
		}
	}
}
