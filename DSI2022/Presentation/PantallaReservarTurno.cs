using DSI2022.Business;

namespace DSI2022.Presentation {
	public partial class PantallaReservarTurno : Form {
		private Dictionary<string, Color> COLORES_ESTADOS = new Dictionary<string, Color>();
		private Dictionary<TurnoDiaDisplay.TurnoDiaEstado, Color> COLORES_TURNODIA_ESTADO = new Dictionary<TurnoDiaDisplay.TurnoDiaEstado, Color>();
		private Dictionary<TurnoDisplay.Estado, Color> COLORES_ESTADO_TURNO = new Dictionary<TurnoDisplay.Estado, Color>();
		GestorRegistrarReservaTurno gestor;

		public PantallaReservarTurno() {
			InitializeComponent();
			COLORES_ESTADOS.Add("Disponible", Color.Blue);
			COLORES_ESTADOS.Add("En Mantenimiento", Color.Green);
			COLORES_ESTADOS.Add("Mantenimiento Correctivo", Color.Gray);

			COLORES_TURNODIA_ESTADO.Add(TurnoDiaDisplay.TurnoDiaEstado.LIBRE, Color.Blue);
			COLORES_TURNODIA_ESTADO.Add(TurnoDiaDisplay.TurnoDiaEstado.OCUPADO, Color.Red);

			COLORES_ESTADO_TURNO.Add(TurnoDisplay.Estado.DISPONIBLE, Color.Blue);
			COLORES_ESTADO_TURNO.Add(TurnoDisplay.Estado.PENDIENTE_CONFIRMACION, Color.Gray);
			COLORES_ESTADO_TURNO.Add(TurnoDisplay.Estado.RESERVADO, Color.Red);

			HabilitarPantalla();
		}

		private void HabilitarPantalla() {
			lsvRecursosTecnologicos.Items.Clear();
			lsvRecursosTecnologicos.Groups.Clear();
			gestor = new GestorRegistrarReservaTurno(this);
			gestor.OpcionReservarTurno();
		}

		public void SolicitarTipoRT(TipoRecursoTecnologico[] tiposDisponibles) {
			PoblarCMBTipoRecursosTecnologicos(tiposDisponibles);
		}

		private void PoblarCMBTipoRecursosTecnologicos(TipoRecursoTecnologico[] tiposDisponibles) {
			cmbTipoRT.Items.Clear();
			cmbTipoRT.SelectedIndex = -1;

			foreach (TipoRecursoTecnologico tipo in tiposDisponibles) {
				ComboBoxItem newItem = new ComboBoxItem();
				newItem.text = tipo.GetNombre();
				newItem.value = tipo;
				cmbTipoRT.Items.Add(newItem);
			}
		}

		private void cmbTipoRT_SelectedIndexChanged(object sender, EventArgs e) {
			btnSeleccionarTipoRecursoTecnologico.Enabled = (cmbTipoRT.SelectedIndex >= 0);
		}

		internal void SolicitarSeleccionRT(List<CentroInvestigacionDisplay> cIDisplay) {
			lsvRecursosTecnologicos.Enabled = true;
			lsvRecursosTecnologicos.Items.Clear();
			lsvRecursosTecnologicos.Groups.Clear();

			foreach (CentroInvestigacionDisplay display in cIDisplay) {
				CrearListViewCentroInvestigacion(display);
			}
		}

		private ListViewGroup CrearListViewCentroInvestigacion(CentroInvestigacionDisplay display) {
			ListViewGroup newGroup = new ListViewGroup(display.nombre);
			lsvRecursosTecnologicos.Groups.Add(newGroup);
			newGroup.Tag = display.from;

			foreach (RecursoTecnologicoDisplay rec in display.recursos) {
				ListViewItem newItem = CrearListViewRecursoTecnologico(rec, newGroup);
				lsvRecursosTecnologicos.Items.Add(newItem);
			}

			return newGroup;
		}

		private ListViewItem CrearListViewRecursoTecnologico(RecursoTecnologicoDisplay rec, ListViewGroup group) {
			ListViewItem newItem = new ListViewItem();

			newItem.Group = group;
			newItem.Text = rec.numero;
			newItem.SubItems.Add(rec.modelo);
			newItem.SubItems.Add(rec.marca);
			newItem.SubItems.Add(rec.estado);
			newItem.Tag = rec.from;
			newItem.ForeColor = COLORES_ESTADOS[rec.estado];

			return newItem;
		}

