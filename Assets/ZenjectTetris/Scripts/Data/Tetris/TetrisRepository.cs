using Zenject;
using ZenjectTetris.Domain.Tetris;

#pragma warning disable 649

namespace ZenjectTetris.Data.Title {

	public class TetrisRepository : RepositoryBase, ITetrisRepository {

		[Inject]
		UserStore userStore;

		[Inject]
		HiScoreStore hiScoreStore;


		public void SaveCurrentUserScore(int score) {
			hiScoreStore.UpdateScore(userStore.GetLoggedInUser().Uuid, score);
		}

	}

}