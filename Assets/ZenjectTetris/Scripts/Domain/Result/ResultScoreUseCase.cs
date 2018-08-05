using Zenject;

#pragma warning disable 649

namespace ZenjectTetris.Domain.Result {

	public class ResultScoreUseCase : UseCaseBase {

		public struct Data {
			public bool IsCurrentGameScore { get; set; }
			public HiScoreRankingData RankingData { get; set; }
		}

		public interface IFactory : IFactory<Data, ResultScoreUseCase> {

		}


		[Inject]
		Data data;


		public int Rank => data.RankingData.Rank;
		public string UserName => data.RankingData.UserName;
		public int Score => data.RankingData.Score;
		public bool IsCurrentGameScore => data.IsCurrentGameScore;

	}

}