using UnityEngine;
using UnityEngine.Assertions;
using Zenject;
using ZenjectTetris.Domain;

namespace ZenjectTetris.Presentation {

	public abstract class PresenterBase<TUseCase> : MonoBehaviour where TUseCase : UseCaseBase {

		public interface IFactoryBase<out TPresenter> : IFactory<Object, Transform, TPresenter> where TPresenter : MonoBehaviour {

		}


		protected TUseCase useCase;


		public void Run(TUseCase useCase) {
			Assert.IsNotNull(useCase);
			this.useCase = useCase;
			RunCore();
		}

		protected abstract void RunCore();
	}

}