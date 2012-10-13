using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive_Normalization.Commands
{
	public interface ICommand<T>
	{
		void RunCommand(T t);
	}

	public interface ICommand<T, K> : ICommand<T>
	{
		K RunCommand(T t);
	}
}
