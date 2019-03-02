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
		public Transform PlacableZonesParent;

		public IEnumerable<PlacableZone> PlacableZones => PlacableZonesParent.GetComponentsInChildren<PlacableZone>();

		public int RemainingObstacles => 3 - PlacableObstaclesParent.GetComponentsInChildren<PlacableObstacle>().Count();

		public bool CanStartGame => RemainingObstacles == 0;

		public static Level Instance => FindObjectOfType<Level>();

		public void Start()
		{
			_disc = FindObjectOfType<Disc>();
		

		}


		public void StartDisc()
		{
			RunDisc(null);
		}
		public void SimulateDisc()
		{
			RunDisc(Setup);
		}

		private void Setup()
		{
			_disc.ResetDisc();

			foreach (var zone in PlacableZones) {
				zone.SetVisualsVisible(true);
			}

			GameView.Instance.ObstacleModule.Show();
			GameView.Instance.ToolBar.Show();
		}


		private void RunDisc(Action callback)
		{
			GameView.Instance.ObstacleModule.Hide();
			GameView.Instance.ToolBar.Hide();

			foreach (var zone in PlacableZones) {
				zone.SetVisualsVisible(false);
			}

			_disc.ResetDisc();
			_disc.Play(callback);
		}


	}
}
