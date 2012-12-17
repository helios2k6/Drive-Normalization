using Drive_Normalization.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive_Normalization.Commands
{
	public class MoveFilesCommand : ICommand<DriveTransactionManager>
	{
		private interface TransactionVisitor
		{
			void Visit(FileTransaction transaction);
			void Visit(FolderTransaction transaction);
		}

		private class FileVisitor : TransactionVisitor
		{
			public void Visit(FileTransaction transaction)
			{
				switch (transaction.TransactionType)
				{
					case TransactionType.Copy:
						File.Copy(transaction.SrcFile, transaction.DestFile);
						break;
					case TransactionType.Delete:
						File.Delete(transaction.SrcFile);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}

			public void Visit(FolderTransaction transaction)
			{
				switch (transaction.TransactionType)
				{
					case TransactionType.Create:
						Directory.CreateDirectory(transaction.Folder);
						break;
					case TransactionType.Delete:
						Directory.Delete(transaction.Folder);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		private enum TransactionType
		{
			Create,
			Copy,
			Delete,
		}

		private abstract class Transaction
		{
			public TransactionType TransactionType { get; set; }

			public abstract void Accept(TransactionVisitor visitor);
		}

		private class FileTransaction : Transaction
		{
			public string SrcFile { get; set; }
			public string DestFile { get; set; }

			public override void Accept(TransactionVisitor visitor)
			{
				visitor.Visit(this);
			}
		}

		private class FolderTransaction : Transaction
		{
			public string Folder { get; set; }

			public override void Accept(TransactionVisitor visitor)
			{
				visitor.Visit(this);
			}
		}

		public void RunCommand(DriveTransactionManager transManager)
		{
			foreach (var t in transManager.TransactionTable)
			{
				foreach (var k in t.GroupsTransfered)
				{
					var newFilePath = Path.Combine(t.ToDrive.DrivePath, k.Name);
					Console.WriteLine(string.Format("Moving folder {0} to folder {1}", k.GroupPath, newFilePath));
				}
			}
		}
	}
}
