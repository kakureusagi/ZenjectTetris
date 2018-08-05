namespace ZenjectTetris.Domain.Core {

	class TetrisData {

		public Field Field { get; }
		public Tetrimino CurrentTetrimino { get; set; }
		public Tetrimino NextTetrimino { get; set; }
		public float RestTime { get; set; }
		public int Score { get; set; }


		public TetrisData(Field field, float restTime) {
			Field = field;
			RestTime = restTime;
		}

	}

}