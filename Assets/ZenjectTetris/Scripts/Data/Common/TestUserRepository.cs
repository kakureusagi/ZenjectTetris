using Zenject;
using ZenjectTetris.Domain.Debug;

#pragma warning disable 649

namespace ZenjectTetris.Data {

	public class TestUserRepository : RepositoryBase, ITestUserRepository {

		[Inject]
		TestUserNameStore testUserNameStore;

		[Inject]
		UserStore userStore;

		[Inject]
		CacheUserStore cacheUserStore;

		[Inject]
		UserTranslator userTranslator;

		public string GetUserName() {
			return testUserNameStore.GetUserName();
		}

		public void SetUserName(string userName) {
			testUserNameStore.SetUserName(userName);
		}

		public void Login(string userName) {
			var userRecord = userStore.GetUserByName(userName);
			var user = userTranslator.Translate(userRecord);
			cacheUserStore.SetUser(user);
		}

		public bool ExistsUser(string userName) {
			return userStore.ExistsUser(userName);
		}

		public void CreateUser(string userName) {
			userStore.CreateUser(userName);
		}
	}

}