		internal void SolicitarSeleccionTurno(TurnoDiaDisplay[] diaTurnos) {
			GenerateCalendar(diaTurnos);
		}

		private void btnSeleccionarTipoRecursoTecnologico_Click(object sender, EventArgs e) {
			ComboBoxItem seleccionado = cmbTipoRT.SelectedItem as ComboBoxItem;
			gestor.SeleccionarTipoRT(seleccionado.value as TipoRecursoTecnologico);
		}

		private void lsvRecursosTecnologicos_SelectedIndexChanged(object sender, EventArgs e) {
			btnSeleccionarRT.Enabled = (lsvRecursosTecnologicos.SelectedIndices.Count == 1);
		}

		private void btnSeleccionarRT_Click(object sender, EventArgs e) {
			CentroInvestigacion cISeleccionado = lsvRecursosTecnologicos.SelectedItems[0].Group.Tag as CentroInvestigacion;
			RecursoTecnologico seleccionado = lsvRecursosTecnologicos.SelectedItems[0].Tag as RecursoTecnologico;
			gestor.SeleccionarRecursoTecnologico(cISeleccionado, seleccionado);
		}

		private void GenerateCalendar(TurnoDiaDisplay[] dias) {
			ClearCalendar();

			foreach (TurnoDiaDisplay dia in dias) {
				Button newButton = new Button();

				newButton.Text = dia.GetNumeroDia();
				newButton.Margin = btnDefaultCalendarButton.Margin;
				newButton.Size = btnDefaultCalendarButton.Size;
				newButton.ForeColor = COLORES_TURNODIA_ESTADO[dia.CalcularDisponibilidad()];
				newButton.Tag = dia;
				newButton.Click += BtnTurno_Clicked;

				flpCalendario.Controls.Add(newButton);
			}
		}

		private void ClearCalendar() {
			flpCalendario.Controls.Clear();
		}

		private void BtnTurno_Clicked(object sender, EventArgs e) {
			Button btn = sender as Button;

			if (btn != null && btn.Tag != null) {
				PopularListaTurnos(btn.Tag as TurnoDiaDisplay);
			}
		}

		private void PopularListaTurnos(TurnoDiaDisplay dia) {
			lsvTurnos.Items.Clear();

			foreach (TurnoDisplay turno in dia) {
				ListViewItem newItem = new ListViewItem(turno.ToString());
				newItem.ForeColor = COLORES_ESTADO_TURNO[turno.GetEstado()];
				newItem.Tag = turno;
				lsvTurnos.Items.Add(newItem);
			}
		}

		private void lsvTurnos_SelectedIndexChanged(object sender, EventArgs e) {
			if (lsvTurnos.SelectedItems.Count != 1) {
				btnReservarTurno.Enabled = false;
				return;
			}

			TurnoDisplay turno = lsvTurnos.SelectedItems[0].Tag as TurnoDisplay;
			btnReservarTurno.Enabled = turno.EstaDisponible();
		}

		private void btnCancelar_Click(object sender, EventArgs e) {
			if (MessageBox.Show("¿Cancelar reserva de turno?", "Cancelar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) {
				Close();
			}
		}

		private void btnReservarTurno_Click(object sender, EventArgs e) {
			TurnoDisplay td = lsvTurnos.SelectedItems[0].Tag as TurnoDisplay;

			if (MostrarConfirmacion(td) == DialogResult.Yes) {
				if (MessageBox.Show("Sera informado por eMail, desea cambiar el metodo de notificacion?", "Notificacion.", MessageBoxButtons.YesNo) == DialogResult.Yes) {
					NotificacionSettings notificacionSettings = new NotificacionSettings();
					notificacionSettings.ShowDialog();
				}

				gestor.ReservarTurno(td.GetTurno());

				MessageBox.Show("Reserva Realizada");
			}
		}

		private DialogResult MostrarConfirmacion(TurnoDisplay? td) {
			return MessageBox.Show(
				"Esta reservando un turno para el " + td.GetFullDateString() + " en el recurso tecnologico " + td.GetDatosRT()
				+ "\nDesea reservar el turno?", "Reserva", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		}
	}
}
