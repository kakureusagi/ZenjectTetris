using System;

namespace ZenjectTetris.Domain.Data {

	public class HiScore {

		public string Uuid { get; set; }
		public int Score { get; set; }
		public DateTime Date { get; set; }
	}

}