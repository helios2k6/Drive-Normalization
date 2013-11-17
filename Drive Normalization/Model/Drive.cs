using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive_Normalization.Model
{
	public class Drive : IVisitable, IEquatable<Drive>
	{
		private readonly HashSet<Group> _groups = new HashSet<Group>();

		public IEnumerable<Group> Groups { get { return _groups; } }
		public string DrivePath { get; private set; }
		public long MaxAllowedSpace { get; private set; }
		public long CurrentDiskUsage { get { return _groups.Sum(f => f.Size); } }

		public Drive(string path, long maxAllowedSpace)
		{
			if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path)) throw new ArgumentException();

			DrivePath = path;
			MaxAllowedSpace = maxAllowedSpace;

			foreach (var d in Directory.GetDirectories(path))
			{
				_groups.Add(new Group(d));
			}
		}

		public void Accept(IVisitor visitor)
		{
			visitor.Visit(_groups);
		}

		public override int GetHashCode()
		{
			return DrivePath.GetHashCode()
				^ MaxAllowedSpace.GetHashCode()
				^ CurrentDiskUsage.GetHashCode()
				^ Groups.Aggregate(13, (agg, item) => agg ^ item.GetHashCode());
		}

		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(obj, null)) return false;
			if (object.ReferenceEquals(this, obj)) return true;

			if (this.GetType() != obj.GetType()) return false;

			return this.Equals(obj as Drive);
		}

		public bool Equals(Drive other)
		{
			return (DrivePath.Equals(other.DrivePath, StringComparison.OrdinalIgnoreCase));
		}

		public override string ToString()
		{
			return DrivePath;
		}
	}
}
