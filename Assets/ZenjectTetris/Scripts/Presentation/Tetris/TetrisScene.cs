using UnityEngine;
using Zenject;
using ZenjectTetris.Domain;
using ZenjectTetris.Domain.Tetris;

#pragma warning disable 649

namespace ZenjectTetris.Presentation.Tetris {

	class TetrisScene : SceneBase {


		[SerializeField]
		TetrisPresenter presenter;

		[SerializeField, InjectOptional]
		TetrisSceneData sceneData;

		[Inject]
		TetrisUseCase.IFactory useCaseFactory;


		public override void Run() {
			var useCase = useCaseFactory.Create(new TetrisUseCase.Data {
				Difficulty = sceneData.Difficulty
			});
			useCase.Run();
			presenter.Run(useCase);
		}

	}

}