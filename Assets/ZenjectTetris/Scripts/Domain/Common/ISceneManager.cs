using System;
using UniRx.Async;
using Zenject;

namespace ZenjectTetris.Domain {

	public interface ISceneManager {

		void SetCurrentScene(SceneName sceneName);
		bool HasLoaded(SceneName sceneName);

		UniTask Load(SceneName sceneName);
		UniTask Load(SceneName sceneName, Action<DiContainer> extraBindings);

		UniTask LoadAdditive(SceneName sceneName);
		UniTask LoadAdditive(SceneName sceneName, Action<DiContainer> extraBindings);

		UniTask Unload(SceneName sceneName);

	}

}