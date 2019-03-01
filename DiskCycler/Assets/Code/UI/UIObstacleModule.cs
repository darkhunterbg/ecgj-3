using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Code.UI
{
	public class UIObstacleModule : MonoBehaviour, IPointerClickHandler
	{
		public GameObject Obstacle;

		public void OnPointerClick(PointerEventData eventData)
		{
			var worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
			worldPos.z = 0;

			if (eventData.button == PointerEventData.InputButton.Left) {


				var obstacle = GameObject.Instantiate(Obstacle);
				obstacle.transform.position = worldPos;
			}
			else if (eventData.button == PointerEventData.InputButton.Right) {
				RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
				if(hit.collider!=null && hit.collider.GetComponentInParent<PlacableObstacle>()) {
					GameObject.Destroy(hit.collider.gameObject);
				}
			}
		}
	}
}
