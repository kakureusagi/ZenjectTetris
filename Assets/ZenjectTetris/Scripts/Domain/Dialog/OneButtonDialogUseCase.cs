using Zenject;

namespace ZenjectTetris.Domain.Dialog {

	public class OneButtonDialogUseCase : DialogUseCaseBase {

		public interface IFactory : IFactory<OneButtonDialogUseCase> {

		}


		public void OnCloseButton() {
			Close();
		}
	}

}