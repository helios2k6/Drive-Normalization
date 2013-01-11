using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory_Extensions_Library.Transactions
{
	public class FileDeleteTransaction : PermanentTransaction
	{
		private readonly string _fileToDelete;
		private bool _alreadyExecuted;

		public FileDeleteTransaction(string fileToDelete)
		{
			_fileToDelete = fileToDelete;
		}

		public override void Apply()
		{
			if (_alreadyExecuted) return;

			_alreadyExecuted = true;

			File.Delete(_fileToDelete);
		}
	}
}
