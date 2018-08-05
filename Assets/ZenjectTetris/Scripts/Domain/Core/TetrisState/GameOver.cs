namespace ZenjectTetris.Domain.Core.TetrisState {

	/// <summary>
	/// ゲームオーバー状態.
	/// </summary>
	class GameOver : Base {

		public override bool IsGameOver => true;

		public GameOver(TetrisData data) : base(data) {
		}

		public override State Update(float deltaTime) {
			return State.GameOver;
		}

	}

}