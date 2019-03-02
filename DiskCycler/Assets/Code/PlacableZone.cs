using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code
{
	public class PlacableZone : MonoBehaviour
	{
		public BoxCollider2D Collider;

		public SpriteRenderer Visuals;

		public void SetVisualsVisible(bool visible)
		{
			Visuals.enabled = visible;
		}

		//public List<PlacableObstacle> InsidePlacables { get; private set; } = new List<PlacableObstacle>();



		//public bool CanPlace(PlacableObstacle obstacle)
		//{
		//	var collider = obstacle.Collider as BoxCollider2D;

		//	var hit = Physics2D.BoxCast(obstacle.transform.position, collider.size, 0, Vector2.zero, gameObject.layer);
		//}

	}
}
