using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using ZenjectTetris.Domain.Result;

#pragma warning disable 649

namespace ZenjectTetris.Presentation.Result {

	class ResultPresenter : PresenterBase<ResultUseCase> {

		[Inject]
		ResultScorePresenter.IFactory scoreFactory;


		[SerializeField]
		private Transform scoresRoot;

		[SerializeField]
		Text currentScore;

		[SerializeField]
		ResultScorePresenter scorePresenterPrefab;

		[SerializeField]
		private Button goToTitleButton;


		protected override void RunCore() {
			// from useCase.
			currentScore.text = useCase.CurrentGameScore.Score.ToString();

			foreach (var childUseCase in useCase.ScoreUseCases) {
				var childPresenter = scoreFactory.Create(scorePresenterPrefab, scoresRoot);
				childPresenter.Run(childUseCase);
			}

			// from view.
			goToTitleButton.OnClickAsObservable()
				.Subscribe(_ => useCase.GoToTitle())
				.AddTo(this);
		}
	}

}