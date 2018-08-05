using UnityEngine;
using UnityEngine.Assertions;
using ZenjectTetris.Presentation.Dialog;

namespace ZenjectTetris.Domain.Dialog {

	public class DialogManager : MonoBehaviour, IDialogManager {

		public void Add<TPresenter, TUseCase>(TPresenter presenter, TUseCase useCase) where TPresenter : DialogPresenterBase<TUseCase> where TUseCase : DialogUseCaseBase {
			Assert.IsNotNull(presenter);
			Assert.IsNotNull(useCase);

			presenter.transform.SetParent(transform);
			presenter.transform.localPosition = Vector3.zero;
			presenter.Run(useCase);
		}
	}

}