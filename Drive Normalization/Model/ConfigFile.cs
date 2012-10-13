using Drive_Normalization.CommandArgs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Drive_Normalization.Model
{
	[Serializable]
	[DataContract(Namespace = "http://nlogneg.com", Name = "Config")]
	public class ConfigFile
	{
		[DataMember(Name = "Drives", IsRequired = true)]
		public IEnumerable<AggregateDriveCommandArgs> DriveArgs { get; set; }

		[DataMember(Name = "Dry Run", IsRequired = false)]
		public bool DryRun { get; set; }
	}
}
