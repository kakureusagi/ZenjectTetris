namespace ZenjectTetris.Domain.Title {

	public interface ITitleRepository {

		bool ExistsUser(string userName);

		void CreateUser(string userName);

		void RemoveUser(string userName);

		void Login(string userName);

	}

}