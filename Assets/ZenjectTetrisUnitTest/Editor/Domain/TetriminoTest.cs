using System;
using NUnit.Framework;
using Zenject;
using ZenjectTetris.Data;
using ZenjectTetris.Domain.Core;

#pragma warning disable 649

namespace ZenjectTetrisUnitTest {

	public class TetriminoTest : ZenjectUnitTestFixture {

		readonly TetriminoColor[][,] leftPatterns = {
			new[,] {
				{TetriminoColor.Color_5, TetriminoColor.Color_5,},
				{TetriminoColor.Color_5, TetriminoColor.Color_5,},
			},
			new[,] {
				{TetriminoColor.None, TetriminoColor.Color_1, TetriminoColor.None, TetriminoColor.None},
				{TetriminoColor.None, TetriminoColor.Color_1, TetriminoColor.None, TetriminoColor.None},
				{TetriminoColor.None, TetriminoColor.Color_1, TetriminoColor.None, TetriminoColor.None},
				{TetriminoColor.None, TetriminoColor.Color_1, TetriminoColor.None, TetriminoColor.None},
			},
			new[,] {
				{TetriminoColor.None, TetriminoColor.Color_6, TetriminoColor.None},
				{TetriminoColor.Color_6, TetriminoColor.Color_6, TetriminoColor.Color_6},
				{TetriminoColor.None, TetriminoColor.None, TetriminoColor.None},
			},
			new[,] {
				{TetriminoColor.Color_4, TetriminoColor.None, TetriminoColor.None},
				{TetriminoColor.Color_4, TetriminoColor.Color_4, TetriminoColor.Color_4},
				{TetriminoColor.None, TetriminoColor.None, TetriminoColor.None},
			},
			new[,] {
				{TetriminoColor.None, TetriminoColor.None, TetriminoColor.Color_3},
				{TetriminoColor.Color_3, TetriminoColor.Color_3, TetriminoColor.Color_3},
				{TetriminoColor.None, TetriminoColor.None, TetriminoColor.None},
			},
			new[,] {
				{TetriminoColor.Color_2, TetriminoColor.None, TetriminoColor.None},
				{TetriminoColor.Color_2, TetriminoColor.Color_2, TetriminoColor.None},
				{TetriminoColor.None, TetriminoColor.Color_2, TetriminoColor.None},
			},
			new[,] {
				{TetriminoColor.None, TetriminoColor.None, TetriminoColor.Color_7},
				{TetriminoColor.None, TetriminoColor.Color_7, TetriminoColor.Color_7},
				{TetriminoColor.None, TetriminoColor.Color_7, TetriminoColor.None},
			}
		};

		/// <summary>
		/// leftPatternsを右回転したときの状態.
		/// </summary>
		readonly TetriminoColor[][,] rightPatterns = {
			new[,] {
				{TetriminoColor.Color_5, TetriminoColor.Color_5,},
				{TetriminoColor.Color_5, TetriminoColor.Color_5,},
			},
			new[,] {
				{TetriminoColor.None, TetriminoColor.None, TetriminoColor.None, TetriminoColor.None},
				{TetriminoColor.Color_1, TetriminoColor.Color_1, TetriminoColor.Color_1, TetriminoColor.Color_1},
				{TetriminoColor.None, TetriminoColor.None, TetriminoColor.None, TetriminoColor.None},
				{TetriminoColor.None, TetriminoColor.None, TetriminoColor.None, TetriminoColor.None},
			},
			new[,] {
				{TetriminoColor.None, TetriminoColor.Color_6, TetriminoColor.None},
				{TetriminoColor.None, TetriminoColor.Color_6, TetriminoColor.Color_6},
				{TetriminoColor.None, TetriminoColor.Color_6, TetriminoColor.None},
			},
			new[,] {
				{TetriminoColor.None, TetriminoColor.Color_4, TetriminoColor.Color_4},
				{TetriminoColor.None, TetriminoColor.Color_4, TetriminoColor.None},
				{TetriminoColor.None, TetriminoColor.Color_4, TetriminoColor.None},
			},
			new[,] {
				{TetriminoColor.None, TetriminoColor.Color_3, TetriminoColor.None},
				{TetriminoColor.None, TetriminoColor.Color_3, TetriminoColor.None},
				{TetriminoColor.None, TetriminoColor.Color_3, TetriminoColor.Color_3},
			},
			new[,] {
				{TetriminoColor.None, TetriminoColor.Color_2, TetriminoColor.Color_2},
				{TetriminoColor.Color_2, TetriminoColor.Color_2, TetriminoColor.None},
				{TetriminoColor.None, TetriminoColor.None, TetriminoColor.None},
			},
			new[,] {
				{TetriminoColor.None, TetriminoColor.None, TetriminoColor.None},
				{TetriminoColor.Color_7, TetriminoColor.Color_7, TetriminoColor.None},
				{TetriminoColor.None, TetriminoColor.Color_7, TetriminoColor.Color_7},
			}
		};

