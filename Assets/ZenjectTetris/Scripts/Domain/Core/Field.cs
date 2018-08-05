namespace ZenjectTetris.Domain.Core {

	/// <summary>
	/// マス目全体を管理.
	/// </summary>
	class Field {

		public TetriminoColor[,] Colors { get; }
		public int Width { get; }
		public int Height { get; }


		public Field(int width, int height) {
			Width = width;
			Height = height;
			Colors = new TetriminoColor[width, height];
			for (var x = 0; x < Width; x++) {
				for (var y = 0; y < Height; y++) {
					Colors[x, y] = TetriminoColor.None;
				}
			}
		}

		public bool CanDelete() {
			for (var y = 0; y < Height; y++) {
				if (CanDeleteLine(y)) {
					return true;
				}
			}

			return false;
		}

		bool CanDeleteLine(int y) {
			for (var x = 0; x < Width; x++) {
				if (Colors[x, y] == TetriminoColor.None) {
					return false;
				}
			}

			return true;
		}

		public int DeleteLines() {
			int deleteCount = 0;
			for (var y = 0; y < Height;) {
				if (!CanDeleteLine(y)) {
					++y;
					continue;
				}

				// 消えるラインの上にあるのを、すべての１行下にずらす.
				for (var addY = 1; addY < Height - y; addY++) {
					for (var x = 0; x < Width; x++) {
						Colors[x, y + addY - 1] = Colors[x, y + addY];
					}
				}

				++deleteCount;
			}

			return deleteCount;
		}

		public void SetTetrimino(int x, int y, TetriminoColor[,] colors) {
			var width = colors.GetLength(0);
			var height = colors.GetLength(1);
			for (var colorX = 0; colorX < width; colorX++) {
				for (var colorY = 0; colorY < height; colorY++) {
					var color = colors[colorX, colorY];
					var fieldX = x + colorX;
					var fieldY = y + colorY;
					if (!CanSetColor(color, fieldX, fieldY)) {
						continue;
					}

					Colors[fieldX, fieldY] = color;
				}
			}
		}

		bool CanSetColor(TetriminoColor color, int fieldX, int fieldY) {
			if (color == TetriminoColor.None) {
				return false;
			}

			if (fieldX < 0 || Width <= fieldX || fieldY < 0 || Height <= fieldY) {
				return false;
			}

			return true;
		}

	}

}