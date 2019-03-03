using Assets.Code.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code
{
	public enum LevelState
	{
		Setup,
		Playing,

	}


	[DefaultExecutionOrder(-100)]
	public class Level : MonoBehaviour
	{
		private Disc _disc;

		public Transform PlacableObstaclesParent;
		public Transform PlacableZonesParent;

		public IEnumerable<PlacableZone> PlacableZones => PlacableZonesParent.GetComponentsInChildren<PlacableZone>();

		public int RemainingObstacles => StartingObstacles - PlacableObstaclesParent.GetComponentsInChildren<PlacableObstacle>().Count(p=>p.Type== ObstacleType.Obstacle);
		public int RemainingLasers => StartingLasers - PlacableObstaclesParent.GetComponentsInChildren<PlacableObstacle>().Count(p => p.Type == ObstacleType.Laser);

		public int StartingSimCharges = 1;
		public int StartingObstacles = 3;
		public int StartingLasers = 0;
		public int SimCharges = 0;
		public bool IsTutorial = false;

		public bool CanStartGame => RemainingObstacles == 0 && RemainingLasers == 0;

		public static Level Instance => FindObjectOfType<Level>();

		public void Start()
		{
			SimCharges = StartingSimCharges;

			_disc = FindObjectOfType<Disc>();
			_disc.Init();
			_disc.ResetDisc();
			

			GameView.Instance.HideAllModals();


			if (IsTutorial) {
				GameView.Instance.TutorialModal.Show();
				GameView.Instance.ObstacleModule.Hide();
				GameView.Instance.ToolBar.Hide();
			}
			else {
				GameView.Instance.ObstacleModule.Show();
				GameView.Instance.ToolBar.Show();
			}
		}

		public void StartDisc()
		{
			RunDisc((Collider2D collider) =>
			{
				if (collider.gameObject.GetComponentInParent<ExitZone>())
					StartCoroutine(DiscEscaped());
				else
					StartCoroutine(GameOver());
			});
		}
		public void SimulateDisc()
		{
			--SimCharges;
			RunDisc((Collider2D collider) =>
			{
				Setup();
			});
		}

		private void Setup()
		{
			_disc.ResetDisc();

			GameView.Instance.ObstacleModule.Show();
			GameView.Instance.ToolBar.Show();
		}


		public IEnumerator GameOver()
		{
			_disc.Kill();

			yield return new WaitForSeconds(2.0f);

			GameView.Instance.DiscDestroyedModal.Show();
		}
		public IEnumerator DiscEscaped()
		{
			_disc.Stop();

			yield return new WaitForSeconds(1.0f);

			GameView.Instance.DiscEscapedModal.Show();
		}

		private void RunDisc(Action<Collider2D> callback)
		{
			GameView.Instance.ObstacleModule.Hide();
			GameView.Instance.ToolBar.Hide();


			_disc.ResetDisc();
			_disc.Play(callback);
		}

		public void RestartLevel()
		{
			foreach (var obstacle in PlacableObstaclesParent.GetComponentsInChildren<PlacableObstacle>().ToArray()) {
				GameObject.Destroy(obstacle.gameObject);
			}

			SimCharges = StartingSimCharges;
			GameView.Instance.HideAllModals();
			Setup();
		}
	}
}
