using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Code.UI
{
	[DefaultExecutionOrder(100)]
	public class UIObstacleModule : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
	{
		public PlacableObstacle Preview;

		private PlacableObstacle _previewPrefab;

		public float Snapping = 1.0f;

		public Color InvalidColor = Color.red;
		public Color ValidColor = Color.green;

		public AudioClip PlaceObstacleSFX;

		void Start()
		{
			Show();
		}

		public void UseObstacle(PlacableObstacle prefab)
		{
			if (Preview != null) {
				GameObject.Destroy(Preview.gameObject);
				Preview = null;
			}

			_previewPrefab = prefab;

			if (prefab != null) {
				Preview = GameObject.Instantiate(prefab);
				Preview.name = "Preview";
				Preview.SetSnapping(Snapping);
				Preview.Collider.enabled = false;
				Preview.Visual.sortingOrder += 2;

				if (Preview.PlacableVisual != null) {
					Preview.PlacableVisual.gameObject.SetActive(true);
					Preview.PlacableVisual.sortingOrder += 2;
				}

			}

			Cursor.visible = Preview == null;

			SetZones();
		}

		private void SetZones()
		{
			foreach (var zone in Level.Instance.PlacableZones) {
				bool visible = Preview != null && Preview.gameObject.activeInHierarchy && zone.CanPlace(Preview.Type);

				zone.SetVisualsVisible(visible);
				if (Preview != null) {
					zone.SetPlaceMode(Preview.Type);
				}
			}
		}

		public void Show()
		{
			gameObject.SetActive(true);
			if (Preview != null)
				Preview.gameObject.SetActive(true);

			SetZones();
		}
		public void Hide()
		{
			gameObject.SetActive(false);
			if (Preview != null)
				Preview.gameObject.SetActive(false);

			Cursor.visible = true;

			SetZones();
		}

		public void Update()
		{
			if (Preview == null || !Preview.gameObject.activeInHierarchy)
				return;

			var pos = Input.mousePosition;
			var worldPos = Camera.main.ScreenToWorldPoint(pos);
			worldPos.z = 0;
			Preview.SetGridPosition(worldPos);

			Preview.Visual.color = Preview.Detector.CanBePlaced ? Color.white : InvalidColor;
			Preview.Visual.color *= 0.75f;

			if (Preview.PlacableVisual != null) {
				Preview.PlacableVisual.color = Preview.Detector.CanBePlaced ? ValidColor : InvalidColor;
				Preview.PlacableVisual.color *= 0.5f;
			}


		}


		public void OnPointerClick(PointerEventData eventData)
		{

			//var worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
			//worldPos.z = 0;

			if (eventData.button == PointerEventData.InputButton.Left) {

				if (Preview == null) {
					var worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
					RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, 10, 1);
					if (hit.collider != null && hit.collider.GetComponentInParent<PlacableObstacle>()) {
						var obstacle = hit.collider.GetComponentInParent<PlacableObstacle>();
						UseObstacle(obstacle.Prefab);
						GameObject.Destroy(obstacle.gameObject);
					}
				}

				else {
					if (!Preview.Detector.CanBePlaced)
						return;

					var obstacle = GameObject.Instantiate(_previewPrefab, Level.Instance.PlacableObstaclesParent);
					if (obstacle.PlacableVisual != null) {
						obstacle.PlacableVisual.gameObject.SetActive(false);
					}
					obstacle.transform.position = Preview.transform.position;
					obstacle.transform.rotation = Preview.transform.rotation;
					//obstacle.Detector.gameObject.SetActive(false);
					obstacle.Prefab = _previewPrefab;

					GameController.Instance.AudioSource.PlayOneShot(PlaceObstacleSFX);

					switch (obstacle.Type) {
						case ObstacleType.Laser: {
								if (Level.Instance.RemainingLasers == 0)
									UseObstacle(null);
							}
							break;

						case ObstacleType.Obstacle: {
								if (Level.Instance.RemainingObstacles == 0)
									UseObstacle(null);
							}
							break;
					}
				}
			}
			else if (eventData.button == PointerEventData.InputButton.Right) {
				if (Preview != null) {
					UseObstacle(null);
					return;
				}

				var worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
				RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, 10, 1);
				if (hit.collider != null && hit.collider.GetComponentInParent<PlacableObstacle>()) {
					var obstacle = hit.collider.GetComponentInParent<PlacableObstacle>();
					GameObject.Destroy(obstacle.gameObject);
				}
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (Preview != null) {
				Preview.gameObject.SetActive(true);
				Cursor.visible = false;
			}
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (Preview != null) {
				Preview.gameObject.SetActive(false);
				Cursor.visible = true;
			}
		}
	}
}
