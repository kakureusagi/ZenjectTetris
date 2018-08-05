using UnityEngine;
using Zenject;
using ZenjectTetris.Domain;
using ZenjectTetris.Domain.Dialog;
using ZenjectTetris.Presentation.Dialog;

#pragma warning disable 649

namespace ZenjectTetris.Installer {

	/// <summary>
	/// 引数なしUseCaseFactory.
	/// </summary>
	/// <typeparam name="TUseCase"></typeparam>
	public abstract class UseCaseFactory<TUseCase> : PlaceholderFactory<TUseCase> where TUseCase : UseCaseBase {
	}

	/// <summary>
	/// 引数ありUseCaseFactory.
	/// </summary>
	/// <typeparam name="TData"></typeparam>
	/// <typeparam name="TUseCase"></typeparam>
	public abstract class UseCaseFactory<TData, TUseCase> : PlaceholderFactory<TData, TUseCase> where TUseCase : UseCaseBase {
	}

	/// <summary>
	/// PresenterFactory.
	/// </summary>
	/// <typeparam name="TPresenter"></typeparam>
	public abstract class PresenterFactory<TPresenter> : PlaceholderFactory<Object, TPresenter> where TPresenter : MonoBehaviour {
		public TPresenter Create(Object param, Transform parent) {
			var instance = base.Create(param);
			instance.transform.SetParent(parent);
			return instance;
		}
	}

	/// <summary>
	/// 引数なしDialogFactory.
	/// </summary>
	/// <typeparam name="TUseCase"></typeparam>
	/// <typeparam name="TPresenter"></typeparam>
	public abstract class DialogFactory<TUseCase, TPresenter> : PlaceholderFactory<TUseCase>
		where TPresenter : DialogPresenterBase<TUseCase>
		where TUseCase : DialogUseCaseBase {

		[Inject]
		Transform dialogRoot;

		[Inject]
		string resourcePath;

		[Inject]
		DiContainer container;

		public override TUseCase Create() {
			var useCase = base.Create();
			var presenterGameObject = container.InstantiatePrefabResource(resourcePath, dialogRoot);
			var presenter = presenterGameObject.GetComponent<TPresenter>();
			presenter.Run(useCase);
			return useCase;
		}
	}

	/// <summary>
	/// 引数ありDialogFactory.
	/// </summary>
	/// <typeparam name="TData"></typeparam>
	/// <typeparam name="TUseCase"></typeparam>
	/// <typeparam name="TPresenter"></typeparam>
	public abstract class DialogFactory<TData, TUseCase, TPresenter> : PlaceholderFactory<TData, TUseCase>
		where TPresenter : DialogPresenterBase<TUseCase>
		where TUseCase : DialogUseCaseBase {

		[Inject]
		IDialogManager dialogManager;

		[Inject]
		string resourcePath;

		[Inject]
		DiContainer container;

		public override TUseCase Create(TData data) {
			var useCase = base.Create(data);
			var presenterGameObject = container.InstantiatePrefabResource(resourcePath);
			var presenter = presenterGameObject.GetComponent<TPresenter>();
			dialogManager.Add(presenter, useCase);
			return useCase;
		}
	}

}