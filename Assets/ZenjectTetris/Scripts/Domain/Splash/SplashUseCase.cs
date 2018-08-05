using Zenject;

#pragma warning disable 649

namespace ZenjectTetris.Domain.Splash {

	public class SplashUseCase : UseCaseBase {

		public interface IFactory : IFactory<SplashUseCase> {

		}


		[Inject]
		ISceneManager sceneManager;


		public void GoNextScene() {
			sceneManager.Load(SceneName.Title);
		}

	}

}