using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_Library.Model
{
	public interface IItem<T>
	{
		T RawItem { get; }
		long Weight { get; }
		long Value { get; }
	}
}
