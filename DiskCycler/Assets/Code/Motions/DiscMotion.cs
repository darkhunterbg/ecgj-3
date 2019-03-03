using Skaillz.EditInline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Motions
{
	[Serializable]
	public class DiscMotion
	{
		public DiscMotionDef Def;
		public Vector2 Scale = Vector2.one;
		public int Cycles = 1;

		private DiscMotionDef _def;
		private Vector2 _scale;
		private int _cycles = 1;

		public Vector2 GetStepOffset(float time)
		{
			if (Def == null)
				return Vector2.zero;

			var offset = Def.GetStepOffset(time);
			return offset * Scale;
		}

		public bool DetectChanges()
		{
			bool changed = false;

			if (_def != Def) {
				_def = Def;
				changed = true;
			}

			if (_scale != Scale) {
				_scale = Scale;
				changed = true;
			}

			if (_cycles != Cycles) {
				_cycles = Cycles;
				changed = true;
			}

			return changed;
		}
	}
}
