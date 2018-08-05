using System.Collections.Generic;
using UniRx;
using ZenjectTetris.Domain.Core.TetrisState;

namespace ZenjectTetris.Domain.Core {

	/// <summary>
	/// テトリスの全体制御 + Facade.
	/// </summary>
	class TetrisFacade {

		public IReadOnlyReactiveProperty<TetriminoColor[,]> FieldColors => fieldColors;
		readonly ReactiveProperty<TetriminoColor[,]> fieldColors = new ReactiveProperty<TetriminoColor[,]>(new TetriminoColor[Const.FieldWidth, Const.FieldHeight]);

		public IReadOnlyReactiveProperty<TetriminoColor[,]> NextColors => nextColors;
		readonly ReactiveProperty<TetriminoColor[,]> nextColors = new ReactiveProperty<TetriminoColor[,]>();

		public IReadOnlyReactiveProperty<int> Score => score;
		readonly ReactiveProperty<int> score = new ReactiveProperty<int>();

		public IReadOnlyReactiveProperty<float> RestTime => restTime;
		readonly ReactiveProperty<float> restTime = new ReactiveProperty<float>();

		public IReadOnlyReactiveProperty<bool> IsGameOver => isGameOver;
		readonly ReactiveProperty<bool> isGameOver = new ReactiveProperty<bool>();

		public float MaxRestTime => Const.GameTime;

		readonly TetrisData data;
		readonly TetriminoFactory factory;
		readonly Input input;

		State state = State.Wait;

		readonly Dictionary<State, Base> states;


		public TetrisFacade(TetrisDifficultySettings difficultySettings) {
			input = new Input();
			factory = new TetriminoFactory(difficultySettings.TetriminoType);
			data = new TetrisData(new Field(Const.FieldWidth, Const.FieldHeight), Const.GameTime);
			states = new Dictionary<State, Base> {
				{State.Wait, new Wait(data)},
				{State.UserOperation, new UserOperation(data, factory, difficultySettings, input)},
				{State.Ground, new Ground(data, Const.NextTetriminoInterval)},
				{State.GameOver, new GameOver(data)},
			};
		}

		public void Run() {
			data.CurrentTetrimino = factory.CreateEmpty();
			data.NextTetrimino = factory.Create();
			UpdateScreen();
		}

		void ChangeState(State nextState) {
			states[state].OnExit();
			state = nextState;
			states[nextState].OnEnter();
		}

		public void StartGame() {
			ChangeState(State.UserOperation);
		}

		public void Update(float deltaTime) {
			var nextState = states[state].Update(deltaTime);
			if (nextState != state) {
				ChangeState(nextState);
			}

			UpdateScreen();
			input.Set(InputType.None, new MoveAmount(0, 0));

			isGameOver.Value = states[nextState].IsGameOver;
			restTime.Value = data.RestTime;
			score.Value = data.Score;
		}

		public void MoveLeft() {
			input.Set(InputType.Move, new MoveAmount(-1, 0));
		}

		public void MoveRight() {
			input.Set(InputType.Move, new MoveAmount(1, 0));
		}

		public void MoveDown() {
			input.Set(InputType.Move, new MoveAmount(0, -1));
		}

		public void Fall() {
			input.Set(InputType.Fall, new MoveAmount(0, 0));
		}

		public void TurnLeft() {
			input.Set(InputType.TurnLeft, new MoveAmount());
		}

		public void TurnRight() {
			input.Set(InputType.TurnRight, new MoveAmount());
		}

		void UpdateScreen() {
			var field = data.Field;
			var currentTetrimino = data.CurrentTetrimino;

			// Field更新.
			var colors = fieldColors.Value;
			for (var x = 0; x < field.Width; x++) {
				for (var y = 0; y < field.Height; y++) {
					colors[x, y] = field.Colors[x, y];
				}
			}

			// FieldにCurrentTetriminoを混ぜる.
			for (var x = 0; x < currentTetrimino.Width; x++) {
				for (var y = 0; y < currentTetrimino.Height; y++) {
					var color = currentTetrimino.TetriminoColors[x, y];
					if (color == TetriminoColor.None) {
						continue;
					}

					var fieldX = currentTetrimino.X + x;
					var fieldY = currentTetrimino.Y + y;
					if (fieldX < 0 || field.Width <= fieldX || fieldY < 0 || field.Height <= fieldY) {
						continue;
					}

					colors[fieldX, fieldY] = color;
				}
			}

			fieldColors.SetValueAndForceNotify(colors);
			nextColors.SetValueAndForceNotify(data.NextTetrimino.TetriminoColors);
		}

	}

}