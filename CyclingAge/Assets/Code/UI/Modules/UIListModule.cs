using Assets.Code.UI.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Code.UI.Modules
{
	public abstract class UIListModule<TData, TElement> : UIModule where TElement : UIListElement<TData> where TData : class
	{
		public Transform Container;
		public TElement ElementPrefab;

		public Action<TElement> ElementClickedHandler;

		protected List<TData> Data { get; private set; }

		protected abstract IEnumerable<TData> GetBindingData();

		public IEnumerable<TElement> Elements => Container.GetComponentsInChildren<TElement>();
		public IEnumerable<TElement> Selected => Selected.Where(p => p.Toggled);

		public void SetSelected(IEnumerable<TData> data)
		{
			foreach (var e in Elements) {
				e.Toggled = (data.Contains(e.Data));

			}
		}

		public TElement GetByData(TData data)
		{
			return Elements.FirstOrDefault(e => e.Data == data);
		}

		public override void Refresh()
		{
			var encounters = GetBindingData();

			UIUtil.EnsureActiveComponentsInContainer(Container, ElementPrefab, encounters, (element, data) =>
			{
				element.Set(data);
				element.ClickHandler = (e) => ElementClickedHandler?.Invoke((TElement)e);
				element.Toggled = false;
			});
		}

	}
}
