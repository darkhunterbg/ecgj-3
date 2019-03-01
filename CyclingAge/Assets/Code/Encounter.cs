using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Code
{
	[Serializable]
	public class Encounter
	{
		public string Name;

		public int Difficulty = 50;

		public List<Hero> Heroes = new List<Hero>();

		public int MaxHeroes { get; private set; } = 3;
		public int DifficultyBaseLine = 50;

		public int TotalAttack => Heroes.Sum(t => t.Attack);
		public int TotalDefense => Heroes.Sum(t => t.Defense);

		public int SuccessChance { get; private set; }
		public int Survivability { get; private set; }

		public void AddHero(Hero hero)
		{
			Debug.Assert(!Heroes.Contains(hero));
			Debug.Assert(Heroes.Count < MaxHeroes);
			Heroes.Add(hero);
			RefreshStats();
		}
		public void RemoveHero(Hero hero)
		{
			Debug.Assert(Heroes.Contains(hero));
			Heroes.Remove(hero);
			RefreshStats();
		}

		private void RefreshStats()
		{
			SuccessChance = (TotalAttack * 100) / Difficulty;
			SuccessChance -= DifficultyBaseLine;
			SuccessChance *= 2;
			SuccessChance = Math.Min(100, Math.Max(SuccessChance, 0));

			Survivability = (TotalDefense * 100) / Difficulty;
			Survivability -= DifficultyBaseLine;
			Survivability *= 2;
			Survivability = Math.Min(100, Math.Max(Survivability, 0));
		}
	}
}
