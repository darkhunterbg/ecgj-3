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
		public Disc Disc { get; private set; }

		public Transform PlacableObstaclesParent;
		public Transform PlacableZonesParent;

		public IEnumerable<PlacableZone> PlacableZones => PlacableZonesParent.GetComponentsInChildren<PlacableZone>();

		public int RemainingObstacles => StartingObstacles - PlacableObstaclesParent.GetComponentsInChildren<PlacableObstacle>().Count(p => p.Type == ObstacleType.Obstacle);
		public int RemainingLasers => StartingLasers - PlacableObstaclesParent.GetComponentsInChildren<PlacableObstacle>().Count(p => p.Type == ObstacleType.Laser);

		public int StartingSimCharges = 1;
		public int StartingObstacles = 3;
		public int StartingLasers = 0;
		public int SimCharges = 0;
		public bool IsTutorial = false;
		public bool HasSlowdown = true;


		public bool CanStartGame => RemainingObstacles == 0 && RemainingLasers == 0;

		public static Level Instance => FindObjectOfType<Level>();


		public void Start()
		{
			SimCharges = StartingSimCharges;

			Disc = FindObjectOfType<Disc>();
			Disc.Init();
			Disc.ResetDisc();
			Disc.IsSlowdownActive = HasSlowdown;


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

		private void SetLasersRunning(bool running)
		{
			var lasers = PlacableObstaclesParent.GetComponentsInChildren<PlacableLaser>();
			foreach (var laser in lasers) {
				if (running)
					laser.StartShooting();
				else
					laser.StopShooting();

			}
		}

		public void StartDisc()
		{
			SetLasersRunning(true);
			GameView.Instance.ToolBar.DisableAll = true;

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
			SetLasersRunning(true);
			RunDisc((Collider2D collider) =>
			{
				Setup();
			});
		}

		private void Setup()
		{
			SetLasersRunning(false);

			Disc.ResetDisc();

			GameView.Instance.ObstacleModule.Show();
			GameView.Instance.ToolBar.Show();
		}


		public IEnumerator GameOver()
		{
			Disc.Kill();

			yield return new WaitForSeconds(2.0f);

			SetLasersRunning(false);

			GameView.Instance.DiscDestroyedModal.Show();
		}
		public IEnumerator DiscEscaped()
		{
			Disc.Stop();

			yield return new WaitForSeconds(1.0f);

			SetLasersRunning(false);

			GameView.Instance.DiscEscapedModal.Show();
		}

		private void RunDisc(Action<Collider2D> callback)
		{
			GameView.Instance.ObstacleModule.Hide();
			//GameView.Instance.ToolBar.Hide();


			Disc.ResetDisc();
			Disc.Play(callback);
		}

		public void RestartLevel()
		{
			foreach (var obstacle in PlacableObstaclesParent.GetComponentsInChildren<PlacableObstacle>().ToArray()) {
				GameObject.Destroy(obstacle.gameObject);
			}

			GameView.Instance.ToolBar.DisableAll = false;
			SimCharges = StartingSimCharges;
			GameView.Instance.HideAllModals();
			Setup();
		}


		
	}
}
