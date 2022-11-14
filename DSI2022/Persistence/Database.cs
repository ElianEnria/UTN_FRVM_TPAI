using DSI2022.Business;

namespace DSI2022.Persistence {
	public static class Database {
		private static string repositoryBase = "C:\\Users\\elian\\OneDrive\\Escritorio\\DSI\\DSI\\DSI2022\\DSI2022\\Repositories";
		private static string centrosInvestigacionPath = Path.Combine(repositoryBase, "CentrosInvestigacion.json");
		private static string tiposRecursosTecnologicosPath = Path.Combine(repositoryBase, "TiposRT.json");
		private static string estadosPath = Path.Combine(repositoryBase, "Estados.json");

		public static TipoRecursoTecnologico[] FetchTiposRT() {
			Repository<TipoRecursoTecnologico> tipos =
				new Repository<TipoRecursoTecnologico>(tiposRecursosTecnologicosPath);

			return tipos.ToArray();
		}

		public static Estado[] FetchEstados() {
			Repository<Estado> estados =
				new Repository<Estado>(estadosPath);

			return estados.ToArray();
		}

		public static CentroInvestigacion[] FetchCentrosInvestigacion() {
			Repository<CentroInvestigacion> centros =
				new Repository<CentroInvestigacion>(centrosInvestigacionPath);

			return centros.ToArray();
		}

		public static void asd(CentroInvestigacion[] centrosMemoria)
		{
			GenerarTurnosParaCI(centrosMemoria);
		}


		private static void GenerarTurnosParaCI(CentroInvestigacion[] centrosMemoria) {

			Repository<CentroInvestigacion> centros = new Repository<CentroInvestigacion>(centrosInvestigacionPath);
			for (int i = 0; i < centrosMemoria.Length; i++)
			{
				centros[i] = centrosMemoria[i];	
			}
			centros.Commit();
		}
	}
}
