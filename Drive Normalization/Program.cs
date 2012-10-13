using Drive_Normalization.CommandArgs.Model;
using Drive_Normalization.Commands;
using Drive_Normalization.Model;
using Drive_Normalization.Model.CommandArgs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Drive_Normalization
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			if (args.Length < 1)
			{
				var com = new PrintHelpCommand();
				com.RunCommand(true);
				return;
			}

			var timer = Stopwatch.StartNew();

			Console.WriteLine("Reading config file");
			var readConfigFileCommand = new ReadConfigurationFileCommand();
			var configFile = readConfigFileCommand.RunCommand(args[0]);

			Console.WriteLine("Aggregating drives");
			var aggregateCommand = new AggregateDrivesCommand();
			var drives = aggregateCommand.RunCommand(configFile.DriveArgs);

			Console.WriteLine("Normalizing drive space");
			var normalizeCom = new NormalizeDrivesCommand();
			var transactionManager = normalizeCom.RunCommand(new NormalizeDrivesCommandArgs { Drives = drives });

			Console.WriteLine(string.Format("Finished normalization in {0} ms", timer.ElapsedMilliseconds));

			Console.WriteLine("Results:");
			Console.WriteLine(transactionManager.ToString());
		}
	}
}
