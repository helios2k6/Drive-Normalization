using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory_Extensions_Library.Transactions
{
	public abstract class PermanentTransaction : ITransaction
	{
		public abstract void Apply();

		public void Undo()
		{
			throw new NotImplementedException();
		}
	}
}
