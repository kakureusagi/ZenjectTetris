using ZenjectTetris.Domain.Data;

namespace ZenjectTetris.Data {

	public class UserTranslator {

		public User Translate(UserRecord record) {
			return new User {
				Uuid = record.Uuid,
				UserName = record.UserName,
			};
		}
	}

}