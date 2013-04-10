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
		private static string GetFolderName(string folder)
		{
			if (string.IsNullOrWhiteSpace(folder)) return string.Empty;

			var indexOfLastSeparator = folder.LastIndexOf(Path.DirectorySeparatorChar);
			var indexOfLastAltSeparator = folder.LastIndexOf(Path.AltDirectorySeparatorChar);

			var largerIndex = indexOfLastSeparator > indexOfLastAltSeparator ? indexOfLastSeparator : indexOfLastAltSeparator;

			return largerIndex + 1 >= folder.Length ? GetFolderName(folder.Substring(0, folder.Length - 1)) : folder.Substring(largerIndex + 1);
		}

		private static void TransferFolder(string sourceFolder, string destFolder)
		{
			//If destination folder does not exist, create it
			if (!Directory.Exists(destFolder))
			{
				Console.WriteLine(string.Format("\tDirectory {0} does not exist. Creating it", destFolder));
				Directory.CreateDirectory(destFolder);
			}

			//Check to see if there are any folders in this directory. If there are, then move them first
			var foldersInSourceDirectory = Directory.GetDirectories(sourceFolder);

			if (foldersInSourceDirectory.Any())
			{
				Console.WriteLine(string.Format("\tProcessing subdirectories of {0}", sourceFolder));
				foreach (var f in foldersInSourceDirectory)
				{
					var folderNameOnly = GetFolderName(f);
					TransferFolder(f, Path.Combine(destFolder, folderNameOnly));
				}
			}

			var filesInSourceDirectory = Directory.EnumerateFiles(sourceFolder);

			//Move all files from source folder
			foreach (var f in filesInSourceDirectory)
			{
				var fullDestPath = Path.Combine(destFolder, Path.GetFileName(f));
				Console.WriteLine(string.Format("\tMoving file from: {0} to {1}", f, fullDestPath));
				File.Move(f, fullDestPath);
			}

			//Delete old directory
			Console.WriteLine("Deleting directory {0}", sourceFolder);
			Directory.Delete(sourceFolder);
		}

		public void RunCommand(DriveTransactionManager transManager)
		{
			foreach (var t in transManager.TransactionTable)
			{
				foreach (var k in t.GroupsTransfered)
				{
					var newFilePath = Path.Combine(t.ToDrive.DrivePath, k.Name);
					Console.WriteLine(string.Format("Moving folder {0} to folder {1}", k.GroupPath, newFilePath));
					TransferFolder(k.GroupPath, newFilePath);
				}
			}
		}
	}
}
