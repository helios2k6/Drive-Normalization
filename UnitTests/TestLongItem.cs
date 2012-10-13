using Knapsack_Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
	internal class TestLongItem : IItem<long>
	{
		public TestLongItem(long weight, long value)
		{
			Weight = weight;
			Value = value;
		}

		public long Weight { get; private set; }

		public long Value { get; private set; }

		public long RawItem
		{
			get { return 0; }
		}
	}
}
