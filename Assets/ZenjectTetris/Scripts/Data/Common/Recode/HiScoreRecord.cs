using System;
using MessagePack;

namespace ZenjectTetris.Data {

	[MessagePackObject(true)]
	public class HiScoreRecord {
		public string Uuid { get; set; }
		public int Score { get; set; }
		public DateTime Date { get; set; }
	}

}