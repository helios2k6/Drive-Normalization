using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_Library.Model
{
	public class Sack<T> : ISack<T>
	{
		private readonly HashSet<IItem<T>> _items = new HashSet<IItem<T>>();

		public long Capacity { get; private set; }

		public long CurrentValue
		{
			get { return _items.Sum(t => t.Value); }
		}

		public long CurrentWeight
		{
			get { return _items.Sum(t => t.Weight); }
		}

		public void AddItem(IItem<T> item)
		{
			_items.Add(item);
		}

		public Sack(long capacity)
		{
			Capacity = capacity;
		}


		public IEnumerable<IItem<T>> Items
		{
			get { return _items; }
		}
	}
}
