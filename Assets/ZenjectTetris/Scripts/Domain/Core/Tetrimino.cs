using UnityEngine.Assertions;

namespace ZenjectTetris.Domain.Core {

	/// <summary>
	/// テトリミノ１つ分の情報.
	/// </summary>
	class Tetrimino {

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

			TetriminoColors = tetriminoColors;
			Width = tetriminoColors.GetLength(0);
			Height = tetriminoColors.GetLength(1);
			buffer = new TetriminoColor[Width, Height];

			var bottommost = Height - 1;
			var leftmost = Width - 1;
			for (var x = 0; x < Width; x++) {
				for (var y = 0; y < Height; y++) {
					if (tetriminoColors[x, y] == TetriminoColor.None) {
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
					buffer[x, y] = TetriminoColors[Height - y - 1, x];
				}
			}

			Copy(buffer, TetriminoColors);
		}

		public void TurnLeft() {
			for (var y = 0; y < Height; y++) {
				for (var x = 0; x < Width; x++) {
					buffer[x, y] = TetriminoColors[y, Width - x - 1];
				}
			}

			Copy(buffer, TetriminoColors);
		}

		void Copy(TetriminoColor[,] from, TetriminoColor[,] to) {
			for (var x = 0; x < Width; x++) {
				for (var y = 0; y < Height; y++) {
					to[x, y] = from[x, y];
				}
			}
		}

	}

}