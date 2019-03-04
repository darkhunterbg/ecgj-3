using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Code.UI
{
	public class UIButton : MonoBehaviour , IPointerEnterHandler, IPointerClickHandler
	{
		public AudioClip Clip;
		public AudioClip ClickClip;

		public void OnPointerClick(PointerEventData eventData)
		{
			GameController.Instance.AudioSource.PlayOneShot(ClickClip);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			GameController.Instance.AudioSource.PlayOneShot(Clip);
		}
	}
}
