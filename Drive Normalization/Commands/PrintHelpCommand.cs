using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive_Normalization.Commands
{
	public class PrintHelpCommand : ICommand<bool>
	{
		public void RunCommand(bool t)
		{
			var builder = new StringBuilder();

			builder.Append("Usage: Normalize.exe <config file>").AppendLine().AppendLine();

			Console.WriteLine(builder.ToString());
		}
	}
}
