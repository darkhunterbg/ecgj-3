using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
	public static class LinqUtils
	{
		public static T GetRandomElement<T>(this IEnumerable<T> col)
		{
			int randomIndex = UnityEngine.Random.Range(0, col.Count());
			return col.ElementAt(randomIndex);
		}

		public static T WeightedRandomElement<T>(this IEnumerable<T> col, Func<T, float> weightFn)
		{

			float sumWeights = col.Sum(weightFn);
			if (sumWeights < float.Epsilon)
				throw new InvalidOperationException("WeightedRandomElement: All element weights are zero");

			var rnd = (float)UnityEngine.Random.Range(0, 1.0f) * sumWeights;
			float sum = 0;

			foreach (var el in col) {
				sum += weightFn(el);

				if (rnd < sum) {
					return el;
				}
			}

			throw new InvalidOperationException("How did you end up here?");
		}

		public static IList<T> Shuffle<T>(this IList<T> list)
		{
			for (int i = list.Count - 1; i > 0; i--) {
				int randomIndex = UnityEngine.Random.Range(0, i + 1);
				T temp = list[randomIndex];
				list[randomIndex] = list[i];
				list[i] = temp;
			}

			return list;
		}
		public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
		{
			foreach (var item in list) {
				action.Invoke(item);
			}
		}

		public static int FindIndex<T>(this T[] arr, Predicate<T> pred)
		{
			return Array.FindIndex(arr, pred);
		}

	}
}
