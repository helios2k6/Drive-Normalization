using Knapsack_Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_Library.Commands
{
	public class UnboundedKnapsackSolver<T> : ISolver<T>
	{
		public ISack<T> Solve(IList<IItem<T>> items, long maxWeight)
		{
			var resultMatrix = new TwoDimensionalMatrix<long>();
			var keepMatrix = new TwoDimensionalMatrix<bool>();

			var weightArray = (from w in items select w.Weight).ToList();
			var valueArray = (from v in items select v.Value).ToList();

			var itemCount = items.Count;

			for (var i = 1; i <= itemCount; i++)
			{
				var weightAtIndex = weightArray[i - 1];
				var valueAtIndex = valueArray[i - 1];
				for (var w = 0; w <= maxWeight; w++)
				{
					var newValue = resultMatrix[i, w - 1] + valueAtIndex;
					var previousValue = resultMatrix[i - 1, w];
					if (weightAtIndex <= w && newValue > previousValue)
					{
						resultMatrix[i, w] = newValue;
						keepMatrix[i, w] = true;
					}
					else
					{
						resultMatrix[i, w] = previousValue;
					}
				}
			}

			var duplicateSack = new DuplicateAllowedSack<T>(maxWeight);

			/*
			 * 1. Find the max value at the max weight
			 * 2. Figure out which items added to the max value
			 * 3. Count them
			 * 4. Add them to the bag
			 */
			for (var i = 1; i <= itemCount; i++)
			{
				if (keepMatrix[i, maxWeight])
				{
					//Count how many times we should add it to the sack
					for (var j = 0; j <= maxWeight; j++)
					{
						if (keepMatrix[i, j])
						{
							duplicateSack.AddItem(items[i - 1]); //The index for the item array is off by one 
						}
					}
				}
			}

			return duplicateSack;
		}
	}
}
