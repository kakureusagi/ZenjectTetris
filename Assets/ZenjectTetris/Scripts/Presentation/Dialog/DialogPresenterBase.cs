using UniRx;
using UniRx.Async;
using UnityEngine;
using ZenjectTetris.Presentation;

#pragma warning disable 649

namespace ZenjectTetris.Domain.Dialog {

	public class DialogPresenterBase<TUseCase> : PresenterBase<TUseCase> where TUseCase : DialogUseCaseBase {

		[SerializeField]
		DialogPerformerBase performer;


		protected override void RunCore() {
			useCase.OnCloseStart
				.Subscribe(_ => StartCloseAnimation())
				.AddTo(this);
		}

		private async UniTask StartCloseAnimation() {
			await performer.Close();
			useCase.OnCloseAnimationEnd();
			Destroy(gameObject);
		}
	}

}