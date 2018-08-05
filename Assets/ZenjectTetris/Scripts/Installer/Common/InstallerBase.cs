using Zenject;

#pragma	warning disable 649

namespace ZenjectTetris.Installer {

	public class InstallerBase<T> : MonoInstaller<T> where T : MonoInstaller<T> {

		public override void InstallBindings() {
		}
	}


}