namespace ZenjectTetris.Domain.Core.TetrisState {

	/// <summary>
	/// コアゲームの状態遷移を基本クラス.
	/// </summary>
	abstract class Base {

		public virtual bool IsGameOver => false;

		protected readonly TetrisData data;


		protected Base(TetrisData data) {
			this.data = data;
		}

		public virtual void OnEnter() {
		}

		public virtual void OnExit() {
		}

		public abstract State Update(float deltaTime);

	}

}