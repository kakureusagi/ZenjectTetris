using System;
using System.Collections.Generic;
using System.Linq;
using MessagePack;
using UnityEngine.Assertions;
using Zenject;
using ZenjectTetris.Domain.Data;

#pragma warning disable 649

namespace ZenjectTetris.Data {

	public class HiScoreStore {

		/// <summary>
		/// セーブデータの実体.
		/// </summary>
		[MessagePackObject(true)]
		public class SaveData {

			static readonly int MaxHiScoreCountPerUser = 10;
			static readonly int MaxHiScoreCount = 30;

			// シリアライズためにpublic.
			public Dictionary<string, HiScore> userLastHiScores = new Dictionary<string, HiScore>();
			public Dictionary<string, List<HiScore>> userHiScores = new Dictionary<string, List<HiScore>>();
			public List<HiScore> allUserScores = new List<HiScore>();


			public HiScore GetLastHiScore(string uuid) {
				return userLastHiScores.TryGetValue(uuid, out var score) ? score : null;
			}

			public HiScore[] GetAllUserHiScores() {
				return allUserScores.ToArray();
			}

			public HiScore[] GetHiScores(string uuid) {
				Assert.IsNotNull(uuid);
				if (userHiScores.TryGetValue(uuid, out var scores)) {
					return scores.ToArray();
				}

				return new HiScore[0];
			}

			public void UpdateScore(string uuid, int score) {
				// 個人毎の情報更新.
				if (!userHiScores.TryGetValue(uuid, out var hiScores)) {
					hiScores = new List<HiScore>();
					userHiScores.Add(uuid, hiScores);
				}

				var hiScore = new HiScore {
					Uuid = uuid,
					Score = score,
					Date = DateTime.Now,
				};

				userHiScores[uuid] = AddHiScore(hiScores, hiScore, MaxHiScoreCount);

				// 最終更新保存.
				if (!userLastHiScores.TryGetValue(uuid, out var _)) {
					userLastHiScores.Add(uuid, hiScore);
				}
				else {
					userLastHiScores[uuid] = hiScore;
				}

				// 全体の情報更新.
				allUserScores = AddHiScore(allUserScores, hiScore, MaxHiScoreCountPerUser);
			}

			private static List<HiScore> AddHiScore(List<HiScore> list, HiScore newScore, int maxCount) {
				list.Add(newScore);
				return list
					.OrderByDescending(score => score.Score)
					.ThenBy(score => score.Date)
					.Take(maxCount)
					.ToList();
			}
		}


		static readonly string Path = "HiScoreStore-SaveData";


		[Inject]
		IFileSave fileSave;

		SaveData data;


		[Inject]
		public void Initialize() {
			data = fileSave.Exists(Path) ? fileSave.Load<SaveData>(Path) : new SaveData();
		}

		public HiScore GetLastScore(string uuid) {
			Assert.IsNotNull(uuid);
			return data.GetLastHiScore(uuid);
		}

		public HiScore[] GetAllUserHiScores() {
			return data.GetAllUserHiScores();
		}

		public HiScore[] GetHiScores(string uuid) {
			Assert.IsNotNull(uuid);
			return data.GetHiScores(uuid);
		}

		public void UpdateScore(string uuid, int score) {
			Assert.IsNotNull(uuid);
			Assert.IsTrue(score >= 0);
			data.UpdateScore(uuid, score);
			fileSave.Save(Path, data);
		}
	}

}