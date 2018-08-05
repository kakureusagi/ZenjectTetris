namespace ZenjectTetris.Data {

	public interface IFileSave {

		bool Exists(string path);

		T Load<T>(string path);

		void Save<T>(string path, T data);

		void Delete(string path);

	}

}