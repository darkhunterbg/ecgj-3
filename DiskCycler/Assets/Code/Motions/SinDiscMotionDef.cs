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
		public override Vector2 GetStepOffset(float time)
		{
			float x = 2.0f * time * Mathf.PI;

			float y = Mathf.Sin(x);
			return new Vector2(x, y) / (2.0f * Mathf.PI);
		}
	}
}
