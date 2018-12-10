using Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
	internal class GaussianElimination : BaseMethod
	{

		public GaussianElimination(int count) : base(count)
		{
		}

		public override Vector FindSolution(SquareMatrix inputMatrix, Vector inputElements)
		{
			//Direct elimination
			var matrix = new SquareMatrix(inputMatrix);
			var elements = new Vector(inputElements);
			for (var row = 0; row < Count-1; row++)
			{
				elements[row] /= matrix[row, row];
				matrix[row] /= matrix[row, row];
				for (var underMain = row+1; underMain < Count; underMain++)
				{
					elements[underMain] -= elements[row] * matrix[underMain, row];
					matrix[underMain] -= matrix[row] * matrix[underMain, row];
				}
			}
			elements[Count-1] /=  matrix[Count-1,Count-1];
			matrix[Count - 1] /= matrix[Count - 1, Count - 1];

			//Backward substitution
			for (var row = Count - 2; row >= 0; row--)
			{
				for (var column = row+1; column < Count; column++)
				{
					elements[row] = elements[row] - elements[column] * matrix[row, column];
				}
			}
			return elements;
		}


	}
}
