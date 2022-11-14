namespace DSI2022.Presentation {
	public class TurnoDiaDisplay : List<TurnoDisplay> {
		public enum TurnoDiaEstado { LIBRE, OCUPADO }
		public DateOnly dia;

		public TurnoDiaDisplay(DateOnly dia) {
			this.dia = dia;
		}

		internal string GetNumeroDia() {
			return dia.Day.ToString();
		}

		internal TurnoDiaEstado CalcularDisponibilidad() {
			foreach (TurnoDisplay td in this) {
				if (td.EstaDisponible()) {
					return TurnoDiaEstado.LIBRE;
				}
			}

			return TurnoDiaEstado.OCUPADO;
		}
	}
}
