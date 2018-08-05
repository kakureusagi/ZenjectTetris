using System.Collections.Generic;
using UnityEngine.Assertions;
using Zenject;
using ZenjectTetris.Domain.Data;

#pragma warning disable 649

namespace ZenjectTetris.Data {

	public class UserStore {

		static readonly string SavePath = typeof(UserStore).Name;


		[Inject]
		IFileSave fileSave;

		Dictionary<string, User> users;
		User currentUser;


		[Inject]
		void Initialize() {
			users = fileSave.Exists(SavePath) ? fileSave.Load<Dictionary<string, User>>(SavePath) : new Dictionary<string, User>();
		}

		public bool HasLoggedIn() {
			return currentUser != null;
		}

		public void Login(string userName) {
			Assert.IsFalse(string.IsNullOrEmpty(userName));
			currentUser = users[userName];
		}

		public User GetLoggedInUser() {
			Assert.IsNotNull(currentUser);
			return currentUser;
		}

		public User GetUser(string uuid) {
			Assert.IsNotNull(uuid);
			return users[uuid];
		}

		public bool ExistsUser(string userName) {
			return users.ContainsKey(userName);
		}

		public void CreateUser(string userName) {
			var user = new User {
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