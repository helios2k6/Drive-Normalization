using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive_Normalization.Model
{
	public interface IVisitor
	{
		void Visit(ICollection<Group> groups);
	}
}
