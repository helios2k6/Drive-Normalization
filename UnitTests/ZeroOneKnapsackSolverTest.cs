using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Knapsack_Library.Model;
using System.Collections.Generic;
using Knapsack_Library.Commands;

namespace UnitTests
{
	[TestClass]
	public class ZeroOneKnapsackSolverTests
	{
		[TestMethod]
		public void ZeroOneKnapsackSolutionTest()
		{
			var setOfItems = new List<IItem<long>>();
			for (var i = 1; i < 20; i++)
			{
				setOfItems.Add(new TestLongItem(i, 1));
			}

			var solver = new ZeroOneKnapsackSolver<long>();
			var result = solver.Solve(setOfItems, 10);
		}
	}
}
