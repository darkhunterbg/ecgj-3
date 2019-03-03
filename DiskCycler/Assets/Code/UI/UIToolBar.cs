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

		public void Show()
		{
			gameObject.SetActive(true);


		}
		public void Hide()
		{
			gameObject.SetActive(false);
		}

		public void UpdateState()
		{
			StartButton.interactable = Level.Instance.CanStartGame;
			ObstacleButton.interactable = Level.Instance.RemainingObstacles > 0;
			SimulateButton.interactable = Level.Instance.SimCharges > 0;
			ObstaclesCountText.text = $"x { Level.Instance.RemainingObstacles}";

			LaserButton.interactable = Level.Instance.RemainingLasers > 0;
			LaserCountText.text = $"x { Level.Instance.RemainingLasers}";
		}

		public void Update()
		{
			UpdateState();
		}
	}
}
