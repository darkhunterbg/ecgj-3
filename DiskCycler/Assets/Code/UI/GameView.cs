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

		public UIModal DiscDestroyedModal;

		private List<UIModal> _modals = new List<UIModal>();

		public void Start()
		{
			HideAllModals();
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
	}
}
