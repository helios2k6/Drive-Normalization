using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive_Normalization.Model
{
	public class DriveTransaction
	{
		public Drive FromDrive { get; private set; }
		public Drive ToDrive { get; private set; }
		public IEnumerable<Group> GroupsTransfered { get; private set; }

		public DriveTransaction(Drive fromDrive, Drive toDrive, IEnumerable<Group> groupsTransfered)
		{
			FromDrive = fromDrive;
			ToDrive = toDrive;
			GroupsTransfered = groupsTransfered;
		}

		public override string ToString()
		{
			var builder = new StringBuilder();
			builder.Append(string.Format("From drive {0} to drive {1}", FromDrive, ToDrive)).AppendLine();
			foreach (var g in GroupsTransfered)
			{
				builder.Append("Send: " + g).AppendLine();
			}

			return builder.ToString();
		}
	}

	public class DriveTransactionManager
	{
		private readonly IList<DriveTransaction> _transactionTable = new List<DriveTransaction>();

		public IEnumerable<DriveTransaction> TransactionTable { get { return _transactionTable; } }

		public void TransferGroupsFromDriveToDrive(Drive fromDrive, Drive toDrive, IEnumerable<Group> groupsToTransfer)
		{
			_transactionTable.Add(new DriveTransaction(fromDrive, toDrive, groupsToTransfer));
			var addVisitor = new AddVisitor(groupsToTransfer);
			var removeVisitor = new RemoveVisitor(groupsToTransfer);

			toDrive.Accept(addVisitor);
			fromDrive.Accept(removeVisitor);
		}

		public void TransferGroupFromDriveToDrive(Drive fromDrive, Drive toDrive, Group group)
		{
			TransferGroupsFromDriveToDrive(fromDrive, toDrive, new List<Group> { group });
		}

		public override string ToString()
		{
			var builder = new StringBuilder();

			var orderedTable = from n in _transactionTable
							   orderby n.FromDrive.DrivePath
							   select n;

			if (!orderedTable.Any()) return "No Results";

			foreach (var k in orderedTable)
			{
				builder.Append(k.ToString()).AppendLine();
			}

			return builder.ToString();
		}
	}
}
