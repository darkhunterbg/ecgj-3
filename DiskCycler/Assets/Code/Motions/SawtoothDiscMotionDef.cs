using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Motions
{
	[CreateAssetMenu(fileName = "SawtoothDiscMotionDef", menuName = "Disc Motions/SawtoothDiscMotionDef")]
	public class SawtoothDiscMotionDef : DiscMotionDef
	{
		public override Vector2 GetStepOffset(float time)
		{
			float x = time;
			x += 0.25f;
			float y = Mathf.Abs( (x - Mathf.Floor(x + 0.5f))) - 0.25f;
			return new Vector2(x - 0.25f, y );
		}
	}
}
