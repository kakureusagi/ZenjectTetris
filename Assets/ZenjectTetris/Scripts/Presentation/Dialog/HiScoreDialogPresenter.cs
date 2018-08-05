using UniRx;
using UnityEngine;
using UnityEngine.UI;
using ZenjectTetris.Domain.Dialog;

#pragma warning disable 649

namespace ZenjectTetris.Presentation.Dialog {

	public class HiScoreDialogPresenter : DialogPresenterBase<HiScoreDialogUseCase> {

		[SerializeField]
		Text score;

		[SerializeField]
		Button closeButton;


		protected override void RunCore() {
			base.RunCore();

			// from useCase.
			score.text = useCase.HiScore.ToString();

			// from view.
			closeButton.OnClickAsObservable()
				.Subscribe(_ => useCase.OnCloseButton())
				.AddTo(this);
		}
	}

}