using UniRx;
using UnityEngine.Assertions;
using Zenject;
using ZenjectTetris.Domain.Core;
using ZenjectTetris.Domain.Tetris;

#pragma warning disable 649

namespace ZenjectTetris.Domain.Title {

	public class TitleUseCase : UseCaseBase {

		public interface IFactory : IFactory<TitleUseCase> {

		}


		[Inject]
		ITitleRepository repository;

		[Inject]
		ISceneManager sceneManager;


		public IReadOnlyReactiveProperty<bool> CreateUserButtonEnabled => createUserButtonEnabled;
		readonly ReactiveProperty<bool> createUserButtonEnabled = new ReactiveProperty<bool>();

		public IReadOnlyReactiveProperty<bool> StartButtonEnabled => startButtonEnabled;
		readonly ReactiveProperty<bool> startButtonEnabled = new ReactiveProperty<bool>();

		public IReadOnlyReactiveProperty<string> UserName => userName;
		readonly ReactiveProperty<string> userName = new ReactiveProperty<string>("");

		public IReadOnlyReactiveProperty<Difficulty> Difficulty => difficulty;
		readonly ReactiveProperty<Difficulty> difficulty = new ReactiveProperty<Difficulty>(Core.Difficulty.Normal);


		public void SetUserName(string userName) {
			Assert.IsNotNull(userName);

			if (userName == "") {
				this.userName.Value = userName;
				startButtonEnabled.Value = false;
				createUserButtonEnabled.Value = false;
				return;
			}

			this.userName.Value = userName;
			var useExists = repository.ExistsUser(userName);
			startButtonEnabled.Value = useExists;
			createUserButtonEnabled.Value = !useExists;
		}

		public void SetDifficulty(Difficulty difficulty) {
			this.difficulty.Value = difficulty;
		}

		public void CreateUser() {
			if (!createUserButtonEnabled.Value) {
				return;
			}

			repository.CreateUser(userName.Value);
			createUserButtonEnabled.Value = false;
			startButtonEnabled.Value = true;
		}

		public void StartGame() {
			if (!startButtonEnabled.Value) {
				return;
			}

			repository.Login(userName.Value);
			var sceneData = new TetrisSceneData {
				Difficulty = difficulty.Value,
			};
			sceneManager.Load(SceneName.Tetris, container => container.BindInstance(sceneData));
		}

	}

}