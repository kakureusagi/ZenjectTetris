using Zenject;
using ZenjectTetris.Domain;

namespace ZenjectTetris.Presentation {

	public class GameTimer : IGameTimer, ITickable {

		public float DeltaTime { get; private set; }
		public float Time { get; private set; }
		public float TimeScale { get; private set; }


		public void Tick() {
			DeltaTime = UnityEngine.Time.deltaTime;
			Time = UnityEngine.Time.time;
			TimeScale = UnityEngine.Time.timeScale;
		}

	}

}