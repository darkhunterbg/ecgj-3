using Assets.Code.UI;
using System;
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

		public int RemainingObstacles => 3 - PlacableObstaclesParent.GetComponentsInChildren<PlacableObstacle>().Count();

		public bool CanStartGame => RemainingObstacles == 0;

		public static Level Instance => FindObjectOfType<Level>();

		public void Start()
		{
			_disc = FindObjectOfType<Disc>();
			_disc.ResetDisc();

			GameView.Instance.ObstacleModule.Show();
			GameView.Instance.ToolBar.Show();

		}

		public void SimulateDisc()
		{
			GameView.Instance.ToolBar.Hide();
			_disc.ResetDisc();
			_disc.Play(() =>
			{
				_disc.ResetDisc();
				GameView.Instance.ToolBar.Show();
			});
		}

		public void StartDisc()
		{
			GameView.Instance.ObstacleModule.Hide();
			GameView.Instance.ToolBar.Hide();
			_disc.ResetDisc();
			_disc.Play(null);
		}
	}
}
