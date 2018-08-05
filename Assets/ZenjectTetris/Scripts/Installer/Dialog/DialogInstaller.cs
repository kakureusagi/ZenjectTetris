using UnityEngine;
using ZenjectTetris.Domain.Dialog;
using ZenjectTetris.Presentation.Dialog;

#pragma	warning disable 649

namespace ZenjectTetris.Installer.Dialog {

	class DialogInstaller : InstallerBase<DialogInstaller> {

		class OneButtonDialogFactory : DialogFactory<OneButtonDialogUseCase, OneButtonDialogPresenter>, OneButtonDialogUseCase.IFactory {

		}

		class HiScoreDialogFactory : DialogFactory<HiScoreDialogUseCase.Data, HiScoreDialogUseCase, HiScoreDialogPresenter>, HiScoreDialogUseCase.IFactory {

		}


		[SerializeField]
		DialogManager dialogManager;


		public override void InstallBindings() {
			base.InstallBindings();

			Container.BindInterfacesTo<DialogManager>().FromInstance(dialogManager);

			Container.BindUseCase<OneButtonDialogUseCase, OneButtonDialogFactory, OneButtonDialogUseCase.IFactory>()
				.WithFactoryArguments("Dialog/OneButtonDialogPresenter");

			Container.BindUseCase<HiScoreDialogUseCase.Data, HiScoreDialogUseCase, HiScoreDialogFactory, HiScoreDialogUseCase.IFactory>()
				.WithFactoryArguments("Dialog/HiScoreDialogPresenter");
		}
	}

}