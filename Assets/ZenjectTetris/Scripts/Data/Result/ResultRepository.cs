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

		[Inject]
		CacheUserStore cacheUserStore;

		[Inject]
		HiScoreTranslator hiScoreTranslator;


		public HiScore GetCurrentUserLastHiScore() {
			var user = cacheUserStore.GetUser();
			var hiScoreRecord = hiScoreStore.GetLastScore(user.Uuid);
			return hiScoreTranslator.Translate(hiScoreRecord);
		}

		public HiScore[] GetCurrentUserHiScores() {
			var user = cacheUserStore.GetUser();
			return hiScoreStore.GetHiScores(user.Uuid)
				.Select(record => hiScoreTranslator.Translate(record))
				.ToArray();
		}

		public HiScoreRankingData[] GetRankingData() {
			return hiScoreStore.GetAllUserHiScores()
				.Select((hiScore, i) => {
					var user = userStore.GetUserById(hiScore.Uuid);
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