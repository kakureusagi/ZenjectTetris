using UnityEditor;
using UnityEngine;
using Zenject;
using ZenjectTetris.Domain.Debug;
using ZenjectTetris.Installer;

#pragma	warning disable 649

namespace ZenjectTetrisEditor {

	class TestUserWindow : ZenjectEditorWindow {

		[Inject]
		TestUserUseCase.IFactory testUserUseCaseFactory;

		TestUserUseCase testUserUseCase;

		string userName;
		int hiScore;


		public override void InstallBindings() {
			SystemInstaller.InstallBindingsCore(Container);
		}

		[MenuItem("Tools/ログインウィンドウを開く")]
		static void ShowWindow() {
			GetWindow<TestUserWindow>("ログインウィンドウ");
		}

		public override void OnEnable() {
			base.OnEnable();
			testUserUseCase = testUserUseCaseFactory.Create();
			testUserUseCase.Run();
			userName = testUserUseCase.GetUserName();
		}

		public override void OnGUI() {
			var titleStyle = new GUIStyle(GUI.skin.label) {
				normal = {textColor = Color.yellow}
			};

			EditorGUILayout.LabelField("ログイン", titleStyle);

			userName = EditorGUILayout.TextField("ログイン名", userName);
			if (GUILayout.Button("ログイン名を設定する")) {
				testUserUseCase.SetUserName(userName);
			}
		}
	}

}