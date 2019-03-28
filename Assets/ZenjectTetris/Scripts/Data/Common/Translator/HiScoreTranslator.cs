using ZenjectTetris.Domain.Data;

namespace ZenjectTetris.Data {

	public class HiScoreTranslator {

		public HiScore Translate(HiScoreRecord record) {
			return new HiScore {
				Uuid = record.Uuid,
				Score = record.Score,
				Date = record.Date,
			};
		}
	}

}