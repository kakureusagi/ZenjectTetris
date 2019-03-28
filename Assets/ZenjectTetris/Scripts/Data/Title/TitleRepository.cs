using Zenject;
using ZenjectTetris.Domain.Title;

#pragma warning disable 649

namespace ZenjectTetris.Data.Title {

	public class TitleRepository : RepositoryBase, ITitleRepository {

		[Inject]
		UserStore userStore;

		[Inject]
		CacheUserStore cacheUserStore;

		[Inject]
		UserTranslator userTranslator;


		public bool ExistsUser(string userName) {
			return userStore.ExistsUser(userName);
		}

		public void CreateUser(string userName) {
			userStore.CreateUser(userName);
		}

		public void RemoveUser(string userName) {
			userStore.RemoveUser(userName);
		}

		public void Login(string userName) {
			var userRecord = userStore.GetUserByName(userName);
			var user = userTranslator.Translate(userRecord);
			cacheUserStore.SetUser(user);
		}

	}

}