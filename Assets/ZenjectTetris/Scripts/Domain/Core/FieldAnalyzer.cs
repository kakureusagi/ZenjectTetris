using UnityEngine.Assertions;

namespace ZenjectTetris.Domain.Core {

	/// <summary>
	/// テトリミノが移動や回転を行えるかどうか判断する.
	/// </summary>
	class FieldAnalyzer {

		readonly Field field;


		public FieldAnalyzer(Field field) {
			Assert.IsNotNull(field);
			this.field = field;
		}

		public bool CanMove(Tetrimino tetrimino, int x, int y) {
			return CanSet(tetrimino, x, y);
		}

		public bool CanTurnRight(Tetrimino tetrimino) {
			// 回転させてからかぶってないか判断.
			tetrimino.TurnRight();
			var canRotation = CanSet(tetrimino, 0, 0);
			tetrimino.TurnLeft();
			return canRotation;
		}

		public bool CanTurnLeft(Tetrimino tetrimino) {
			// 回転させてからかぶってないか判断.
			tetrimino.TurnLeft();
			var canRotation = CanSet(tetrimino, 0, 0);
			tetrimino.TurnRight();
			return canRotation;
		}

		bool CanSet(Tetrimino tetrimino, int gapX, int gapY) {
			for (var x = 0; x < tetrimino.Width; x++) {
				for (var y = 0; y < tetrimino.Height; y++) {
					var color = tetrimino.TetriminoColors[x, y];
					if (color == TetriminoColor.None) {
						continue;
					}

					var fieldX = tetrimino.X + x + gapX;
					var fieldY = tetrimino.Y + y + gapY;
					if (field.Height <= fieldY) {
						continue;
					}

					if (fieldX < 0 || field.Width <= fieldX || fieldY < 0) {
						return false;
					}


					if (field.Colors[fieldX, fieldY] == TetriminoColor.None) {
						continue;
					}


					return false;
				}
			}

			return true;
		}

	}

}