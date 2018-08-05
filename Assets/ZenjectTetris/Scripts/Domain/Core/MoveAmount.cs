namespace ZenjectTetris.Domain.Core {

	struct MoveAmount {

		public int X { get; }
		public int Y { get; }

		public MoveAmount(int x, int y) {
			X = x;
			Y = y;
		}

	}

}