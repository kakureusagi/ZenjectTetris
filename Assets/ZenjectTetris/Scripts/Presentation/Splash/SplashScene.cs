using UnityEngine;
using Zenject;
using ZenjectTetris.Domain;
using ZenjectTetris.Domain.Splash;

#pragma warning disable 649

namespace ZenjectTetris.Presentation.Splash {

	class SplashScene : SceneBase {

		[SerializeField]
		SplashPresenter presenter;


		[Inject]
		SplashUseCase.IFactory factory;

		[Inject]
		ISceneManager sceneManager;


		public override void Run() {
			var useCase = factory.Create();
			useCase.Run();
			presenter.Run(useCase);
		}

	}

}