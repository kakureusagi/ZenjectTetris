namespace ZenjectTetris.Domain.Core.TetrisState {

	/// <summary>
	/// テトリミノが地面に設置した状態.
	/// </summary>
	class Ground : Base {

		readonly ScoreCalculator scoreCalculator = new ScoreCalculator();
		readonly float interval;
		float time;


		public Ground(TetrisData data, float interval) : base(data) {
			this.interval = interval;
		}

		public override void OnEnter() {
			time = interval;
		}

		public override State Update(float deltaTime) {
			time -= deltaTime;
			if (time > 0) {
				return State.Ground;
			}

			var field = data.Field;
			if (field.CanDelete()) {
				int deleteLine = field.DeleteLines();
				int score = scoreCalculator.Calculate(deleteLine);
				data.Score += score;
			}

			return State.UserOperation;
		}

	}

}