using UniRx.Async;
using UnityEngine;
using Zenject;
using ZenjectTetris.Domain;
using ZenjectTetris.Domain.Debug;

#pragma warning disable 649

namespace ZenjectTetris.Presentation {

	public class SceneTester : MonoBehaviour {

		[SerializeField]
		SceneBase scene;

		[SerializeField]
		bool login = true;

		[SerializeField]
		SceneName sceneName;


		[Inject]
		ISceneManager sceneManager;

		[Inject]
		TestUserUseCase.IFactory loginUseCaseFactory;

		static bool firstScene = true;


		async void Start() {
			if (firstScene) {
				sceneManager.SetCurrentScene(sceneName);

				if (login) {
					var loginUseCase = loginUseCaseFactory.Create();
					loginUseCase.Run();
					loginUseCase.Login();

					// ネットワークでログインすることが多いでしょう.
					await UniTask.DelayFrame(1);
				}
			}

			firstScene = false;
			scene.Run();
		}
	}

}