using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory_Extensions_Library.Interfaces
{
	public interface ITransactionManager
	{
		ISession GetCurrentSession();
		void CommitCurrentSession();
		void CancelCurrentSession();
		void UndoLastSession();
	}
}
