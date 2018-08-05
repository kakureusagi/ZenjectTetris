using UnityEngine;
using Zenject;
using ZenjectTetris.Domain;
using ZenjectTetris.Domain.Result;

#pragma warning disable 649

namespace ZenjectTetris.Presentation.Result {

	class ResultScene : SceneBase {

		[SerializeField]
		private ResultPresenter presenter;

		[Inject]
		ResultUseCase.IFactory useCaseFactory;


		public override void Run() {
			var useCase = useCaseFactory.Create();
			useCase.Run();
			presenter.Run(useCase);
		}
	}

}