		string rootPath;
		Tetrimino data;

		[Inject]
		FileSave _fileSave;


		[OneTimeSetUp]
		public void OnetimeSetUp() {
		}

		[OneTimeTearDown]
		public void OneTimeTeaDown() {
		}

		[SetUp]
		public override void Setup() {
			base.Setup();
		}

		[TearDown]
		public override void Teardown() {
			base.Teardown();
		}

		[TestCase(0, 2, 2, 0, 0)]
		[TestCase(1, 4, 4, 1, 0)]
		[TestCase(2, 3, 3, 0, 1)]
		[TestCase(3, 3, 3, 0, 1)]
		[TestCase(4, 3, 3, 0, 1)]
		[TestCase(5, 3, 3, 0, 0)]
		[TestCase(6, 3, 3, 1, 0)]
		public void 幅や高さや最左位置や最下位置を取得できる(int patternIndex, int width, int height, int leftmost, int bottommost) {
			var tetrimino = CreateTetrimino(leftPatterns[patternIndex]);

			Assert.AreEqual(tetrimino.Width, width);
			Assert.AreEqual(tetrimino.Height, height);
			Assert.AreEqual(tetrimino.Leftmost, leftmost);
			Assert.AreEqual(tetrimino.Bottommost, bottommost);
		}

		[TestCase(0, 0)]
		[TestCase(-1, -1)]
		[TestCase(1, 1)]
		[TestCase(-3, -4)]
		[TestCase(-3, 4)]
		[TestCase(3, -4)]
		[TestCase(3, 4)]
		public void 位置を設定できる(int x, int y) {
			var tetrimino = CreateTetrimino(leftPatterns[0]);
			tetrimino.SetPosition(x, y);
			Assert.AreEqual(tetrimino.X, x);
			Assert.AreEqual(tetrimino.Y, y);
		}

		[TestCase(0, 0)]
		[TestCase(-1, -1)]
		[TestCase(1, 1)]
		[TestCase(-3, -4)]
		[TestCase(-3, 4)]
		[TestCase(3, -4)]
		[TestCase(3, 4)]
		public void 移動できる(int x, int y) {
			var tetrimino = CreateTetrimino(leftPatterns[0]);
			tetrimino.Move(x, y);
			Assert.AreEqual(tetrimino.X, x);
			Assert.AreEqual(tetrimino.Y, y);
		}

		[TestCase(0)]
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		[TestCase(4)]
		[TestCase(5)]
		[TestCase(6)]
		public void 右回転できる(int patternIndex) {
			var left = CreateTetrimino(leftPatterns[patternIndex]);
			var right = CreateTetrimino(rightPatterns[patternIndex]);

			left.TurnRight();

			CollectionAssert.AreEqual(left.TetriminoColors, right.TetriminoColors);
		}

		[TestCase(0)]
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		[TestCase(4)]
		[TestCase(5)]
		[TestCase(6)]
		public void 左回転できる(int patternIndex) {
			var left = CreateTetrimino(leftPatterns[patternIndex]);
			var right = CreateTetrimino(rightPatterns[patternIndex]);

			right.TurnLeft();

			CollectionAssert.AreEqual(left.TetriminoColors, right.TetriminoColors);
		}

		private static Tetrimino CreateTetrimino(TetriminoColor[,] original) {
			var height = original.GetLength(0);
			var width = original.GetLength(1);
			var copy = new TetriminoColor[height, width];
			Array.Copy(original, copy, height * width);
			return new Tetrimino(copy);
		}
	}

}