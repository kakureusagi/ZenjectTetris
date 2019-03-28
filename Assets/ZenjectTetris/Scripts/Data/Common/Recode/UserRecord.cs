using MessagePack;

namespace ZenjectTetris.Data {

	/// <summary>
	/// DBっぽい名前のついたDataレイヤー用の
	/// </summary>
	[MessagePackObject(true)]
	public class UserRecord {
		public string Uuid { get; set; }
		public string UserName { get; set; }
	}

}