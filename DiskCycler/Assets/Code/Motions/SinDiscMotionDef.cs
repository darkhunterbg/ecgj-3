using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Motions
{
	[CreateAssetMenu(fileName = "SinDiscMotionDef", menuName = "Disc Motions/SinDiscMotionDef")]
	public class SinDiscMotionDef : DiscMotionDef
	{
		public Vector2 Scale = Vector2.one;

		public override Vector2 GetStepOffset(float time)
		{
			float x =  time;

			float y = Mathf.Sin(x);
			return new Vector2(x, y) * Scale;
		}
	}
}
