using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Code.UI
{
	public static class UIUtil
	{
		public static T GetComponentUnderCursor<T>(PointerEventData eventData) where T : Component
		{
			List<RaycastResult> raycastResults = new List<RaycastResult>();
			EventSystem.current.RaycastAll(eventData, raycastResults);

			T hoveredComp = null;
			foreach (var result in raycastResults) {
				T comp = result.gameObject.GetComponent<T>();
				if (comp != null) {
					hoveredComp = comp;
					break;
				}
			}

			return hoveredComp;
		}

		//Make sure container has exact amount of active components. This will create, enable/disable game obejects to achive functionality
		public static void EnsureActiveComponentsInContainer<TComponent>(Transform container,
			TComponent prefab, int count, Action<TComponent> componentCallback) where TComponent : MonoBehaviour
		{

			TComponent[] existing = container.GetComponentsInChildren<TComponent>();
			for (int i = 0; i < count; ++i) {

				TComponent element = null;
				if (i < existing.Length) {
					element = existing[i];
					if (!element.gameObject.activeInHierarchy)
						element.gameObject.SetActive(true);
				}
				else {
					element = GameObject.Instantiate(prefab, container);
					if (!element.gameObject.activeInHierarchy)
						element.gameObject.SetActive(true);
				}

				componentCallback?.Invoke(element);
			}

			for (int j = count; j < existing.Length; ++j) {
				existing[j].gameObject.SetActive(false);
			}
		}

		public static IEnumerable<TComponent> EnsureActiveComponentsInContainer<TComponent>(Transform container,
			TComponent prefab, int count) where TComponent : MonoBehaviour
		{
			List<TComponent> result = new List<TComponent>(count);

			EnsureActiveComponentsInContainer(container, prefab, count, e => { result.Add(e); });

			return result;
		}

		public static void EnsureActiveComponentsInContainer<TComponent>(Transform container,
			TComponent prefab, int count, List<TComponent> outElements) where TComponent : MonoBehaviour
		{
			EnsureActiveComponentsInContainer(container, prefab, count, e => { outElements.Add(e); });
		}

		public static void EnsureActiveComponentsInContainer<TComponent, TElement>(Transform container,
			TComponent prefab, IEnumerable<TElement> elements, Action<TComponent, TElement> componentCallback) where TComponent : MonoBehaviour
		{
			int count = elements.Count();
			int i = 0;

			EnsureActiveComponentsInContainer(container, prefab, count, component =>
			{
				TElement element = elements.ElementAt(i);
				componentCallback?.Invoke(component, element);
				++i;
			});
		}

		/// Makes sure the container will have additional [count] elements active
		public static void EnsureAddActiveComponentsInContainer<TComponent>(Transform container, TComponent prefab, int count, List<TComponent> outAddedList) where TComponent : MonoBehaviour
		{
			TComponent[] existing = container.GetComponentsInChildren<TComponent>();

			int newTotal = existing.Length;
			int active = 0;

			for (int i = 0; i < existing.Length; ++i) {

				TComponent element = existing[i];
				if (!element.gameObject.activeInHierarchy) {
					element.gameObject.SetActive(true);
					outAddedList.Add(element);
					--count;
				}
				++active;
			}

			for (int i = 0; i < count; ++i) {
				TComponent element = GameObject.Instantiate(prefab, container);

				if (!element.gameObject.activeInHierarchy)
					element.gameObject.SetActive(true);
				outAddedList.Add(element);
				++active;
				++newTotal;
			}

			for (int j = active; j < newTotal; ++j) {
				existing[j].gameObject.SetActive(false);
			}
		}
	}
}
