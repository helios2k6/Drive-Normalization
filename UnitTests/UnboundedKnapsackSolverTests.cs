using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Knapsack_Library.Commands;
using Knapsack_Library.Model;

namespace UnitTests
{
	[TestClass]
	public class UnboundedKnapsackSolverTests
	{
		[TestMethod]
		public void UnboundedKnapsackSolutionTest()
		{
			var setOfItems = new List<IItem<long>>();
			for (var i = 1; i < 20; i++)
			{
				setOfItems.Add(new TestLongItem(i, 1));
			}

			var solver = new UnboundedKnapsackSolver<long>();
			var result = solver.Solve(setOfItems, 10);
		}
	}
}
