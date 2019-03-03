using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Code
{
	[ExecuteInEditMode]
	public class PlacableZone : MonoBehaviour
	{
		public BoxCollider2D Collider;

		public GameObject Visuals;

		public TextMeshPro Text;

		public bool CanPlaceWalls = true;
		public bool CanPlaceLasers = true;

		public bool FlipOrientation = false;

		public float Snapping = 0.5f;
		public float TextScale = 0.25f;

		public void Start()
		{
		}

		public bool CanPlace(ObstacleType type)
		{
			switch (type) {
				case ObstacleType.Obstacle: return CanPlaceWalls;
				case ObstacleType.Laser: return CanPlaceLasers;
			}

			return false;
		}

		public void SetPlaceMode(ObstacleType type)
		{
			Text.text = $"PLACE {type.ToString().ToUpper()} HERE";
		}

		public void SetVisualsVisible(bool visible)
		{
			Visuals.gameObject.SetActive(visible);
		}

		public void Update()
		{


			if (Application.isEditor && transform.hasChanged) {
				UpdateBounds();

				//Sprite.transform.hasChanged = false;
				//Collider.offset = Sprite.transform.localPosition;
				//Collider.size = Sprite.transform.localScale / 6.25f;
				Text.rectTransform.localScale = (new Vector3(TextScale / transform.localScale.x, TextScale / transform.localScale.y, 1));
			}
		}

		private void UpdateBounds()
		{
			transform.hasChanged = false;

			if (Snapping <= 0)
				return;


			float s = Snapping;

			Vector2 pos = transform.localPosition;
			Vector2Int gridPos = new Vector2Int((int)(pos.x / Snapping), (int)(pos.y / Snapping));
			transform.localPosition = new Vector3(gridPos.x * Snapping, gridPos.y * Snapping, transform.position.z);



			Vector2 scale = transform.localScale;
			Vector2Int gridScale = new Vector2Int((int)(scale.x / s), (int)(scale.y / s));
			transform.localScale = new Vector3(gridScale.x * s, gridScale.y * s, transform.localScale.z);

		}


		//public List<PlacableObstacle> InsidePlacables { get; private set; } = new List<PlacableObstacle>();



		//public bool CanPlace(PlacableObstacle obstacle)
		//{
		//	var collider = obstacle.Collider as BoxCollider2D;

		//	var hit = Physics2D.BoxCast(obstacle.transform.position, collider.size, 0, Vector2.zero, gameObject.layer);
		//}

	}
}
