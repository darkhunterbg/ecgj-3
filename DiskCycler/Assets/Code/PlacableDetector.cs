using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code
{
	public class PlacableDetector : MonoBehaviour
	{
		public BoxCollider2D Collider;

		public bool CanBePlaced { get; private set; }

		public List<PlacableZone> InsideZones { get; private set; } = new List<PlacableZone>();

		private bool _collideWithOtherDetector;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.GetComponentInParent<PlacableDetector>()) {
				_collideWithOtherDetector = true;
			}

			PlacableZone zone = collision.GetComponentInParent<PlacableZone>();
			if (zone == null)
				return;

			var collider = collision as BoxCollider2D;


			if (!InsideZones.Contains(zone))
				InsideZones.Add(zone);
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			if (collision.GetComponentInParent<PlacableDetector>()) {
				_collideWithOtherDetector = false;
			}

			PlacableZone zone = collision.GetComponentInParent<PlacableZone>();
			if (zone == null)
				return;

			InsideZones.Remove(zone);
		}



		public void Update()
		{
			if (!transform.hasChanged)
				return;


			transform.hasChanged = false;

			CanBePlaced = false;


			if (_collideWithOtherDetector)
				return;


			foreach (var zone in InsideZones) {
				var collider = zone.Collider;

				bool inside = (collider.bounds.Contains(Collider.bounds.min)
				&& collider.bounds.Contains(Collider.bounds.max));

				if (inside) {
					CanBePlaced = true;
					return;
				}
			}
		}
	}
}
