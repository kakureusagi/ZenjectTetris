namespace ZenjectTetris.Domain.Core.TetrisState {

	/// <summary>
	/// 何もしない状態.
	/// </summary>
	class Wait : Base {

		public Wait(TetrisData data) : base(data) {
		}

		public override State Update(float deltaTime) {
			return State.Wait;
		}

	}

}