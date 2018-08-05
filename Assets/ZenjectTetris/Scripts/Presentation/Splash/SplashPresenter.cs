using UniRx;
using UnityEngine;
using UnityEngine.UI;
using ZenjectTetris.Domain.Splash;

#pragma warning disable 649

namespace ZenjectTetris.Presentation.Splash {

	public class SplashPresenter : PresenterBase<SplashUseCase> {

		[SerializeField]
		Button goNextButton;


		protected override void RunCore() {
			// from useCase.
			goNextButton.OnClickAsObservable()
				.Subscribe(_ => useCase.GoNextScene())
				.AddTo(this);
		}

	}

}