using UnityEditor;
using UnityEngine;

namespace Assets.Code
{
	[ExecuteInEditMode]
	public class DiscProjector : MonoBehaviour
	{
		[Range(0, 1000)]
		public int Projections = 0;

		public float ProjectionStep = 0.1f;

		private int _proj = 0;
		private bool _updateProj = true;

		public Transform ProjectionsRoot;
		public GameObject ProjectionsVisuals;

		private DiscMotionDef _motion;

		private void OnValidate()
		{
			if (Projections != _proj) {
				_proj = Projections;
				_updateProj = true;
			}
		}

		private void Update()
		{
			if (!Application.isPlaying) {

				var disc = GetComponent<Disc>();
				if (disc.Motion == null)
					return;

				if (disc.Motion != _motion) {
					_motion = disc.Motion;
					_updateProj = true;
				}
				else if (disc.Motion.Dirty) {
					_updateProj = true;
					disc.Motion.Dirty = false;
				}
			}

			if (_updateProj) {

				while (ProjectionsRoot.childCount > 0) {
					GameObject.DestroyImmediate(ProjectionsRoot.GetChild(0).gameObject);
				}
				_updateProj = false;

				if (Application.isPlaying)
					return;

				var disc = GetComponent<Disc>();
				if (disc.Motion == null)
					return;

				for (int i = 1; i <= _proj; ++i) {
					var projection = GameObject.Instantiate(ProjectionsVisuals, ProjectionsRoot);
					var offset = disc.Motion.GetStepOffset(ProjectionStep * i);

					projection.transform.position += new Vector3(offset.x, offset.y, 0);
				}
			}
		}
	}
}
