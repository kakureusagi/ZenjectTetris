using UnityEngine;
using Zenject;
using ZenjectTetris.Domain;
using ZenjectTetris.Domain.Title;

#pragma warning disable 649

namespace ZenjectTetris.Presentation.Title {

	class TitleScene : SceneBase {

		[SerializeField]
		TitlePresenter presenter;

		[Inject]
		TitleUseCase.IFactory factory;


		public override void Run() {
			var useCase = factory.Create();
			useCase.Run();
			presenter.Run(useCase);
		}

	}

}