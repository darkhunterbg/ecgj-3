using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code
{
	public interface ICollisionReciever
	{
		void OnCollision(Collider2D collision);
	}

	public class CollisionNotifier : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D collision)
		{
			var c = GetComponentInParent<ICollisionReciever>();
			if (c != null)
				c.OnCollision(collision);
		}
	}
}
