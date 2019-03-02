using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Code.UI
{
	[DefaultExecutionOrder(100)]
	public class UIObstacleModule : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
	{
		public PlacableObstacle Preview;

		public float Snapping = 1.0f;

		void Start()
		{

		}

		public void UseObstacle(PlacableObstacle prefab)
		{
			if (Preview != null) {
				GameObject.Destroy(Preview.gameObject);
				Preview = null;
			}

			if (prefab != null) {
				Preview = GameObject.Instantiate(prefab);
				Preview.name = "Preview";
				Preview.SetSnapping(Snapping);
				Preview.gameObject.SetActive(true);
				Preview.Visual.color *= 0.75f;
				Preview.Collider.enabled = false;
			}

			Cursor.visible = Preview == null;
		}

		public void Show()
		{
			gameObject.SetActive(true);
			if (Preview != null)
				Preview.gameObject.SetActive(true);
		}
		public void Hide()
		{
			gameObject.SetActive(false);
			if (Preview != null)
				Preview.gameObject.SetActive(false);

			Cursor.visible = true;
		}

		public void Update()
		{
			if (Preview == null || !Preview.gameObject.activeInHierarchy)
				return;

			var pos = Input.mousePosition;
			var worldPos = Camera.main.ScreenToWorldPoint(pos);
			worldPos.z = 0;
			Preview.SetGridPosition(worldPos);

			Preview.Visual.color = Preview.Detector.CanBePlaced ? Color.white : Color.red;
			Preview.Visual.color *= 0.75f;


		}


		public void OnPointerClick(PointerEventData eventData)
		{

			//var worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
			//worldPos.z = 0;

			if (eventData.button == PointerEventData.InputButton.Left) {

				if (!Preview.Detector.CanBePlaced)
					return;

				var obstacle = GameObject.Instantiate(Preview, Level.Instance.PlacableObstaclesParent);
				obstacle.Visual.color = Color.white;
				obstacle.Collider.enabled = true;
				obstacle.Detector.gameObject.SetActive(false);
				obstacle.name = "PlacableObstacle";

				if (Level.Instance.CanStartGame)
					UseObstacle(null);
			}
			else if (eventData.button == PointerEventData.InputButton.Right) {
				if (Preview != null) {
					UseObstacle(null);
					return;
				}

				var worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
				RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, 10, 1);
				if (hit.collider != null && hit.collider.GetComponentInParent<PlacableObstacle>()) {
					GameObject.Destroy(hit.collider.gameObject);
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
