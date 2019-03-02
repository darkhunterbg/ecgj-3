using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Code.UI
{
	[DefaultExecutionOrder(100)]
	public class UIObstacleModule : MonoBehaviour, IPointerClickHandler
	{
		public PlacableObstacle Obstacle;

		public PlacableObstacle Preview;

		public float Snapping = 1.0f;

		void Start()
		{
			Preview = GameObject.Instantiate(Obstacle);
			Preview.name = "Preview";
			Preview.SetSnapping(Snapping);
			Preview.gameObject.SetActive(true);
			Preview.Visual.color *= 0.75f;
			Preview.Collider.enabled = false;
			Cursor.visible = false;
		}

		public void Update()
		{
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

				var obstacle = GameObject.Instantiate(Preview);
				obstacle.Visual.color = Color.white;
				obstacle.Collider.enabled = true;
				obstacle.Detector.gameObject.SetActive(false);
			}
			else if (eventData.button == PointerEventData.InputButton.Right) {
				var worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
				RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, 10, 1);
				if (hit.collider != null && hit.collider.GetComponentInParent<PlacableObstacle>()) {
					GameObject.Destroy(hit.collider.gameObject);
				}
			}
		}
	}
}
