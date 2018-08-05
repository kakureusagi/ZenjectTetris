using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

namespace ZenjectTetris.Presentation.Tetris {

	class TetrisEffect : EffectBase {

		[SerializeField]
		Text restTime;


		public async UniTask CountDownGameStartTime(int seconds) {
			var countDown = seconds;
			for (var i = 0; i < seconds; i++) {
				restTime.text = countDown.ToString();
				await UniTask.Delay(1000);

				--countDown;
				if (countDown == 0) {
					restTime.enabled = false;
					break;
				}
			}
		}

	}


}