using UnityEngine;
using Zenject;
using ZenjectTetris.Data;
using ZenjectTetris.Domain;

namespace ZenjectTetris.Installer {

	public static class ContainerExtensions {

		public static ConditionCopyNonLazyBinder BindPresenter<TPresenter, TPresenterFactory, TPresenterIFactory>(this DiContainer container)
			where TPresenterFactory : PlaceholderFactory<Object, TPresenter>, TPresenterIFactory
			where TPresenterIFactory : IFactory<Object, Transform, TPresenter> {
			return container
				.BindFactoryCustomInterface<Object, TPresenter, TPresenterFactory, TPresenterIFactory>()
				.FromFactory<PrefabFactory<TPresenter>>();
		}

		public static FactoryToChoiceIdBinder<TUseCase> BindUseCase<TUseCase, TUseCaseFactory, TUseCaseIFactory>(this DiContainer container)
			where TUseCase : UseCaseBase
			where TUseCaseFactory : PlaceholderFactory<TUseCase>, TUseCaseIFactory
			where TUseCaseIFactory : IFactory<TUseCase> {
			return container.BindFactoryCustomInterface<TUseCase, TUseCaseFactory, TUseCaseIFactory>();
		}

		public static FactoryToChoiceIdBinder<TData, TUseCase> BindUseCase<TData, TUseCase, TUseCaseFactory, TUseCaseIFactory>(this DiContainer container)
			where TUseCase : UseCaseBase
			where TUseCaseFactory : PlaceholderFactory<TData, TUseCase>, TUseCaseIFactory
			where TUseCaseIFactory : IFactory<TData, TUseCase> {
			return container.BindFactoryCustomInterface<TData, TUseCase, TUseCaseFactory, TUseCaseIFactory>();
		}

		public static ConcreteIdArgConditionCopyNonLazyBinder BindRepository<TRepository>(this DiContainer container) where TRepository : RepositoryBase {
			return container.BindInterfacesTo<TRepository>().AsTransient();
		}
	}

}