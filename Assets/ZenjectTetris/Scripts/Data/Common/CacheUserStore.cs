using UnityEngine.Assertions;
using ZenjectTetris.Domain.Data;

namespace ZenjectTetris.Data {

	public class CacheUserStore {

		User user;

		public User GetUser() {
			return user;
		}

		public void SetUser(User user) {
			Assert.IsNotNull(user);
			this.user = user;
		}
	}

}