using System;

namespace ZenjectTetris.Domain.Result {

	public class HiScoreRankingData {

		public int Rank { get; set; }
		public string UserName { get; set; }
		public int Score { get; set; }
		public DateTime Date { get; set; }
	}

}