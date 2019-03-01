using Skaillz.EditInline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code
{
	public class Disc : MonoBehaviour, ICollisionReciever
	{
		[EditInline]
		public DiscMotionDef Motion;

		private float _elapsed = 0.0f;
		private Vector3 _startingPos;

		public float Velocity = 10;

		private void Start()
		{
			_startingPos = transform.position;
		}

		public void FixedUpdate()
		{
			_elapsed += Time.fixedDeltaTime * Velocity;

			var offset = Motion.GetStepOffset(_elapsed);

			transform.position = _startingPos + new Vector3(offset.x, offset.y) ;
		}



		public void OnCollision(Collider2D collision)
		{
		}
	}
}
