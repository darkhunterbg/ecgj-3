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
	}
}
