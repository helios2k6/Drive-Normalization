using Directory_Extensions_Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory_Extensions_Library.Sessions
{
	public class Session : ISession
	{
		private readonly Stack<ITransaction> _transactionStack = new Stack<ITransaction>();
		private bool _isOpen = true;

		public void PushTransaction(ITransaction transaction)
		{
			if (!_isOpen) throw new InvalidOperationException();

			_transactionStack.Push(transaction);
		}

		public void Close()
		{
			_isOpen = false;
		}

		public bool IsOpen
		{
			get { return _isOpen; }
		}

		public IEnumerable<ITransaction> Transactions
		{
			get { return _transactionStack; }
		}
	}
}
