using System;
using System.Linq;
using UnityEngine;

#pragma warning disable 649

namespace ZenjectTetris.Domain.Core {

	/// <summary>
	/// テトリミノのTypeと色の関連設定.
	/// </summary>
	[Serializable]
	public class TetriminoColorSettings {

		[Serializable]
		class Settings {

			public TetriminoColor TetriminoTetriminoColor => tetriminoTetriminoColor;

			[SerializeField]
			TetriminoColor tetriminoTetriminoColor;

			public Color Color => color;

			[SerializeField]
			Color color;

		}


		[SerializeField]
		Settings[] list;

		[NonSerialized]
		bool initialized;

		Color[] cache;


		public Color GetColor(TetriminoColor color) {
			if (!initialized) {
				cache = list
					.OrderBy(item => item.TetriminoTetriminoColor).Select(item => item.Color)
					.ToArray();
				initialized = true;
			}

			return cache[(int) color];
		}

	}

}