using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory_Extensions_Library.Transactions
{
	/// <summary>
	/// Represents a file copy transaction
	/// </summary>
	public class FileCopyTransaction : ITransaction
	{
		private readonly string _oldPath;
		private readonly string _newPath;
		private bool _alreadyExecutedTransaction;

		/// <summary>
		/// Constructs a FileCopyTransaction object
		/// </summary>
		/// <param name="oldPath">The old file to copy</param>
		/// <param name="newPath">The path to the new file</param>
		public FileCopyTransaction(string oldPath, string newPath)
		{
			if (string.IsNullOrWhiteSpace(oldPath) || string.IsNullOrWhiteSpace(newPath)) throw new ArgumentNullException();

			_oldPath = oldPath;
			_newPath = newPath;
		}

		/// <summary>
		/// Copies the file from the old path to the new path if this transaction hasn't been run yet
		/// </summary>
		public void Apply()
		{
			if (_alreadyExecutedTransaction) return;

			_alreadyExecutedTransaction = true;

			File.Copy(_oldPath, _newPath);
		}

		/// <summary>
		/// If this transaction has been run already, it will delete the new file but it will not do anything to the old file
		/// </summary>
		public void Undo()
		{
			if (!_alreadyExecutedTransaction) return;

			_alreadyExecutedTransaction = false;

			File.Delete(_newPath);
		}
	}
}
