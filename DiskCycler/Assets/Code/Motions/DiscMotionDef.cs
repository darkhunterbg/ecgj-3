using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code
{
	public abstract class DiscMotionDef : ScriptableObject
	{
		[NonSerialized]
		public bool Dirty = true;

		public abstract Vector2 GetStepOffset(float time);

		public void OnValidate()
		{
			Dirty = true;
		}
	}

}
