namespace ZenjectTetris.Domain {

	public abstract class UseCaseBase {

		public void Run() {
			RunCore();
		}

		protected virtual void RunCore() {
		}
	}

}