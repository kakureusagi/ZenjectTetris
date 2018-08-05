using UnityEngine.Assertions;
using Zenject;

#pragma warning disable 649

namespace ZenjectTetris.Domain.Debug {

	public class TestUserUseCase : UseCaseBase {

		public interface IFactory : IFactory<TestUserUseCase> {

		}


		[Inject]
		ITestUserRepository repository;


		public void Login() {
			var userName = repository.GetUserName();
			if (string.IsNullOrEmpty(userName)) {
				userName = "No Name";
			}

			if (!repository.ExistsUser(userName)) {
				repository.CreateUser(userName);
			}

			repository.Login(userName);
		}

		public string GetUserName() {
			return repository.GetUserName();
		}

		public void SetUserName(string userName) {
			Assert.IsNotNull(userName);
			repository.SetUserName(userName);
		}

	}

}