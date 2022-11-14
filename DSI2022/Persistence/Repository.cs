using Newtonsoft.Json;

namespace DSI2022.Persistence {
	internal class Repository<T> : List<T> where T : class {
		private string filePath;

		public Repository(string filePath) {
			this.filePath = filePath;

			if (!File.Exists(filePath)) {
				FileStream fs = File.Create(filePath);
				fs.Close();
			}

			Load();
		}

		public void Commit() {
			JsonSerializer serializer = JsonSerializer.Create();
			string serialized = JsonConvert.SerializeObject(ToArray());

			using (StreamWriter sw = new StreamWriter(filePath)) {
				sw.Write(serialized);
			}
		}

		private void Load() {
			string contents = File.ReadAllText(filePath);
			if (contents == null)
				return;

			T[] values = JsonConvert.DeserializeObject<T[]>(contents);

			if (values != null) {
				AddRange(values);
			}
		}
	}
}
