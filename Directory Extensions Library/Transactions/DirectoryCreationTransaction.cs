using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory_Extensions_Library.Transactions
{
	public class DirectoryCreationTransaction : ITransaction
	{
		private readonly string _directoryName;
		private bool _alreadyExecuted;

		public DirectoryCreationTransaction(string directoryName)
		{
			_directoryName = directoryName;
		}

		public void Apply()
		{
			if (_alreadyExecuted) return;

			_alreadyExecuted = true;

			Directory.CreateDirectory(_directoryName);
		}

		public void Undo()
		{
			if (!_alreadyExecuted) return;

			_alreadyExecuted = false;

			Directory.Delete(_directoryName);
		}
	}
}
