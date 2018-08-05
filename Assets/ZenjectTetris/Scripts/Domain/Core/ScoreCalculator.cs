using UnityEngine.Assertions;

namespace ZenjectTetris.Domain.Core {

	public class ScoreCalculator {

		static readonly int[] Scores = {100, 300, 500, 800};

		public int Calculate(int deleteLine) {
			Assert.IsTrue(0 < deleteLine && deleteLine <= Scores.Length);
			return Scores[deleteLine - 1];
		}
	}

}