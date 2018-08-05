using System.IO;
using MessagePack;
using UnityEngine;
using Zenject;

#pragma warning disable 649

namespace ZenjectTetris.Data {

	public class FileSave : IFileSave {

		[Inject]
		string rootPath;


		public bool Exists(string path) {
			var _path = GetPath(path);
			return File.Exists(_path);
		}

		public T Load<T>(string path) {
			var _path = GetPath(path);
			using (var stream = new FileStream(_path, FileMode.Open, FileAccess.Read)) {
				return MessagePackSerializer.Deserialize<T>(stream);
			}
		}

		public void Save<T>(string path, T data) {
			var _path = GetPath(path);
			using (var stream = new FileStream(_path, FileMode.Create, FileAccess.Write)) {
				MessagePackSerializer.Serialize<T>(stream, data);
			}

			Debug.Log(_path);
		}

		public void Delete(string path) {
			var _path = GetPath(path);
			File.Delete(_path);
		}

		string GetPath(string path) {
			return $"{rootPath}/{path}";
		}

	}

}