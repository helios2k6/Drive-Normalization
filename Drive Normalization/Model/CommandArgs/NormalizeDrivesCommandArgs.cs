using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive_Normalization.Model.CommandArgs
{
	public class NormalizeDrivesCommandArgs
	{
		public IEnumerable<Drive> Drives { get; set; }
	}
}
