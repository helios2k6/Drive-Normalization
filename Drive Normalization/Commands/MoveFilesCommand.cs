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
		public void RunCommand(DriveTransactionManager transManager)
		{
			foreach (var t in transManager.TransactionTable)
			{
				foreach (var k in t.GroupsTransfered)
				{
					var newFilePath = Path.Combine(t.ToDrive.DrivePath, k.Name);
					Console.WriteLine(string.Format("Moving folder {0} to folder {0}", k.GroupPath, newFilePath));
					File.Move(k.GroupPath, newFilePath);
				}
			}
		}
	}
}
