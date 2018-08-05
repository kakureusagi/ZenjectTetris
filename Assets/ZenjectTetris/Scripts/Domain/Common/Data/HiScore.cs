using System;
using MessagePack;

namespace ZenjectTetris.Domain.Data {

	[MessagePackObject(true)]
	public class HiScore {

		public string Uuid { get; set; }
		public int Score { get; set; }
		public DateTime Date { get; set; }
	}

}