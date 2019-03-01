using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Code.UI.Elements
{
	public  class UIListElement<TData> : UIToggleElement , IPointerClickHandler
	{
		[Header("ListElement")]
		public Action<UIListElement<TData>> ClickHandler;

		public TData Data;

		public virtual void Set(TData data)
		{
			Data = data;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			ClickHandler?.Invoke(this);
		}
	}
}
