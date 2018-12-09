using Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
	abstract class BaseMethod
	{
		public int Count { get; protected set; }

		public BaseMethod(int count)
		{
			Count = count;
		}

		public abstract Vector FindSolution(SquareMatrix inputMatrix, Vector inputElements);
	}
}
