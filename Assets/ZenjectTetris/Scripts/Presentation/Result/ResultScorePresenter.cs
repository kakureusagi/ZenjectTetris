using UnityEngine;
using UnityEngine.UI;
using ZenjectTetris.Domain.Result;

#pragma warning disable 649

namespace ZenjectTetris.Presentation.Result {

	public class ResultScorePresenter : PresenterBase<ResultScoreUseCase> {

		public interface IFactory : IFactoryBase<ResultScorePresenter> {

		}


		[SerializeField]
		private Text rank;

		[SerializeField]
		private Text userName;

		[SerializeField]
		private Text score;


		protected override void RunCore() {
			// from useCase.
			rank.text = useCase.Rank.ToString();
			userName.text = useCase.UserName;
			score.text = useCase.Score.ToString();

			if (useCase.IsCurrentGameScore) {
				foreach (var text in new[] {rank, userName, score}) {
					text.color = Color.blue;
				}
			}
		}
	}

}