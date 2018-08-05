using UniRx.Async;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZenjectTetris.Domain;

namespace ZenjectTetris.Presentation.First {

	public class FirstScene : MonoBehaviour {

		async void Start() {
			await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(SceneName.System.ToString(), LoadSceneMode.Additive);
			await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(SceneName.Splash.ToString(), LoadSceneMode.Additive);
			await UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(SceneName.First.ToString());
		}
	}

}