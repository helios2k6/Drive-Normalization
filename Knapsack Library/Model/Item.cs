using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_Library.Model
{
	public class Item<T> : IItem<T>
	{
		public Item(T item, long weight, long value)
		{
			RawItem = item;
			Weight = weight;
			Value = value;
		}

		public T RawItem { get; private set; }

		public long Weight { get; private set; }

		public long Value { get; private set; }
	}
}
