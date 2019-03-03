using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code
{
	public enum ObstacleType
	{
		Obstacle ,
		Laser ,
	}

	[ExecuteInEditMode]
	public class PlacableObstacle : MonoBehaviour
	{
		public float Snapping { get; private set; } = 0.0f;

		public SpriteRenderer Visual;
		public SpriteRenderer PlacableVisual;
		public Collider2D Collider;

		public PlacableDetector Detector;
		public PlacableLaser Laser;

		public PlacableObstacle Prefab;

		public ObstacleType Type;

		public void SetSnapping(float range)
		{
			Snapping = range;
			UpdatePosition();
		}

		public void SetGridPosition(Vector2 pos)
		{
			transform.position = new Vector3(pos.x, pos.y, 0);
			UpdatePosition();
		}

		private void UpdatePosition()
		{
			transform.hasChanged = false;

			if (Snapping <= 0)
				return;

			Vector2 pos = transform.position;
			Vector2Int gridPos = new Vector2Int((int)(pos.x / Snapping), (int)(pos.y / Snapping));
			transform.position = new Vector3(gridPos.x * Snapping, gridPos.y * Snapping, transform.position.z);
		}

		public void Update()
		{
			if (transform.hasChanged) {
				UpdatePosition();
			}
		}


	}
}
