using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_Library.Model
{
	public class DuplicateAllowedSack<T> : ISack<T>
	{
		private readonly IList<IItem<T>> _items = new List<IItem<T>>();

		public long Capacity { get; private set; }

		public long CurrentValue
		{
			get { return _items.Sum(t => t.Value); }
		}

		public long CurrentWeight
		{
			get { return _items.Sum(t => t.Weight); }
		}

		public IEnumerable<IItem<T>> Items
		{
			get { return _items; }
		}

		public void AddItem(IItem<T> item)
		{
			_items.Add(item);
		}

		public DuplicateAllowedSack(long capacity)
		{
			Capacity = capacity;
		}
	}
}
