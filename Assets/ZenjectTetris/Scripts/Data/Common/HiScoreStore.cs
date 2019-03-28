using System;
using System.Collections.Generic;
using System.Linq;
using MessagePack;
using UnityEngine.Assertions;
using Zenject;

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
			public Dictionary<string, HiScoreRecord> userLastHiScores = new Dictionary<string, HiScoreRecord>();
			public Dictionary<string, List<HiScoreRecord>> userHiScores = new Dictionary<string, List<HiScoreRecord>>();
			public List<HiScoreRecord> allUserScores = new List<HiScoreRecord>();


			public HiScoreRecord GetLastHiScore(string uuid) {
				return userLastHiScores.TryGetValue(uuid, out var score) ? score : null;
			}

			public HiScoreRecord[] GetAllUserHiScores() {
				return allUserScores.ToArray();
			}

			public HiScoreRecord[] GetHiScores(string uuid) {
				Assert.IsNotNull(uuid);
				if (userHiScores.TryGetValue(uuid, out var scores)) {
					return scores.ToArray();
				}

				return new HiScoreRecord[0];
			}

			public void UpdateScore(string uuid, int score) {
				// 個人毎の情報更新.
				if (!userHiScores.TryGetValue(uuid, out var hiScores)) {
					hiScores = new List<HiScoreRecord>();
					userHiScores.Add(uuid, hiScores);
				}

				var hiScore = new HiScoreRecord {
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

			private static List<HiScoreRecord> AddHiScore(List<HiScoreRecord> list, HiScoreRecord newScore, int maxCount) {
				list.Add(newScore);
				return list
					.OrderByDescending(score => score.Score)
					.ThenBy(score => score.Date)
					.Take(maxCount)
					.ToList();
			}
		}


		static readonly string Path = "HiScoreStore.save";


		[Inject]
		IFileSave fileSave;

		SaveData data;


		[Inject]
		public void Initialize() {
			data = fileSave.Exists(Path) ? fileSave.Load<SaveData>(Path) : new SaveData();
		}

		public HiScoreRecord GetLastScore(string uuid) {
			Assert.IsNotNull(uuid);
			return data.GetLastHiScore(uuid);
		}

		public HiScoreRecord[] GetAllUserHiScores() {
			return data.GetAllUserHiScores();
		}

		public HiScoreRecord[] GetHiScores(string uuid) {
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