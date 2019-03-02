using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Motions
{
	[CreateAssetMenu(fileName = "TrochoidDiscMotionDef", menuName = "Disc Motions/TrochoidDiscMotionDef")]
	public class TrochoidDiscMotionDef : DiscMotionDef
	{
		public override Vector2 GetStepOffset(float time)
		{
			time *= 2.0f * Mathf.PI;
			float x = time - Mathf.Sin(time);

			float y = 1 -Mathf.Cos(time);
			return new Vector2(x, y)/ (2.0f * Mathf.PI);
		}
	}
}
