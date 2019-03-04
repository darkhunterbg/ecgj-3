using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI
{
	public enum UISimulationState
	{
		InProgress,
		Success,
		Fail
	}

	public class UISimulation : MonoBehaviour
	{
		public GameObject Borders;
		public Image Stripes;
		public Color InProgressColor = Color.white;
		public Color FailColor = Color.white;
		public Color SuccessColor = Color.white;

		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		public void SetSimulation(UISimulationState state)
		{
			switch(state) {
				case UISimulationState.InProgress:
					Stripes.color = InProgressColor;break;
				case UISimulationState.Fail:
					Stripes.color = FailColor; break;
				case UISimulationState.Success:
					Stripes.color = SuccessColor; break;
			}
		}
	}
}
