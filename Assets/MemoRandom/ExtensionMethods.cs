using System.Collections.Generic;
using UnityEngine;

namespace Kowa
{
	namespace MemoRandom
	{
		// enables LotteryManager functionality to be called directly on any list
		public static class ExtensionMethods
		{
			// Returns next element from the passed IList<T> that has not been returned previously
			public static T DrawNext<T>(this IList<T> collection)
			{
				return LotteryManager<T>.DrawNext(collection);
			}

			// Returns true, if all elements of the passed IList<T> have been returned once
			public static bool IsEmpty<T>(this IList<T> collection)
			{
				return LotteryManager<T>.IsEmpty(collection);
			}

			// Disables remembering which elements of the passed IList<T> have been already returned
			// by removing from an internal tracking dictionary
			public static void Remove<T>(this IList<T> collection)
			{
				Debug.Log("Removed: " + collection.GetType());
				LotteryManager<T>.Remove(collection);
			}

			public static List<T> Shuffle<T>(this IList<T> list)
			{
				return MemoRandom.ShuffleManager<T>.Shuffle(list);
			}
		}
	}
}
