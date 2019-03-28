using UnityEngine;
using Zenject;
using ZenjectTetris.Data;
using ZenjectTetris.Domain.Debug;
using ZenjectTetris.Presentation;

namespace ZenjectTetris.Installer {

	public class SystemInstaller : InstallerBase<SystemInstaller> {

		class TestUserUseCaseFactory : UseCaseFactory<TestUserUseCase>, TestUserUseCase.IFactory {

		}

		public override void InstallBindings() {
			base.InstallBindings();
			InstallBindingsCore(Container);
		}

		public static void InstallBindingsCore(DiContainer container) {
			// domain.
			container.BindInterfacesTo<SceneManager>().AsCached();

			// presentation.
			container.BindExecutionOrder<GameTimer>(-1000000);
			container.BindInterfacesTo<GameTimer>().AsCached();

			// data.
			container.BindInterfacesTo<FileSave>().AsCached().WithArguments(Application.temporaryCachePath);
			container.Bind<CacheUserStore>().AsCached();
			container.Bind<UserStore>().AsCached();
			container.Bind<UserTranslator>().AsCached();
			container.Bind<HiScoreTranslator>().AsCached();

			// test.
			container.BindUseCase<TestUserUseCase, TestUserUseCaseFactory, TestUserUseCase.IFactory>();
			container.BindRepository<TestUserRepository>();
			container.Bind<TestUserNameStore>().AsCached();
		}
	}

}