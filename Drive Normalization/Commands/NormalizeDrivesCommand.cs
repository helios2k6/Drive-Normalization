using Drive_Normalization.Model;
using Drive_Normalization.Model.CommandArgs;
using Knapsack_Library.Commands;
using Knapsack_Library.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive_Normalization.Commands
{
	public sealed class NormalizeDrivesCommand : ICommand<NormalizeDrivesCommandArgs, DriveTransactionManager>
	{
		private const double EPSILON = 0.05;

		/// <summary>
		/// Attempts to normalize the amount of space on each drive. 
		/// </summary>
		/// <remarks>
		/// The algorithm for normalizing the drives is as follows:
		/// 
		/// Let N = the amount of files each drive should have (the normalized value)
		/// Let D = set of all drives, D' = set of all drives above N, D" = set of all drives below N
		/// 
		/// 
		/// 
		/// </remarks>
		/// <param name="t">Command args</param>
		/// <returns>A list of actions to execute in order</returns>
		public DriveTransactionManager RunCommand(NormalizeDrivesCommandArgs t)
		{
			var totalAmountOfAvailableSpace = (double)t.Drives.Sum(d => d.MaxAllowedSpace);
			var totalAmountOfTakenSpace = (double)t.Drives.Sum(d => d.CurrentDiskUsage);

			var idealPercentageRatio = totalAmountOfTakenSpace / totalAmountOfAvailableSpace;
			
			var drivesOverLimit = new List<Drive>();
			foreach (var k in t.Drives)
			{
				var drivePercentage = (double)k.CurrentDiskUsage / (double)k.MaxAllowedSpace;
				var percentAbove = drivePercentage - idealPercentageRatio;
				if (percentAbove > EPSILON)
				{
					drivesOverLimit.Add(k);
				}
			}

			var drivesUnderLimit = (from n in t.Drives
								   where !drivesOverLimit.Contains(n)
								   select n).ToList();

			if (!drivesUnderLimit.Any()) return new DriveTransactionManager();
			
			//Translate our group to IItems<Group> and pool them here
			var solver = new ZeroOneKnapsackSolver<Group>();
			var pooledAvailableItems = new List<IItem<Group>>();
			var groupToOriginalDriveMap = new Dictionary<Group, Drive>();

			foreach (var d in drivesOverLimit)
			{
				var amountOverLimit = d.CurrentDiskUsage - (long)Math.Round((double)d.MaxAllowedSpace * idealPercentageRatio);
				var listOfItems = new List<IItem<Group>>(from n in d.Groups select new Item<Group>(n, n.Size, n.Size));

				var availableFileResult = solver.Solve(listOfItems, amountOverLimit);

				foreach (var g in availableFileResult.Items)
				{
					pooledAvailableItems.Add(g);
					groupToOriginalDriveMap[g.RawItem] = d;
				}
			}

			var transactionManager = new DriveTransactionManager();
			for (var i = 0; i < drivesUnderLimit.Count - 1; i++)
			{
				var currentDrive = drivesUnderLimit[i];
				var amountUnderLimit = (long)Math.Round((double)currentDrive.MaxAllowedSpace * idealPercentageRatio) - currentDrive.CurrentDiskUsage;

				var result = solver.Solve(pooledAvailableItems, amountUnderLimit);

				foreach (var k in result.Items)
				{
					pooledAvailableItems.Remove(k);
					transactionManager.TransferGroupFromDriveToDrive(groupToOriginalDriveMap[k.RawItem], currentDrive, k.RawItem);
				}
			}

			foreach (var k in pooledAvailableItems)
			{
				transactionManager.TransferGroupFromDriveToDrive(groupToOriginalDriveMap[k.RawItem], drivesUnderLimit[drivesUnderLimit.Count - 1], k.RawItem);
			}

			return transactionManager;
		}

		void ICommand<NormalizeDrivesCommandArgs>.RunCommand(NormalizeDrivesCommandArgs t)
		{
			throw new NotImplementedException();
		}
	}
}
