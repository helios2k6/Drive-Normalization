using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive_Normalization.Model
{
	public class Group
	{
		private const double ByteGroupSize = 1000000.0;

		private readonly Lazy<long> _size;
		private readonly Lazy<IEnumerable<string>> _files;

		public string Name { get; private set; }
		public string GroupPath { get; private set; }
		public long Size { get { return _size.Value; } }
		public IEnumerable<string> Files { get { return _files.Value; } }

		public Group(string path, string name)
		{
			if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(path) || !Directory.Exists(path)) throw new ArgumentException();

			Name = name;
			GroupPath = path;

			_files = new Lazy<IEnumerable<string>>(new Func<IEnumerable<string>>(() => Directory.GetFiles(path, "*", SearchOption.AllDirectories)));
			_size = new Lazy<long>(new Func<long>(() => _files.Value.Sum(t => 
			{
				var lengthInBytes = new FileInfo(t).Length;
				return (long)Math.Round((double)lengthInBytes / ByteGroupSize);
			})));
		}

		public Group(string path) : this(path, Path.GetFileName(path)) { }

		public override string ToString()
		{
			return Name;
		}
	}
}
