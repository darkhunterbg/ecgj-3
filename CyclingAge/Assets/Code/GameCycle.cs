using System;
using System.Collections.Generic;

namespace Assets.Code
{
	[Serializable]
	public class GameCycle
	{
		public int Number;

		public List<Hero> Heroes = new List<Hero>();
		public List<Encounter> AvailableEncounters = new List<Encounter>();

		public GameTime Time = new GameTime();

		public void Begin()
		{
			UpdateState();
		}

		public void NextTurn()
		{
			Time.ProgressTime();
		}

		private void UpdateState()
		{
			AvailableEncounters.Add(new Encounter()
			{
				Difficulty = 60,
				Name = "Defeat the bad guys"
			});
			AvailableEncounters.Add(new Encounter()
			{
				Difficulty = 40,
				Name = "Defeat the bad guys EZ"
			});
			AvailableEncounters.Add(new Encounter()
			{
				Difficulty = 80,
				Name = "Defeat the bad guys HARD"
			});
		}

	}
}
