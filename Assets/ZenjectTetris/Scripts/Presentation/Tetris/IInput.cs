using System;
using UniRx;

namespace ZenjectTetris.Presentation.Tetris {

	public interface IInput {

		IObservable<Unit> MoveLeftAsObservable();
		IObservable<Unit> MoveRightAsObservable();
		IObservable<Unit> MoveDownAsObservable();
		IObservable<Unit> MoveFallAsObservable();
		IObservable<Unit> TurnLeftAsObservable();
		IObservable<Unit> TurnRightAsObservable();


	}

}