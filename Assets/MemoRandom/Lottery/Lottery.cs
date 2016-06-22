using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Kowa
{
	namespace MemoRandom
	{
		public class Lottery<T>
		{
			public IList<T> Collection { get; private set; }
			public List<T> CollectionCopy { get; private set; }

			/// <summary>
			/// Constructor: Sets Collection and copies them.
			/// </summary>
			/// <param name="newCollection">The passed generic list</param>
			public Lottery(IList<T> newCollection)
			{
				Collection = newCollection;
				CopyEntries();
			}

			/// <summary>
			/// Copies all entries
			/// </summary>
			private void CopyEntries()
			{
				CollectionCopy = Collection.ToList();
			}

			/// <summary>
			/// Gets and removes the next random entry. Refills on empty.
			/// </summary>
			/// <returns>Returns next entry</returns>
			public T DrawNext()
			{
				return DrawNext(LotteryMode.RefillOnEmpty);
			}
			
			/// <summary>
			/// Overload: Gets the next random entry. Refills or 
			/// </summary>
			/// <param name="mode">The mode to determine how to behave, when copied list is empty</param>
			/// <returns>Returns next entry or default(T), depending on mode</returns>
			public T DrawNext(LotteryMode mode)
			{
				if ( IsEmtpy () )
				{
					switch ( mode )
					{
						case LotteryMode.RefillOnEmpty:
							CopyEntries ();
							break;
						case LotteryMode.RemoveOnEmpty:
							LotteryManager<T>.Remove ( Collection );
							return default ( T );
						case LotteryMode.None:
							return default ( T );
					}
				}
				var id = Random.Range ( 0, CollectionCopy.Count );
				var temp = CollectionCopy[id];
				CollectionCopy.Remove ( CollectionCopy[id] );
				return temp;
			}
			
			/// <summary>
			/// Determines whether the copied list is empty
			/// </summary>
			/// <returns>Returns True, if copied list is empty</returns>
			public bool IsEmtpy()
			{
				return CollectionCopy.Count == 0;
			}
		}
	}
}
