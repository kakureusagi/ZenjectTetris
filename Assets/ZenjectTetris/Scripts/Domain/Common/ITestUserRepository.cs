namespace ZenjectTetris.Domain.Debug {

	public interface ITestUserRepository {

		string GetUserName();
		void SetUserName(string userName);

		void Login(string userName);
		bool ExistsUser(string userName);
		void CreateUser(string userName);
	}

}