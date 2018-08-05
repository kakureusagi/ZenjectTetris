using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using ZenjectTetris.Domain.Core;
using ZenjectTetris.Domain.Title;

#pragma warning disable 649

namespace ZenjectTetris.Presentation.Title {

	class TitlePresenter : PresenterBase<TitleUseCase> {

		[Serializable]
		class DifficultyButton {
			public Difficulty Difficulty => difficulty;

			[SerializeField]
			Difficulty difficulty;

			public Button Button => button;

			[SerializeField]
			Button button;
		}


		[SerializeField]
		InputField userName;

		[SerializeField]
		Button gameStartButton;

		[SerializeField]
		Button createUserButton;

		[SerializeField]
		DifficultyButton[] difficultyButtons;

		[SerializeField]
		Color focusColor;

		[SerializeField]
		Color nonFocusColor;


		protected override void RunCore() {
			// from view.
			gameStartButton.OnClickAsObservable()
				.Subscribe(_ => useCase.StartGame())
				.AddTo(this);

			createUserButton.OnClickAsObservable()
				.Subscribe(_ => useCase.CreateUser())
				.AddTo(this);

			userName.OnValueChangedAsObservable()
				.Subscribe(userName => useCase.SetUserName(userName))
				.AddTo(this);

			foreach (var button in difficultyButtons) {
				button.Button.OnClickAsObservable()
					.Subscribe(_ => useCase.SetDifficulty(button.Difficulty))
					.AddTo(this);
			}

			// from useCase.
			useCase.StartButtonEnabled
				.Subscribe(value => gameStartButton.interactable = value)
				.AddTo(this);

			useCase.CreateUserButtonEnabled
				.Subscribe(value => createUserButton.interactable = value)
				.AddTo(this);

			useCase.UserName
				.Subscribe(value => userName.text = value)
				.AddTo(this);

			useCase.Difficulty
				.Subscribe(difficulty => {
					foreach (var button in difficultyButtons) {
						button.Button.targetGraphic.color = button.Difficulty == difficulty ? focusColor : nonFocusColor;
					}
				})
				.AddTo(this);
		}
	}

}