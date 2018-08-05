using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

#pragma warning disable 649

namespace ZenjectTetris.Domain.Core {

	/// <summary>
	/// テトリスのゲームバランス設定のまとめ担当.
	/// </summary>
	[Serializable]
	public class TetrisSettings {

		[SerializeField]
		TetrisDifficultySettings[] list;


		public TetrisDifficultySettings GetDifficultySettings(Difficulty difficulty) {
			var settings = list.FirstOrDefault(i => i.Difficulty == difficulty);
			Assert.IsNotNull(settings);

			return settings;
		}

	}

}