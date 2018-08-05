using ZenjectTetris.Data;
using ZenjectTetris.Data.Title;
using ZenjectTetris.Domain.Tetris;
using ZenjectTetris.Presentation.Tetris;

namespace ZenjectTetris.Installer.Tetris {

	class TetrisInstaller : InstallerBase<TetrisInstaller> {

		class TetrisUseCaseFactory : UseCaseFactory<TetrisUseCase.Data, TetrisUseCase>, TetrisUseCase.IFactory {

		}


		public override void InstallBindings() {
			base.InstallBindings();

			Container.BindUseCase<TetrisUseCase.Data, TetrisUseCase, TetrisUseCaseFactory, TetrisUseCase.IFactory>();

			Container.BindRepository<TetrisRepository>();

			Container.BindInterfacesTo<Input>().AsCached();

			Container.Bind<HiScoreStore>().AsCached();
		}

	}

}