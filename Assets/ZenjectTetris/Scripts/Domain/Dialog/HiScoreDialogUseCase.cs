using Zenject;

namespace ZenjectTetris.Domain.Dialog {

	public class HiScoreDialogUseCase : DialogUseCaseBase {

		public interface IFactory : IFactory<Data, HiScoreDialogUseCase> {

		}

		public struct Data {
			public int Score { get; set; }
		}


		[Inject]
		Data data;


		public int HiScore => data.Score;

		public void OnCloseButton() {
			Close();
		}
	}

}