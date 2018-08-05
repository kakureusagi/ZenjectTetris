using System;
using UnityEngine;
using ZenjectTetris.Domain.Core;

#pragma warning disable 649

namespace ZenjectTetris.Domain.Tetris {

	[Serializable]
	public class TetrisSceneData {

		public Difficulty Difficulty { get => difficulty; set => difficulty = value; }

		[SerializeField]
		Difficulty difficulty;
	}

}