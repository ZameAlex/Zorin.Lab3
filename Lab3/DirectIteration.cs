using Matrix;
using static System.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
	class DirectIteration : BaseMethod
	{

		public DirectIteration(int count) : base(count)
		{
		}

		public override Vector FindSolution(SquareMatrix inputMatrix, Vector inputElements)
		{
			var error = 0d;
			var result = new Vector(inputElements);
			var tmp_x = new Vector(inputElements.Count);
			do
			{
				error = 0;
				tmp_x = new Vector(inputElements);
				for (int row = 0; row < Count; row++)
				{
					for (int column = 0; column < Count; ++column)
					{
						if (row != column)
						{
							tmp_x[row] -= inputMatrix[row, column] * result[column];
						}
					}
					var x_updated = tmp_x[row] / inputMatrix[row, row];
					var e = Abs(result[row] - x_updated);
					result[row] = x_updated;
					if (e > error)
					{ error = e; }
				}
			}
			while
			(error > Pow(10, -5));
			return result;
		}
	}
}
