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

		public int AccuracyPower { get; set; }
		public DirectIteration(int count, int accuracyPower) : base(count)
		{
			AccuracyPower = accuracyPower;
		}

		private double M_Norm(SquareMatrix matrix)
		{
			var max = 0d;
			for (var row = 0; row < matrix.Count; row++)
			{
				var tmp = 0d;
				for (var column = 0; column < matrix.Count; column++)
				{
					if(row!=column)
						tmp += matrix[row, column]/matrix[row,row];
				}

				if (Abs(tmp) > max)
					max = Abs(tmp);
			}

			return max;
		}

		public override Vector FindSolution(SquareMatrix inputMatrix, Vector inputElements)
		{
			var q = M_Norm(inputMatrix);
			var epsilon = Pow(10, AccuracyPower) * (1 - q) / q;
			var error = 0d;
			var result = new Vector(inputElements);
			var tmpX = new Vector(inputElements.Count);
			do
			{
				error = 0;
				tmpX = new Vector(inputElements);
				for (int row = 0; row < Count; row++)
				{
					for (int column = 0; column < Count; ++column)
					{
						if (row != column)
						{
							tmpX[row] -= inputMatrix[row, column] * result[column];
						}
					}
					var xUpdated = tmpX[row] / inputMatrix[row, row];
					var e = Abs(result[row] - xUpdated);
					result[row] = xUpdated;
					if (e > error)
					{
						error = e;
					}
				}
			}
			while
			(error > epsilon);
			return result;
		}
	}
}
