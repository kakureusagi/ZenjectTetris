using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using ZenjectTetris.Domain.Core;

#pragma warning disable 649

namespace ZenjectTetris.Installer.Core {

	/// <summary>
	/// コアゲームのInstaller.
	/// </summary>
	[CreateAssetMenu(menuName = "ZenjectTetris/Core/Settings")]
	class TetrisSettingsInstaller : ScriptableObjectInstaller<TetrisSettingsInstaller> {

		[FormerlySerializedAs("tetrisSettingsContainer")]
		[SerializeField]
		TetrisSettings tetrisSettings;

		[SerializeField]
		TetriminoColorSettings colorSettings;


		public override void InstallBindings() {
			Container.BindInstance(tetrisSettings).AsCached();
			Container.BindInstance(colorSettings).AsCached();
		}

	}

}