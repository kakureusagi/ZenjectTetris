using System.Linq;
using Zenject;
using ZenjectTetris.Domain.Data;
using ZenjectTetris.Domain.Result;

#pragma warning disable 649

namespace ZenjectTetris.Data.Result {

	public class ResultRepository : RepositoryBase, IResultRepository {

		[Inject]
		UserStore userStore;

		[Inject]
		HiScoreStore hiScoreStore;


		public HiScore GetCurrentUserLastHiScore() {
			var user = userStore.GetLoggedInUser();
			return hiScoreStore.GetLastScore(user.Uuid);
		}

		public HiScore[] GetCurrentUserHiScores() {
			var user = userStore.GetLoggedInUser();
			return hiScoreStore.GetHiScores(user.Uuid);
		}

		public HiScoreRankingData[] GetRankingData() {
			return hiScoreStore.GetAllUserHiScores()
				.Select((hiScore, i) => {
					var user = userStore.GetUser(hiScore.Uuid);
					return new HiScoreRankingData {
						Rank = i + 1,
						Score = hiScore.Score,
						UserName = user.UserName,
						Date = hiScore.Date,
					};
				})
				.ToArray();
		}
	}

}