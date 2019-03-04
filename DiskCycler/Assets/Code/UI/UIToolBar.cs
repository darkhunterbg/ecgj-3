using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI
{
	public class UIToolBar : MonoBehaviour
	{
		public Button StartButton;
		public Button ObstacleButton;
		public Button SimulateButton;
		public Button LaserButton;
		public Text ObstaclesCountText;
		public Text LaserCountText;

		public PlacableObstacle ObstaclePrefab;
		public PlacableObstacle LaserPrefab;

		public Action OnStartClicked;

		public ProgressBar EnergyBar;

		public void StartClicked()
		{
			Level.Instance.StartDisc();
		}

		public void UseObstacleClicked()
		{
			GameView.Instance.ObstacleModule.UseObstacle(ObstaclePrefab);
		}

		public void UseLaserClicked()
		{
			GameView.Instance.ObstacleModule.UseObstacle(LaserPrefab);
		}

		public void SimulateClicked()
		{
			Level.Instance.SimulateDisc();
		}

		public bool DisableAll = false;

		public void Show()
		{
			EnergyBar.gameObject.SetActive(Level.Instance.Disc.IsSlowdownActive);

			gameObject.SetActive(true);

			if (Level.Instance.StartingObstacles > 0) {
				ObstacleButton.gameObject.SetActive(true);
				ObstaclesCountText.gameObject.SetActive(true);
			}
			else {
				ObstacleButton.gameObject.SetActive(false);
				ObstaclesCountText.gameObject.SetActive(false);
			}

			if (Level.Instance.StartingLasers > 0) {
				LaserButton.gameObject.SetActive(true);
				LaserCountText.gameObject.SetActive(true);
			}
			else {
				LaserButton.gameObject.SetActive(false);
				LaserCountText.gameObject.SetActive(false);
			}

		}
		public void Hide()
		{
			gameObject.SetActive(false);
		}

		public void SetSimulationMode(bool isSimulating)
		{
		}

		public void UpdateState()
		{
			bool nonInteraccable = Level.Instance.Disc.IsPlaying || DisableAll;

			StartButton.interactable = !nonInteraccable && Level.Instance.CanStartGame;
			SimulateButton.interactable = !nonInteraccable && Level.Instance.SimCharges > 0;


			ObstacleButton.interactable = !nonInteraccable && Level.Instance.RemainingObstacles > 0;
			ObstaclesCountText.text = $"x { Level.Instance.RemainingObstacles}";

			LaserButton.interactable = !nonInteraccable && Level.Instance.RemainingLasers > 0;
			LaserCountText.text = $"x { Level.Instance.RemainingLasers}";

			int value = (int)((Level.Instance.Disc.Energy * 100.0f) / Level.Instance.Disc.MaxEnergy);
			EnergyBar.progress = (value / 5) * 5;
		}

		public void Update()
		{
			UpdateState();
		}
	}
}
