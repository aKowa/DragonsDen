using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Kowa
{
	namespace MemoRandom
	{
		public static class ShuffleManager<T>
		{
			/// <summary>
			/// Shuffles the passed collection.
			/// </summary>
			/// <param name="collection">The collection to shuffle.</param>
			/// <returns>Returns shuffled collection.</returns>
			public static List<T> Shuffle ( IList<T> collection )
			{
				return collection.OrderBy ( x => Random.Range ( 0, collection.Count ) ).ToList ();
			}
		}
	}
}
