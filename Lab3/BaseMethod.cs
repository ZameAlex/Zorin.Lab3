using Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
	internal abstract class BaseMethod
	{
		public int Count { get; protected set; }

		protected BaseMethod(int count)
		{
			Count = count;
		}

		public abstract Vector FindSolution(SquareMatrix inputMatrix, Vector inputElements);
	}
}
