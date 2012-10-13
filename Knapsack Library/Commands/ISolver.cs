using Knapsack_Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_Library.Commands
{
	public interface ISolver<T>
	{
		ISack<T> Solve(IList<IItem<T>> items, long maxWeight);
	}
}
