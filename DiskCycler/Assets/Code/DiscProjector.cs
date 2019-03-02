using Assets.Code.Motions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code
{
	[ExecuteInEditMode]
	public class DiscProjector : MonoBehaviour
	{
		[Range(0, 1000)]
		public int Projections = 0;

		public float ProjectionTime = 1.0f;

		private bool _updateProj = true;

		public Transform ProjectionsRoot;
		public GameObject ProjectionsVisuals;

		public void Start()
		{
			if(Application.isPlaying) {
				Cleanup();
				enabled = false;
			}
		}

		public void Cleanup()
		{
			while (ProjectionsRoot.childCount > 0) {
				GameObject.DestroyImmediate(ProjectionsRoot.GetChild(0).gameObject);
			}
		}

		private void OnValidate()
		{
			//if (Projections != _proj) {
			//	_proj = Projections;
			//	_updateProj = true;
			//}

			_updateProj = true;
		}

		private void Update()
		{
			if (!Application.isPlaying) {

				var disc = GetComponent<Disc>();
				if (disc.Motion == null)
					return;

				if (disc.Motion.DetectChanges()) {
					_updateProj = true;
				}
			}

			if (_updateProj) {
				Cleanup();

				_updateProj = false;

				if (Application.isPlaying)
					return;

				var disc = GetComponent<Disc>();
				if (disc.Motion == null)
					return;

				float step = ProjectionTime / Projections;

				for (int i = 1; i <= Projections; ++i) {
					var projection = (GameObject)PrefabUtility.InstantiatePrefab(ProjectionsVisuals, SceneManager.GetActiveScene());
					projection.transform.SetParent(ProjectionsRoot);
					var offset = disc.Motion.GetStepOffset(step * i);

					projection.transform.position += new Vector3(offset.x, offset.y, 0);
				}
			}
		}
	}
}
