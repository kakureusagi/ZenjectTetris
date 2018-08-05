using ZenjectTetris.Domain.Dialog;

namespace ZenjectTetris.Presentation.Dialog {

	public interface IDialogManager {

		void Add<TPresenter, TUseCase>(TPresenter presenter, TUseCase useCase)
			where TPresenter : DialogPresenterBase<TUseCase>
			where TUseCase : DialogUseCaseBase;
	}

}