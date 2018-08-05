using UniRx.Async;
using UnityEngine;

namespace ZenjectTetris.Domain.Dialog {

	public class DialogPerformerBase : MonoBehaviour {

		public async UniTask Close() {
			// ここでは適当にアニメーションする.
			await UniTask.DelayFrame(1);
			transform.localScale = Vector3.zero;
			await UniTask.DelayFrame(1);
		}

	}

}