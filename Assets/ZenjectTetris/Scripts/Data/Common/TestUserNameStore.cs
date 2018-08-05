using MessagePack;
using UnityEngine.Assertions;
using Zenject;

#pragma warning disable 649

namespace ZenjectTetris.Data {

	public class TestUserNameStore {

		[MessagePackObject(true)]
		public class SaveData {
			public string UserName { get; set; }
		}


		static readonly string Path = "TestUserStore-SaveData";


		[Inject]
		IFileSave fileSave;

		SaveData saveData;


		[Inject]
		public void Initialize() {
			saveData = fileSave.Exists(Path) ? fileSave.Load<SaveData>(Path) : new SaveData();
		}

		public void SetUserName(string userName) {
			Assert.IsNotNull(userName);
			saveData.UserName = userName;
			fileSave.Save(Path, saveData);
		}

		public bool HasUserName() {
			return string.IsNullOrEmpty(saveData.UserName);
		}

		public string GetUserName() {
			return saveData.UserName;
		}
	}

}