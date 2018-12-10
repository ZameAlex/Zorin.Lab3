using Matrix;
using static System.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
	internal class DirectIteration : BaseMethod
	{

		public int AccuracyPower { get; set; }
		public DirectIteration(int count, int accuracyPower) : base(count)
		{
			AccuracyPower = accuracyPower;
		}

		

		public override Vector FindSolution(SquareMatrix inputMatrix, Vector inputElements)
		{
			var q = inputMatrix.M_Norm();
			var epsilon = Pow(10, AccuracyPower) * (1 - q) / q;
			var error = 0d;
			var result = new Vector(inputElements);
			do
			{
				error = 0;
				var tmpX = new Vector(inputElements);
				for (var row = 0; row < Count; row++)
				{
					for (var column = 0; column < Count; ++column)
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
