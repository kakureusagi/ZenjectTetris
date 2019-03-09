using UnityEngine.Assertions;

namespace ZenjectTetris.Domain.Core {

	/// <summary>
	/// テトリミノ１つ分の情報.
	/// </summary>
	public class Tetrimino {

		public TetriminoColor[,] TetriminoColors { get; }
		public int Width { get; }
		public int Height { get; }
		public int Bottommost { get; }
		public int Leftmost { get; }

		public int X { get; private set; }
		public int Y { get; private set; }

		readonly TetriminoColor[,] buffer;


		public Tetrimino(TetriminoColor[,] tetriminoColors) {
			Assert.IsNotNull(tetriminoColors);

			// 多次元配列は下の方が数字が大きい。テトリスでは上方向がyが大きく、向きが逆なので注意.
			TetriminoColors = tetriminoColors;
			Height = tetriminoColors.GetLength(0);
			Width = tetriminoColors.GetLength(1);
			buffer = new TetriminoColor[Height, Width];

			var bottommost = Height - 1;
			var leftmost = Width - 1;
			for (var y = 0; y < Height; y++) {
				for (var x = 0; x < Width; x++) {
					if (tetriminoColors[Height - 1 - y, x] == TetriminoColor.None) {
						continue;
					}

					if (y < bottommost) {
						bottommost = y;
					}

					if (x < leftmost) {
						leftmost = x;
					}
				}
			}

			Bottommost = bottommost;
			Leftmost = leftmost;
		}

		public void SetPosition(int x, int y) {
			X = x;
			Y = y;
		}

		public void Move(int x, int y) {
			X += x;
			Y += y;
		}

		public void TurnRight() {
			for (var y = 0; y < Height; y++) {
				for (var x = 0; x < Width; x++) {
					buffer[x, Height - 1 - y] = TetriminoColors[y, x];
				}
			}

			Copy(buffer, TetriminoColors);
		}

		public void TurnLeft() {
			for (var y = 0; y < Height; y++) {
				for (var x = 0; x < Width; x++) {
					buffer[Width - 1 - x, y] = TetriminoColors[y, x];
				}
			}

			Copy(buffer, TetriminoColors);
		}

		void Copy(TetriminoColor[,] from, TetriminoColor[,] to) {
			for (var y = 0; y < Height; y++) {
				for (var x = 0; x < Width; x++) {
					to[y, x] = from[y, x];
				}
			}
		}

	}

}