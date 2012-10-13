using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive_Normalization.Model
{
	internal class RemoveVisitor : IVisitor
	{
		private readonly IEnumerable<Group> _groupsToRemove;
		private bool _hasRemoved;

		public RemoveVisitor(IEnumerable<Group> groupsToRemove)
		{
			_groupsToRemove = groupsToRemove;
		}

		public void Visit(ICollection<Group> groups)
		{
			if (_hasRemoved) return;

			foreach (var g in _groupsToRemove)
			{
				groups.Remove(g);
			}

			_hasRemoved = true;
		}
	}
}
