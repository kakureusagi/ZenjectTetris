using UnityEngine.Assertions;

namespace ZenjectTetris.Domain.Core.TetrisState {

	/// <summary>
	/// ユーザーの操作を受け付ける状態.
	/// </summary>
	class UserOperation : Base {

		readonly TetriminoFactory factory;
		readonly TetrisDifficultySettings difficultySettings;
		readonly IInput input;

		readonly FieldAnalyzer fieldAnalyzer;

		float dropTime;
		bool isGameOver;


		public UserOperation(TetrisData data, TetriminoFactory factory, TetrisDifficultySettings difficultySettings, IInput input) : base(data) {
			Assert.IsNotNull(factory);
			Assert.IsNotNull(difficultySettings);
			Assert.IsNotNull(input);
			this.factory = factory;
			this.difficultySettings = difficultySettings;
			this.input = input;

			fieldAnalyzer = new FieldAnalyzer(data.Field);
		}

		public override void OnEnter() {
			dropTime = difficultySettings.DropInterval;
			SetCurrentTetriminoToNext();
			if (!fieldAnalyzer.CanMove(data.CurrentTetrimino, 0, 0)) {
				// 移動できない.
				isGameOver = true;
			}
		}

		public override State Update(float deltaTime) {
			if (isGameOver) {
				return State.GameOver;
			}

			data.RestTime -= deltaTime;
			if (data.RestTime <= 0) {
				isGameOver = true;
				return State.GameOver;
			}

			dropTime -= deltaTime;
			if (dropTime <= 0) {
				// 強制的に下に移動.
				dropTime = difficultySettings.DropInterval;
				return UpdateCore(InputType.Move, new MoveAmount(0, -1));
			}

			return UpdateCore(input.InputType, input.MoveAmount);
		}

		void SetCurrentTetriminoToNext() {
			data.CurrentTetrimino = data.NextTetrimino;
			data.CurrentTetrimino.SetPosition(2 - data.CurrentTetrimino.Leftmost, data.Field.Height - 1 - data.CurrentTetrimino.Bottommost);
			data.NextTetrimino = factory.Create();
		}

		State UpdateCore(InputType inputType, MoveAmount moveAmount) {
			var currentTetrimino = data.CurrentTetrimino;

			switch (inputType) {
				case InputType.TurnLeft when fieldAnalyzer.CanTurnLeft(currentTetrimino):
					data.CurrentTetrimino.TurnLeft();
					break;
				case InputType.TurnRight when fieldAnalyzer.CanTurnRight(currentTetrimino):
					data.CurrentTetrimino.TurnRight();
					break;
				case InputType.Fall: {
					while (fieldAnalyzer.CanMove(currentTetrimino, 0, -1)) {
						currentTetrimino.Move(0, -1);
					}

					data.Field.SetTetrimino(currentTetrimino.X, currentTetrimino.Y, currentTetrimino.TetriminoColors);
					return State.Ground;
				}
				case InputType.Move: {
					var canMove = fieldAnalyzer.CanMove(currentTetrimino, moveAmount.X, moveAmount.Y);
					if (moveAmount.Y < 0 && !canMove) {
						data.Field.SetTetrimino(currentTetrimino.X, currentTetrimino.Y, currentTetrimino.TetriminoColors);
						return State.Ground;
					}

					if (canMove) {
						currentTetrimino.Move(moveAmount.X, moveAmount.Y);
					}

					break;
				}
				case InputType.None:
					break;
				default:
					break;
			}

			return State.UserOperation;
		}

	}

}