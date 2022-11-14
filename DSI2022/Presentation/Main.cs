using DSI2022.Business;
using DSI2022.Persistence;
using DSI2022.Presentation;
using DSI2022.Security;

namespace DSI2022 {
	public partial class Main : Form {
		public Main() {
			InitializeComponent();
			SessionManager.Login(new AsignacionCientificoDelCI(
				new PersonalCientifico(
					new Usuario(
						"1234",
						"Usuario 1",
						true),
					"1234",
					"Alberto",
					"Johnson",
					425645786,
					"email@institucional.edu",
					"email@personal.com",
					"+54 90453153"
				),
				DateTime.Now,
				DateTime.Now)
			);
		}

		private void btnReservarTurno_Click(object sender, EventArgs e) {
			PantallaReservarTurno pantallaReservarTurno = new PantallaReservarTurno();
			pantallaReservarTurno.ShowDialog();
		}
	}
}