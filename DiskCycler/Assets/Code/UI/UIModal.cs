using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.UI
{
	public class UIModal : MonoBehaviour
	{

		public void Show()
		{
			gameObject.SetActive(true);


		}
		public void Hide()
		{
			gameObject.SetActive(false);
		}
	}
}
