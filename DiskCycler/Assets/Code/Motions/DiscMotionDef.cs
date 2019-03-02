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
		public abstract Vector2 GetStepOffset(float time);

	}

}
