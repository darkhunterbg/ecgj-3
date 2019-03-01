using Assets.Code.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code
{
	public class GameController : MonoBehaviour
	{
		public static GameController Instance => FindObjectOfType<GameController>();

		public GameCycle Cycle = new GameCycle() { Number = 1 };


		public void Start()
		{
			Cycle.Begin();
			GameView.Instance.Initialize(this);
			GameView.Instance.InitialView();
		}

		public void Update()
		{

		}

		public void NextTurn()
		{
			Cycle.NextTurn();
			GameView.Instance.InitialView();
		}


	}
}
