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
