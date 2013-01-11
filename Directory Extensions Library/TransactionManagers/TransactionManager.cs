using Directory_Extensions_Library.Interfaces;
using Directory_Extensions_Library.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory_Extensions_Library.TransactionManagers
{
	public class TransactionManager : ITransactionManager
	{
		private readonly Stack<ISession> _sessionStack = new Stack<ISession>();
		private ISession _currentSession = new Session();

		public ISession GetCurrentSession()
		{
			return _currentSession;
		}

		public void CommitCurrentSession()
		{
			_currentSession.Close();

			_sessionStack.Push(_currentSession);

			foreach (var t in _currentSession.Transactions)
			{
				t.Apply();
			}
		}

		public void CancelCurrentSession()
		{
			_currentSession.Close();

			_currentSession = new Session();
		}

		public void UndoLastSession()
		{
			CancelCurrentSession();

			if (!_sessionStack.Any()) return;

			var lastSession = _sessionStack.Pop();
			foreach (var t in lastSession.Transactions)
			{
				t.Undo();
			}
		}
	}
}
