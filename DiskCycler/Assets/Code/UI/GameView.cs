using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.UI
{
	public class GameView : MonoBehaviour
	{
		public static GameView Instance => FindObjectOfType<GameView>();

		public UIObstacleModule ObstacleModule;
		public UIToolBar ToolBar;
		public UISimulation Simulation;

		public UIModal DiscDestroyedModal;
		public UIModal DiscEscapedModal;
		public UIModal VictoryModal;
		

		private List<UIModal> _modals = new List<UIModal>();


		public void Start()
		{
		}


		public void ShowModal(UIModal modal)
		{
			HideAllModals();
			modal.Show();
		}

		public void HideAllModals()
		{
			foreach (var modal in GetComponentsInChildren<UIModal>(includeInactive: true))
				modal.Hide();
		}

		public void RestartLevel()
		{
			Level.Instance.RestartLevel();
		}

		public void NextLevel()
		{
			GameController.Instance.EndLevel(false);
		}
		public void MainMenu()
		{
			GameController.Instance.EndLevel(true);
		}
		public void Exit()
		{
			Application.Quit();
		}
	}
}
