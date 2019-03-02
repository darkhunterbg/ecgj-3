using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Motions
{
	[Serializable]
	public class DiscMotionBlender
	{
		public List<DiscMotion> Motions = new List<DiscMotion>();

		private List<DiscMotion> _motions = new List<DiscMotion>();

		public Vector2 GetStepOffset(float time)
		{
			if (Motions.Count == 0)
				return Vector2.zero;

			int totalCycles = Motions.Sum(p => p.Cycles);

			var currentCycleTime = time % totalCycles;

			int elaspedFullCycles = (int)time / totalCycles;
			int interval = 0;

			DiscMotion currentMotion = Motions[Motions.Count - 1];

			float xOffset = TotalCycleLength * elaspedFullCycles;

			for (int i = 0; i < Motions.Count; ++i) {
				interval += Motions[i].Cycles;
				if (currentCycleTime <= interval) {
					currentMotion = Motions[i];
					interval -= Motions[i].Cycles;
					break;
				}
				xOffset += Motions[i].GetStepOffset(Motions[i].Cycles).x;// ((float)Motions[i].Cycles * Motions[i].Scale.x);
			}

			float cycleTime = currentCycleTime - interval;

			return currentMotion.GetStepOffset(cycleTime) + new Vector2(xOffset, 0);
		}

		private float TotalCycleLength => Motions.Sum(p => p.GetStepOffset(p.Cycles).x);

		public bool DetectChanges()
		{
			bool changes = false;

			if (_motions.Count != Motions.Count) {
				changes = true;
				_motions.Clear();
				_motions.AddRange(Motions);
			}
			else for (int i = 0; i < Motions.Count; ++i) {
					if (_motions[i] != Motions[i]) {
						changes = true;
						_motions[i] = Motions[i];
					}
					else {
						if (_motions[i].DetectChanges()) {
							changes = true;
						}
					}
				}

			return changes;

		}

	}
}
