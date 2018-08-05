using ZenjectTetris.Domain.Splash;

namespace ZenjectTetris.Installer.Splash {

	class SplashInstaller : InstallerBase<SplashInstaller> {

		class SplashUseCaseFactory : UseCaseFactory<SplashUseCase>, SplashUseCase.IFactory {

		}


		public override void InstallBindings() {
			base.InstallBindings();
			Container.BindUseCase<SplashUseCase, SplashUseCaseFactory, SplashUseCase.IFactory>();
		}
	}

}