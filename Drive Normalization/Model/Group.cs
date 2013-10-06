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

		/// <summary>
		/// Name of the group, which is usually the folder name.
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// Root folder of the group
		/// </summary>
		public string GroupPath { get; private set; }
		/// <summary>
		/// Size in Megabytes
		/// </summary>
		public long Size { get { return _size.Value; } }
		/// <summary>
		/// Files in the group
		/// </summary>
		public IEnumerable<string> Files { get { return _files.Value; } }

		/// <summary>
		/// A logical anime group, which is usually a folder of anime episodes
		/// </summary>
		/// <param name="path">The path of the group of anime files</param>
		/// <param name="name">The name of the group, which is usually the folder name</param>
		public Group(string path, string name)
		{
			if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(path) || !Directory.Exists(path)) throw new ArgumentException();

			Name = name;
			GroupPath = path;

			_files = new Lazy<IEnumerable<string>>(() => Directory.GetFiles(path, "*", SearchOption.AllDirectories));
			_size = new Lazy<long>(() => _files.Value.Sum(t => 
			{
				var lengthInBytes = new FileInfo(t).Length;
				return (long)Math.Round((double)lengthInBytes / ByteGroupSize);
			}));
		}

		public Group(string path) : this(path, Path.GetFileName(path)) { }

		public override string ToString()
		{
			return Name;
		}
	}
}
