using Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var matrix = new SquareMatrix(
				new double[,]
				{
					{23, -2, -11, 6 },
					{0, 28, 17, 10 },
					{9, 1, 14, 3 },
					{-3, -2, 0, -16 }
				});
			var elements = new Vector (
				new double[] 
				{ 106, 279, 111, -99 });
			var ge = new DirectIteration(4,-5);
			var result = ge.FindSolution(matrix, elements);
			for (var row = 0; row < matrix.Count; row++)
			{
				double temp=0;
				for (var column = 0; column < matrix.Count; column++)
				{
					temp += matrix[row, column] * result[column];
				}
				Console.WriteLine(temp);
			}
			Console.ReadKey();
		}
	}
}
