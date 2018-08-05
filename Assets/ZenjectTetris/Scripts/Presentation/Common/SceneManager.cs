using System;
using System.Collections.Generic;
using UniRx.Async;
using UnityEngine.SceneManagement;
using Zenject;
using ZenjectTetris.Domain;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

#pragma warning disable 649

namespace ZenjectTetris.Presentation {

	public class SceneManager : ISceneManager {

		[Inject]
		ZenjectSceneLoader loader;

		private readonly HashSet<string> loadedSceneNames = new HashSet<string>();


		public void SetCurrentScene(SceneName sceneName) {
			var scene = UnitySceneManager.GetSceneByName(sceneName.ToString());
			UnitySceneManager.SetActiveScene(scene);
			loadedSceneNames.Add(sceneName.ToString());
		}

		public bool HasLoaded(SceneName sceneName) {
			return HasLoaded(sceneName.ToString());
		}

		public bool HasLoaded(string sceneName) {
			return loadedSceneNames.Contains(sceneName);
		}

		public async UniTask Load(SceneName sceneName) {
			await Load(sceneName.ToString(), null);
		}

		public async UniTask Load(SceneName sceneName, Action<DiContainer> extraBindings) {
			await Load(sceneName.ToString(), extraBindings);
		}

		private async UniTask Load(string sceneName, Action<DiContainer> extraBindings) {
			var currentScene = UnitySceneManager.GetActiveScene();
			await LoadAdditive(sceneName, extraBindings);

			var nextScene = UnitySceneManager.GetSceneByName(sceneName);
			UnitySceneManager.SetActiveScene(nextScene);

			await UnitySceneManager.UnloadSceneAsync(currentScene);
		}

		public async UniTask LoadAdditive(SceneName sceneName) {
			await LoadAdditive(sceneName.ToString(), null);
		}

		public async UniTask LoadAdditive(SceneName sceneName, Action<DiContainer> extraBindings) {
			await LoadAdditive(sceneName.ToString(), extraBindings);
		}

		private async UniTask LoadAdditive(string sceneName, Action<DiContainer> extraBindings) {
			await loader.LoadSceneAsync(sceneName, LoadSceneMode.Additive, extraBindings);
			loadedSceneNames.Add(sceneName);
		}

		public async UniTask Unload(SceneName sceneName) {
			await Unload(sceneName.ToString());
		}

		private async UniTask Unload(string sceneName) {
			await UnitySceneManager.UnloadSceneAsync(sceneName);
			loadedSceneNames.Remove(sceneName);
		}

	}

}