using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_Library.Model
{
	public interface ISack<T>
	{
		long Capacity { get; }
		long CurrentValue { get; }
		long CurrentWeight { get; }

		IEnumerable<IItem<T>> Items { get; }
		void AddItem(IItem<T> item);
	}
}
