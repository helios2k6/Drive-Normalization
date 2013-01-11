using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory_Extensions_Library.Interfaces
{
	public interface ISession
	{
		void PushTransaction(ITransaction transaction);
		void Close();
		bool IsOpen { get; }
		IEnumerable<ITransaction> Transactions { get; }
	}
}
