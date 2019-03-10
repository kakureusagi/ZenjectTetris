using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Async;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using ZenjectTetris.Domain;
using ZenjectTetris.Domain.Core;
using ZenjectTetris.Domain.Tetris;

#pragma warning disable 649

namespace ZenjectTetris.Presentation.Tetris {

	class TetrisPresenter : PresenterBase<TetrisUseCase> {

		/// <summary>
		/// テトリミノのサイズと表示位置の関係.
		/// </summary>
		static readonly Dictionary<int, int[]> SizeAndIndexes = new Dictionary<int, int[]> {
			{1, new int[] {12}},
			{2, new int[] {5, 6, 9, 10}},
			{3, new int[] {6, 7, 8, 11, 12, 13, 16, 17, 18}},
			{4, Enumerable.Range(0, 4 * 4).ToArray()},
			{5, Enumerable.Range(0, 5 * 5).ToArray()},
		};


		[Inject]
		TetriminoColorSettings colorSettings;

		[Inject]
		IInput input;

		[Inject]
		IGameTimer timer;


		[SerializeField]
		TetrisEffect effect;

		[SerializeField]
		Text score;

		[SerializeField]
		Image restTime;

		[SerializeField]
		Image[] nextOddTetriminoImages;

		[SerializeField]
		Image[] nextEvenTetriminoImages;

		[SerializeField]
		Image[] fieldImages;

		[SerializeField]
		GameObject gameOverRoot;

		[SerializeField]
		Button toResultButton;


		protected override void RunCore() {
			FromPresenter();
			FromView();

			this.UpdateAsObservable()
				.Subscribe(_ => useCase.Update(timer.DeltaTime))
				.AddTo(this);

			StartEffect();
		}

		void FromView() {
			input.MoveLeftAsObservable()
				.Subscribe(_ => useCase.MoveTetriminoLeft())
				.AddTo(this);
			input.MoveRightAsObservable()
				.Subscribe(_ => useCase.MoveTetriminoRight())
				.AddTo(this);
			input.MoveDownAsObservable()
				.Subscribe(_ => useCase.MoveTetriminoDown())
				.AddTo(this);
			input.MoveFallAsObservable()
				.Subscribe(_ => useCase.FallTetrimino())
				.AddTo(this);
			input.TurnLeftAsObservable()
				.Subscribe(_ => useCase.TurnTetriminoLeft())
				.AddTo(this);
			input.TurnRightAsObservable()
				.Subscribe(_ => useCase.TurnTetriminoRight())
				.AddTo(this);

			toResultButton.OnClickAsObservable()
				.Subscribe(_ => useCase.GoToResult())
				.AddTo(this);
		}

		void FromPresenter() {
			useCase.FieldColors
				.Where(colors => colors != null)
				.Subscribe(UpdateFieldColors)
				.AddTo(this);
			useCase.NextTetrimino
				.Where(colors => colors != null)
				.Subscribe(UpdateNextTetrimino)
				.AddTo(this);
			useCase.Score
				.Subscribe(score => this.score.text = score.ToString())
				.AddTo(this);
			useCase.RestTime
				.Subscribe(time => restTime.fillAmount = time / useCase.MaxRestTime)
				.AddTo(this);
			useCase.IsGameOver
				.Subscribe(value => gameOverRoot.SetActive(value))
				.AddTo(this);
		}

		async UniTask StartEffect() {
			await effect.CountDownGameStartTime(3);
			useCase.StartGame();
		}

		void UpdateFieldColors(TetriminoColor[,] tetriminoColors) {
			var width = tetriminoColors.GetLength(0);
			var height = tetriminoColors.GetLength(1);
			for (var x = 0; x < width; x++) {
				for (var y = 0; y < height; y++) {
					var i = x + y * width;
					fieldImages[i].color = colorSettings.GetColor(tetriminoColors[x, y]);
				}
			}
		}

		void UpdateNextTetrimino(TetriminoColor[,] tetriminoColors) {
			ClearNextTetrimino();

			var size = tetriminoColors.GetLength(0);
			var indexes = SizeAndIndexes[size];
			var targets = size % 2 == 0 ? nextEvenTetriminoImages : nextOddTetriminoImages;

			for (var x = 0; x < size; x++) {
				for (var y = 0; y < size; y++) {
					var i = x + y * size;
					var index = indexes[i];
					var color = tetriminoColors[x, y];
					targets[index].color = colorSettings.GetColor(color);
				}
			}
		}

		void ClearNextTetrimino() {
			foreach (var image in nextEvenTetriminoImages) {
				image.color = Color.clear;
			}

			foreach (var image in nextOddTetriminoImages) {
				image.color = Color.clear;
			}
		}
	}

}