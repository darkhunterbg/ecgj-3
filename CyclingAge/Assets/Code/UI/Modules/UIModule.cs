using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.UI
{
	public  abstract class UIModule : MonoBehaviour
	{
		public void Show()
		{
			gameObject.SetActive(true);
			Refresh();
		}
		public void Hide()
		{
			gameObject.SetActive(false);
		}

		public abstract void Refresh();
	}
}
