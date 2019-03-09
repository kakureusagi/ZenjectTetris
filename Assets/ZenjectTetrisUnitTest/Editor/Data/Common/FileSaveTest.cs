using System.IO;
using MessagePack;
using NUnit.Framework;
using Zenject;
using ZenjectTetris.Data;

#pragma warning disable 649

namespace ZenjectTetrisUnitTest {

	public class FileSaveTest : ZenjectUnitTestFixture {


		[MessagePackObject(true)]
		public class Data {

			public int a;

		}


		string rootPath;
		Data data;

		[Inject]
		FileSave _fileSave;


		[OneTimeSetUp]
		public void OnetimeSetUp() {
			rootPath = Path.GetTempPath() + GetType().Name;
			Directory.CreateDirectory(rootPath);
		}

		[OneTimeTearDown]
		public void OneTimeTeaDown() {
			Directory.Delete(rootPath, true);
		}

		[SetUp]
		public override void Setup() {
			base.Setup();
			Container.Bind<FileSave>().AsTransient().WithArguments(rootPath);
			Container.Inject(this);

			data = new Data {a = 100};
		}

		[TearDown]
		public override void Teardown() {
			base.Teardown();
		}


		[Test]
		public void 保存できる() {
			var path = "save.txt";
			_fileSave.Save(path, data);
			Assert.IsTrue(File.Exists($"{rootPath}/{path}"));
		}

		[Test]
		public void 保存したファイルの存在を確認できる() {
			var path = "exists.txt";
			_fileSave.Save(path, data);
			Assert.IsTrue(_fileSave.Exists(path));
		}

		[Test]
		public void 保存したファイルを読み込める() {
			var path = "read.txt";
			_fileSave.Save(path, data);
			var readData = _fileSave.Load<Data>(path);
			Assert.AreEqual(readData.a, 100);
		}

		[Test]
		public void ファイルを削除できる() {
			var path = "delete.txt";
			_fileSave.Save(path, data);
			Assert.IsTrue(File.Exists($"{rootPath}/{path}"));
			_fileSave.Delete(path);
			Assert.IsFalse(File.Exists($"{rootPath}/{path}"));
		}

	}

}