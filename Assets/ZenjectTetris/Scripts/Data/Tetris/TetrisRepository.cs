using Zenject;
using ZenjectTetris.Domain.Tetris;

#pragma warning disable 649

namespace ZenjectTetris.Data.Title {

	public class TetrisRepository : RepositoryBase, ITetrisRepository {

		[Inject]
		HiScoreStore hiScoreStore;

		[Inject]
		CacheUserStore userStore;


		public void SaveCurrentUserScore(int score) {
			hiScoreStore.UpdateScore(userStore.GetUser().Uuid, score);
		}

	}

}