﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_Library.Model
{
	internal class TwoDimensionalMatrix<T>
	{
		private readonly SortedList<long, SortedList<long, T>> _weightMatrix = new SortedList<long, SortedList<long, T>>();
		private long _virtualRowCount;
		private long _virtualColCount;

		public IEnumerable GetEnumerable()
		{
			return _weightMatrix;
		}

		public T this[long i, long j]
		{
			get { return (_weightMatrix.ContainsKey(i) && _weightMatrix[i].ContainsKey(j)) ? _weightMatrix[i][j] : default(T); }
			set
			{
				//Check row count
				if (_virtualRowCount <= i) _virtualRowCount = i + 1;

				//Check col count
				if (_virtualColCount <= j) _virtualColCount = j + 1;

				SortedList<long, T> row;
				if (!_weightMatrix.TryGetValue(i, out row))
				{
					row = new SortedList<long, T>();
					_weightMatrix[i] = row;
				}

				row[j] = value;
			}
		}

		public override string ToString()
		{
			var stringBuilder = new StringBuilder();

			for (var currentRow = 0; currentRow < _virtualRowCount; currentRow++)
			{
				for (var currentCol = 0; currentCol < _virtualColCount; currentCol++)
				{
					stringBuilder.Append(this[currentRow, currentCol]);
					if (currentCol + 1 != _virtualColCount)
					{
						stringBuilder.Append(", ");
					}
				}
				stringBuilder.AppendLine();
			}

			return stringBuilder.ToString();
		}
	}
}
