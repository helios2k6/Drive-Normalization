using Knapsack_Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_Library.Commands
{
	public class ZeroOneKnapsackSolver<T> : ISolver<T>
	{
		public ISack<T> Solve(IList<IItem<T>> items, long maxWeight)
		{
			var resultMatrix = new TwoDimensionalMatrix<long>();
			var keepMatrix = new TwoDimensionalMatrix<bool>();

			var itemCount = items.Count;

			for (var i = 1; i <= itemCount; i++)
			{
				var weightAtIndex = items[i - 1].Weight;
				var valueAtIndex = items[i - 1].Value;

				for (var w = 0; w <= maxWeight; w++)
				{
					var newValue = valueAtIndex + resultMatrix[i - 1, w - weightAtIndex];
					var oldValue = resultMatrix[i - 1, w];
					if (weightAtIndex <= w && newValue > oldValue)
					{
						resultMatrix[i, w] = newValue;
						keepMatrix[i, w] = true;
					}
					else
					{
						resultMatrix[i, w] = oldValue;
					}
				}
			}

			var defaultSack = new Sack<T>(maxWeight);

			var upperBound = maxWeight;
			for (var i = items.Count; i > 0; i--)
			{
				if (keepMatrix[i, upperBound])
				{
					defaultSack.AddItem(items[i - 1]);
					upperBound -= items[i - 1].Weight;
				}
			}

			return defaultSack;
		}
	}
}
