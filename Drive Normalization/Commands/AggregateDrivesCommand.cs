using Drive_Normalization.CommandArgs.Model;
using Drive_Normalization.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive_Normalization.Commands
{
	public sealed class AggregateDrivesCommand : ICommand<IEnumerable<AggregateDriveCommandArgs>, IEnumerable<Drive>>
	{
		public IEnumerable<Drive> RunCommand(IEnumerable<AggregateDriveCommandArgs> drivePaths)
		{
			return drivePaths.Select(p => new Drive(p.DrivePath, p.MaxAllowedSpace));
		}

		void ICommand<IEnumerable<AggregateDriveCommandArgs>>.RunCommand(IEnumerable<AggregateDriveCommandArgs> t)
		{
			throw new NotImplementedException();
		}
	}
}
