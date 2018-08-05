using ZenjectTetris.Data.Title;
using ZenjectTetris.Domain.Title;

namespace ZenjectTetris.Installer.Title {

	class TitleInstaller : InstallerBase<TitleInstaller> {

		class TitleUseCaseFactory : UseCaseFactory<TitleUseCase>, TitleUseCase.IFactory {

		}


		public override void InstallBindings() {
			base.InstallBindings();
			Container.BindUseCase<TitleUseCase, TitleUseCaseFactory, TitleUseCase.IFactory>();
			Container.BindRepository<TitleRepository>();
		}

	}

}