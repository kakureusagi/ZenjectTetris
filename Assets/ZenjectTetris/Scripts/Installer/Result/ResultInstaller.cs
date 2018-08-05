using ZenjectTetris.Data;
using ZenjectTetris.Data.Result;
using ZenjectTetris.Domain.Result;
using ZenjectTetris.Presentation.Result;

namespace ZenjectTetris.Installer.Result {

	class ResultInstaller : InstallerBase<ResultInstaller> {

		class ResultUseCaseFactory : UseCaseFactory<ResultUseCase>, ResultUseCase.IFactory {

		}

		class ResultScoreUseCaseFactory : UseCaseFactory<ResultScoreUseCase.Data, ResultScoreUseCase>, ResultScoreUseCase.IFactory {

		}

		class ResultScorePresenterFactory : PresenterFactory<ResultScorePresenter>, ResultScorePresenter.IFactory {

		}


		public override void InstallBindings() {
			base.InstallBindings();

			// presentation.
			Container.BindPresenter<ResultScorePresenter, ResultScorePresenterFactory, ResultScorePresenter.IFactory>();

			// domain.
			Container.BindUseCase<ResultUseCase, ResultUseCaseFactory, ResultUseCase.IFactory>();
			Container.BindUseCase<ResultScoreUseCase.Data, ResultScoreUseCase, ResultScoreUseCaseFactory, ResultScoreUseCase.IFactory>();

			// data.
			Container.BindRepository<ResultRepository>();
			Container.Bind<HiScoreStore>().AsCached();
		}
	}

}