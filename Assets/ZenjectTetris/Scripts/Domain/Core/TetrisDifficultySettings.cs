using System;
using UnityEngine;

#pragma warning disable 649

namespace ZenjectTetris.Domain.Core {

	/// <summary>
	/// テトリスのゲームバランス設定.
	/// </summary>
	[Serializable]
	public class TetrisDifficultySettings {

		public Difficulty Difficulty => difficulty;

		[SerializeField]
		Difficulty difficulty;

		public TetriminoType TetriminoType => tetriminoType;

		[SerializeField]
		TetriminoType tetriminoType;

		public float DropInterval => dropInterval;

		[SerializeField]
		float dropInterval;

	}

}