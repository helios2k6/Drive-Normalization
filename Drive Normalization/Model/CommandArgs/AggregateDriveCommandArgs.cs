using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Drive_Normalization.CommandArgs.Model
{
	[Serializable]
	[DataContract(Namespace = "http://nlogneg.com", Name = "Aggregate Drive Commands")]
	public sealed class AggregateDriveCommandArgs
	{
		[DataMember(IsRequired = true, Name = "Path")]
		public string DrivePath { get; set; }
		[DataMember(IsRequired = true, Name = "Max Space")]
		public long MaxAllowedSpace { get; set; }
	}
}
