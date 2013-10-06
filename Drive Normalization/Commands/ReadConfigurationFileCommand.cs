using Drive_Normalization.CommandArgs.Model;
using Drive_Normalization.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Drive_Normalization.Commands
{
	public sealed class ReadConfigurationFileCommand : ICommand<string, ConfigFile>
	{
		public ConfigFile RunCommand(string t)
		{
			if (string.IsNullOrWhiteSpace(t)) throw new ArgumentException();

			using (var inputStream = new FileStream(t, FileMode.Open, FileAccess.Read))
			{
				var deserializer = new DataContractJsonSerializer(typeof(ConfigFile));
				var result = deserializer.ReadObject(inputStream) as ConfigFile;

				return result;
			}
		}

		void ICommand<string>.RunCommand(string t)
		{
			throw new NotImplementedException();
		}
	}
}
