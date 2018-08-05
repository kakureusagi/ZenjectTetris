using System;
using UniRx;
using UnityEngine;

namespace ZenjectTetris.Presentation.Tetris {

	public class Input : IInput {

		public IObservable<Unit> MoveLeftAsObservable() {
			return KeyAsObservable(KeyCode.LeftArrow);
		}

		public IObservable<Unit> MoveRightAsObservable() {
			return KeyAsObservable(KeyCode.RightArrow);
		}

		public IObservable<Unit> MoveDownAsObservable() {
			return KeyAsObservable(KeyCode.DownArrow);
		}

		public IObservable<Unit> MoveFallAsObservable() {
			return KeyAsObservable(KeyCode.UpArrow);
		}

		public IObservable<Unit> TurnLeftAsObservable() {
			return KeyAsObservable(KeyCode.Z);
		}

		public IObservable<Unit> TurnRightAsObservable() {
			return KeyAsObservable(KeyCode.X);
		}

		static IObservable<Unit> KeyAsObservable(KeyCode code) {
			return Observable.EveryGameObjectUpdate()
				.Where(_ => UnityEngine.Input.GetKeyDown(code))
				.AsUnitObservable();
		}

	}

}