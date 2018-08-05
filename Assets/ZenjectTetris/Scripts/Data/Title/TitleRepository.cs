using Zenject;
using ZenjectTetris.Domain.Title;

#pragma warning disable 649

namespace ZenjectTetris.Data.Title {

	public class TitleRepository : RepositoryBase, ITitleRepository {

		[Inject]
		UserStore userStore;


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
			userStore.Login(userName);
		}

	}

}