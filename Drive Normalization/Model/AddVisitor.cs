using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive_Normalization.Model
{
	internal class AddVisitor : IVisitor
	{
		private readonly IEnumerable<Group> _groupsToAdd;
		private bool _hasAdded;

		public AddVisitor(IEnumerable<Group> groupsToAdd)
		{
			_groupsToAdd = groupsToAdd;
		}

		public void Visit(ICollection<Group> groups)
		{
			if (_hasAdded) return;
			foreach (var g in _groupsToAdd)
			{
				groups.Add(g);
			}

			_hasAdded = true;
		}
	}
}
