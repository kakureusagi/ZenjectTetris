using UniRx;
using Zenject;
using ZenjectTetris.Domain.Core;

#pragma warning disable 649

namespace ZenjectTetris.Domain.Tetris {

	public class TetrisUseCase : UseCaseBase {

		public interface IFactory : IFactory<Data, TetrisUseCase> {

		}

		public struct Data {
			public Difficulty Difficulty { get; set; }
		}


		public IReadOnlyReactiveProperty<TetriminoColor[,]> FieldColors => tetrisFacade.FieldColors;
		public IReadOnlyReactiveProperty<TetriminoColor[,]> NextTetrimino => tetrisFacade.NextColors;

		public IReadOnlyReactiveProperty<float> RestTime => tetrisFacade.RestTime;
		public IReadOnlyReactiveProperty<int> Score => tetrisFacade.Score;
		public IReadOnlyReactiveProperty<bool> IsGameOver => tetrisFacade.IsGameOver;

		public float MaxRestTime => tetrisFacade.MaxRestTime;


		[Inject]
		Data data;

		[Inject]
		TetrisSettings settings;

		[Inject]
		ISceneManager sceneManager;

		[Inject]
		ITetrisRepository repository;


		TetrisFacade tetrisFacade;


		protected override void RunCore() {
			var difficultySettings = settings.GetDifficultySettings(data.Difficulty);
			tetrisFacade = new TetrisFacade(difficultySettings);
			tetrisFacade.Run();
		}

		public void GoToResult() {
			sceneManager.Load(SceneName.Result);
		}

		public void Update(float deltaTime) {
			tetrisFacade.Update(deltaTime);
		}

		public void StartGame() {
			tetrisFacade.StartGame();

			tetrisFacade.IsGameOver
				.Where(isGameOver => isGameOver)
				.Subscribe(_ => repository.SaveCurrentUserScore(tetrisFacade.Score.Value));
		}

		public void MoveTetriminoLeft() {
			tetrisFacade.MoveLeft();
		}

		public void MoveTetriminoRight() {
			tetrisFacade.MoveRight();
		}

		public void MoveTetriminoDown() {
			tetrisFacade.MoveDown();
		}

		public void FallTetrimino() {
			tetrisFacade.Fall();
		}

		public void TurnTetriminoLeft() {
			tetrisFacade.TurnLeft();
		}

		public void TurnTetriminoRight() {
			tetrisFacade.TurnRight();
		}

	}

}