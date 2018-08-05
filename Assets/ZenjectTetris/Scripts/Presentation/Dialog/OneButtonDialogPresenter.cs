using UniRx;
using UnityEngine;
using UnityEngine.UI;
using ZenjectTetris.Domain.Dialog;

#pragma warning disable 649

namespace ZenjectTetris.Presentation.Dialog {

	public class OneButtonDialogPresenter : DialogPresenterBase<OneButtonDialogUseCase> {

		[SerializeField]
		Button closeButton;


		protected override void RunCore() {
			base.RunCore();

			closeButton.OnClickAsObservable()
				.Subscribe(_ => useCase.OnCloseButton())
				.AddTo(this);
		}
	}

}