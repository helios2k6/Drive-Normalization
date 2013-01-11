using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory_Extensions_Library
{
	public interface ITransaction
	{
		void Apply();
		void Undo();
	}
}
