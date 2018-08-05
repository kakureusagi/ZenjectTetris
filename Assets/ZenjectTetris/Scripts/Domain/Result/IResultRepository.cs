using ZenjectTetris.Domain.Data;

namespace ZenjectTetris.Domain.Result {

	public interface IResultRepository {

		HiScore GetCurrentUserLastHiScore();

		HiScore[] GetCurrentUserHiScores();

		HiScoreRankingData[] GetRankingData();

	}

}