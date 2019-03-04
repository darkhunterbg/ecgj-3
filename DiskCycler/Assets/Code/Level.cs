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

		public UIModal ShowModalOnStart;

		public bool CanStartGame => RemainingObstacles == 0 && RemainingLasers == 0;

		public static Level Instance => FindObjectOfType<Level>();


		public void Start()
		{
			SimCharges = StartingSimCharges;

			Disc = FindObjectOfType<Disc>();
			Disc.Init();
			Disc.ResetDisc();


			GameView.Instance.HideAllModals();
			GameView.Instance.Simulation.Hide();

			if (ShowModalOnStart != null) {
				GameView.Instance.ShowModal(ShowModalOnStart);
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
			}, simulation: false);
		}
		public void SimulateDisc()
		{
			--SimCharges;
			GameView.Instance.ToolBar.DisableAll = true;
			SetLasersRunning(true);
			GameView.Instance.Simulation.Show();
			GameView.Instance.Simulation.SetSimulation(UISimulationState.InProgress);
			RunDisc((Collider2D collider) =>
			{
				if (collider.gameObject.GetComponentInParent<ExitZone>())
					StartCoroutine(SimDiscEscaped());
				else
					StartCoroutine(SimGameOver());
			}, simulation: false);
		}

		private void Setup()
		{
			GameView.Instance.Simulation.Hide();

			GameView.Instance.ToolBar.DisableAll = false;
			SetLasersRunning(false);

			Disc.ResetDisc();

			GameView.Instance.ObstacleModule.Show();
			GameView.Instance.ToolBar.Show();
		}

		public IEnumerator SimGameOver()
		{
			Disc.DiscPause();
			Disc.Visuals.color = Disc.FailColor;

			GameView.Instance.Simulation.SetSimulation(UISimulationState.Fail);


			yield return new WaitForSeconds(1.5f);

			SetLasersRunning(false);

			Setup();
		}

		public IEnumerator SimDiscEscaped()
		{
			Disc.Stop();

			Disc.ResetDisc();

			GameView.Instance.Simulation.SetSimulation(UISimulationState.Success);


			yield return new WaitForSeconds(1.5f);

			SetLasersRunning(false);

			Setup();
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

			yield return null;
			//yield return new WaitForSeconds(1.0f);

			SetLasersRunning(false);

			GameView.Instance.DiscEscapedModal.Show();
		}

		private void RunDisc(Action<Collider2D> callback, bool simulation)
		{
			GameView.Instance.ObstacleModule.Hide();
			//GameView.Instance.ToolBar.Hide();


			Disc.ResetDisc();
			Disc.Play(callback, simulation: simulation);
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
