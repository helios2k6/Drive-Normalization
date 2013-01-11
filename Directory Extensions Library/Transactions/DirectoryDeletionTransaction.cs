using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory_Extensions_Library.Transactions
{
	public class DirectoryDeletionTransaction : PermanentTransaction
	{
		private readonly string _directoryName;
		private bool _alreadyExecuted;

		public DirectoryDeletionTransaction(string directoryName)
		{
			_directoryName = directoryName;
		}

		public override void Apply()
		{
			if (_alreadyExecuted) return;

			_alreadyExecuted = true;

			Directory.Delete(_directoryName);
		}
	}
}
