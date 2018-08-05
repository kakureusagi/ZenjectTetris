namespace ZenjectTetris.Domain {

	public interface IGameTimer {

		float DeltaTime { get; }
		float Time { get; }
		float TimeScale { get; }

	}

}