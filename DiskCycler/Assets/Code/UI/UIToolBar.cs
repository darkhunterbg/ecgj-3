using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI
{
	public class UIToolBar : MonoBehaviour
	{
		public Button StartButton;
		public Button ObstacleButton;
		public Button SimulateButton;
		public Text ObstaclesCountText;

		public PlacableObstacle ObstaclePrefab;

		public Action OnStartClicked;

		public void StartClicked()
		{
			Level.Instance.StartDisc();
		}

		public void UseObstacleClicked()
		{
			GameView.Instance.ObstacleModule.UseObstacle(ObstaclePrefab);
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
			ObstacleButton.interactable = !Level.Instance.CanStartGame;
			SimulateButton.interactable = Level.Instance.SimCharges > 0;
			ObstaclesCountText.text = $"x { Level.Instance.RemainingObstacles}";
		}

		public void Update()
		{
			UpdateState();
		}
	}
}
