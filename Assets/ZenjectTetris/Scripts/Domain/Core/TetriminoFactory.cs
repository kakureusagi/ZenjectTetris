using System;
using System.Collections.Generic;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace ZenjectTetris.Domain.Core {

	/// <summary>
	/// テトリミノの生成担当.
	/// </summary>
	class TetriminoFactory {

		public static readonly TetriminoColor[,] Empty = new TetriminoColor[0, 0];


		readonly List<TetriminoColor[,]> fourPatterns = new List<TetriminoColor[,]> {
			new TetriminoColor[2, 2] {
				{TetriminoColor.Color_5, TetriminoColor.Color_5,},
				{TetriminoColor.Color_5, TetriminoColor.Color_5,},
			},
			new TetriminoColor[4, 4] {
				{TetriminoColor.None, TetriminoColor.Color_1, TetriminoColor.None, TetriminoColor.None},
				{TetriminoColor.None, TetriminoColor.Color_1, TetriminoColor.None, TetriminoColor.None},
				{TetriminoColor.None, TetriminoColor.Color_1, TetriminoColor.None, TetriminoColor.None},
				{TetriminoColor.None, TetriminoColor.Color_1, TetriminoColor.None, TetriminoColor.None},
			},
			new TetriminoColor[3, 3] {
				{TetriminoColor.None, TetriminoColor.Color_6, TetriminoColor.None},
				{TetriminoColor.Color_6, TetriminoColor.Color_6, TetriminoColor.Color_6},
				{TetriminoColor.None, TetriminoColor.None, TetriminoColor.None},
			},
			new TetriminoColor[3, 3] {
				{TetriminoColor.Color_4, TetriminoColor.None, TetriminoColor.None},
				{TetriminoColor.Color_4, TetriminoColor.Color_4, TetriminoColor.Color_4},
				{TetriminoColor.None, TetriminoColor.None, TetriminoColor.None},
			},
			new TetriminoColor[3, 3] {
				{TetriminoColor.None, TetriminoColor.None, TetriminoColor.Color_3},
				{TetriminoColor.Color_3, TetriminoColor.Color_3, TetriminoColor.Color_3},
				{TetriminoColor.None, TetriminoColor.None, TetriminoColor.None},
			},
			new TetriminoColor[3, 3] {
				{TetriminoColor.Color_2, TetriminoColor.None, TetriminoColor.None},
				{TetriminoColor.Color_2, TetriminoColor.Color_2, TetriminoColor.None},
				{TetriminoColor.None, TetriminoColor.Color_2, TetriminoColor.None},
			},
			new TetriminoColor[3, 3] {
				{TetriminoColor.None, TetriminoColor.None, TetriminoColor.Color_7},
				{TetriminoColor.None, TetriminoColor.Color_7, TetriminoColor.Color_7},
				{TetriminoColor.None, TetriminoColor.Color_7, TetriminoColor.None},
			}
		};


		readonly TetriminoType type;


		public TetriminoFactory(TetriminoType type) {
			Assert.AreEqual(type, TetriminoType.Four, "まだ４個のテトリミノしか作ってないよ");
			this.type = type;
		}

		public Tetrimino CreateEmpty() {
			return new Tetrimino(Empty);
		}

		public Tetrimino Create() {
			var random = Random.Range(0, fourPatterns.Count);
			var original = fourPatterns[random];

			var height = original.GetLength(0);
			var width = original.GetLength(1);
			var instance = new TetriminoColor[height, width];

			Array.Copy(original, instance, height * width);
			return new Tetrimino(instance);
		}

	}

}