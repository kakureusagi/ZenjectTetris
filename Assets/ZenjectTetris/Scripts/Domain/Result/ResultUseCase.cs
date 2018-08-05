using System.Linq;
using UniRx;
using Zenject;
using ZenjectTetris.Domain.Data;
using ZenjectTetris.Domain.Dialog;

#pragma warning disable 649

namespace ZenjectTetris.Domain.Result {

	public class ResultUseCase : UseCaseBase {

		public interface IFactory : IFactory<ResultUseCase> {

		}


		[Inject]
		IResultRepository repository;

		[Inject]
		ISceneManager sceneManager;

		[Inject]
		ResultScoreUseCase.IFactory scoreFactory;

		[Inject]
		HiScoreDialogUseCase.IFactory hiScoreDialogFactory;


		public IReactiveCollection<ResultScoreUseCase> ScoreUseCases { get; private set; }
		public HiScore CurrentGameScore { get; private set; }


		protected override void RunCore() {
			// テストシーンなどでは空はありうる.
			CurrentGameScore = repository.GetCurrentUserLastHiScore() ?? new HiScore();

			ScoreUseCases = repository.GetRankingData()
				.OrderBy(data => data.Rank)
				.Select(data => scoreFactory.Create(new ResultScoreUseCase.Data {
					RankingData = data,
					IsCurrentGameScore = data.Date == CurrentGameScore.Date,
				}))
				.ToReactiveCollection();

			// ハイスコア達成ダイアログ出すか決める.
			var lastScore = repository.GetCurrentUserHiScores()
				.OrderByDescending(data => data.Score)
				.ThenBy(data => data.Date)
				.First();
			if (lastScore != null && lastScore.Date == CurrentGameScore.Date) {
				hiScoreDialogFactory.Create(new HiScoreDialogUseCase.Data {
					Score = CurrentGameScore.Score,
				});
			}
		}

		public void GoToTitle() {
			sceneManager.Load(SceneName.Title);
		}


	}

}