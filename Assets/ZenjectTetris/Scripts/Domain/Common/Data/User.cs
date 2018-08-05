using MessagePack;

namespace ZenjectTetris.Domain.Data {

	[MessagePackObject(true)]
	public class User {

		public string Uuid { get; set; }
		public string UserName { get; set; }

	}

}