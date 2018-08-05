using System;
using UniRx;

namespace ZenjectTetris.Domain.Dialog {

	public class DialogUseCaseBase : UseCaseBase {

		public IObservable<Unit> OnCloseEnd => onCloseEnd;
		readonly Subject<Unit> onCloseEnd = new Subject<Unit>();

		public IObservable<Unit> OnCloseStart => onCloseStart;
		readonly Subject<Unit> onCloseStart = new Subject<Unit>();


		protected void Close() {
			onCloseStart.OnNext(Unit.Default);
			onCloseStart.OnCompleted();
		}

		public void OnCloseAnimationEnd() {
			onCloseEnd.OnNext(Unit.Default);
			onCloseEnd.OnCompleted();
		}
	}

}