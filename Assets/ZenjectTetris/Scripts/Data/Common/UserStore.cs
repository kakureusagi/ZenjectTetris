using System.Collections.Generic;
using UnityEngine.Assertions;
using Zenject;

#pragma warning disable 649

namespace ZenjectTetris.Data {

	public class UserStore {

		static readonly string SavePath = $"{typeof(UserStore).Name}.save";


		[Inject]
		IFileSave fileSave;

		Dictionary<string, UserRecord> users;


		[Inject]
		void Initialize() {
			users = fileSave.Exists(SavePath) ? fileSave.Load<Dictionary<string, UserRecord>>(SavePath) : new Dictionary<string, UserRecord>();
		}

		public UserRecord GetUserById(string uuid) {
			Assert.IsNotNull(uuid);
			return users.TryGetValue(uuid, out var user) ? user : null;
		}

		public UserRecord GetUserByName(string userName) {
			Assert.IsNotNull(userName);
			// 内部的には雑にUUIDとUserNameは一緒にしちゃってる.
			return GetUserById(userName);
		}

		public bool ExistsUser(string userName) {
			return users.ContainsKey(userName);
		}

		public void CreateUser(string userName) {
			var user = new UserRecord {
				UserName = userName,
				Uuid = userName,
			};
			users.Add(userName, user);
			fileSave.Save(SavePath, users);
		}

		public void RemoveUser(string userName) {
			users.Remove(userName);
			fileSave.Save(SavePath, users);
		}

	}

}