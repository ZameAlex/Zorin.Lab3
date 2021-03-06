﻿using System;
using System.Text;
using static System.Math;

namespace Matrix
{
	public class SquareMatrix
	{
		public int Count { get; protected set; }
		public double[,] MatrixElements { get; set; }
		protected SquareMatrix AdditionalMatrix { get; set; }
		public SquareMatrix ReverseMatrix { get; protected set; }
		public double Determinant { get; protected set; }

		#region Constructors
		public SquareMatrix(int n)
		{
			Count = n;
			MatrixElements = new double[Count, Count];
		}

		public SquareMatrix(int n, int min, int max)
		{
			Count = n;
			MatrixElements = new double[Count, Count];
			RandomInitialization(min, max);
		}

		public SquareMatrix(SquareMatrix matrix)
		{
			this.Count = matrix.Count;
			MatrixElements = new double[Count, Count];
			Array.Copy(matrix.MatrixElements, this.MatrixElements, MatrixElements.Length);
		}

		public SquareMatrix(double[,] elements)
		{
			if (elements.GetLength(0) == elements.GetLength(1))
			{
				Count = elements.GetLength(0);
				MatrixElements = new double[Count, Count];
				Array.Copy(elements, MatrixElements, elements.Length);
			}
			else
				throw new ArgumentException("Matrix is not square!");
		}
		#endregion Constructors


		#region AdditionalMethods

		public double M_Norm()
		{
			var max = 0d;
			for (var row = 0; row < Count; row++)
			{
				var tmp = 0d;
				for (var column = 0; column < Count; column++)
				{
					if (row != column)
						tmp += MatrixElements[row, column] / MatrixElements[row, row];
				}

				if (Abs(tmp) > max)
					max = Abs(tmp);
			}

			return max;
		}

		public void RandomInitialization(int min, int max)
		{
			var randomizer = new Random((int)DateTime.Now.Ticks);
			for (var row = 0; row < Count; row++)
			{
				for (var column = 0; column < Count; column++)
				{
					MatrixElements[row, column] = randomizer.Next(min, max);
				}
			}
		}

		public void Transpone()
		{
			for (var row = 0; row < Count; row++)
				for (var column = row + 1; column < Count; column++)
				{
					var temp = this.MatrixElements[row, column];
					this.MatrixElements[row, column] = this.MatrixElements[column, row];
					this.MatrixElements[column, row] = temp;
				}
		}

		public void FindReverseMatrix(int module)
		{
			FindAdditionalMatrix();
			CountDeterminant();
			if (Determinant == 0)
				throw new ReverseMatrixNotExistsException();
			AdditionalMatrix.Transpone();
			AdditionalMatrix *= FindReverseDeterminant(module);
			ReverseMatrix = AdditionalMatrix;
		}

		public double this[int row, int column] {
			get => MatrixElements[row, column];
			set => MatrixElements[row, column] = value;
		}
		public Vector this[int row]
		{
			get => new Vector(GetRow(row));
			set
			{
				for (var element = 0; element < value.Count; element++)
				{
					MatrixElements[row, element] = value[element];
				}
			}
		}


		#endregion AdditionalMethods


		#region PrivateMethods


		private double[] GetRow(int row)
		{
			var array = new double[Count];
			for (var i = 0; i < Count; ++i)
				array[i] = MatrixElements[row, i];
			return array;
		}

		private double CountDeterminant()
		{
			Determinant = 0;
			if (this.AdditionalMatrix == null)
				for (var column = 0; column < Count; column++)
				{
					Determinant += this[0, column] * this.FindAlgerbricAddition(0, column);
				}
			else
				for (var column = 0; column < Count; column++)
				{
					Determinant += this[0, column] * this.AdditionalMatrix[0, column];
				}
			return Determinant;
		}

		private double FindAlgerbricAddition(int row, int column)
		{
			var minor = new SquareMatrix(Count - 1);
			var power = -1;
			if ((row + column) % 2 == 0)
				power = 1;
			var tempAddForRow = 0;
			for (var _row = 0; _row < Count; _row++)
			{
				if (row == _row)
				{
					tempAddForRow = -1;
					continue;
				}
				var tempAddForColumn = 0;
				for (var _column = 0; _column < Count; _column++)
				{
					if (column == _column)
					{
						tempAddForColumn = -1;
						continue;
					}
					minor.MatrixElements[_row + tempAddForRow, _column + tempAddForColumn] = this[_row, _column];
				}
			}
			if (minor.Count == 1)
				return minor[0, 0] * power;
			return minor.CountDeterminant() * power;
		}

		private void FindAdditionalMatrix()
		{
			var resultMatrixElements = new double[Count, Count];
			for (var row = 0; row < Count; row++)
			{
				for (var column = 0; column < Count; column++)
					resultMatrixElements[row, column] = FindAlgerbricAddition(row, column);
			}
			AdditionalMatrix = new SquareMatrix(resultMatrixElements);
		}

		private double ExtendedEuclideanAlgorithm(double a, double b, out double x, out double y)
		{
			if (a == 0)
			{
				x = 0; y = 1;
				return b;
			}
			var d = ExtendedEuclideanAlgorithm(b % a, a, out var x1, out var y1);
			x = y1 - (b / a) * x1;
			y = x1;
			return d;
		}

		private double FindReverseDeterminant(int module)
		{
			double x, y;
			var g = ExtendedEuclideanAlgorithm(Determinant, module, out x, out y);
			if (g == 1)
				x = (x % module + module) % module;
			else
				throw new ReverseMatrixNotExistsException();
			return x;
		}


		#endregion PrivateMethods

		#region OverloadOperations
		public static SquareMatrix operator +(SquareMatrix first, SquareMatrix second)
		{
			var length = first.Count;
			var resultMatrixElements = new double[length, length];
			for (var row = 0; row < length; row++)
			{
				for (var column = 0; column < length; column++)
					resultMatrixElements[row, column] = first[row, column] + second[row, column];
			}
			return new SquareMatrix(resultMatrixElements);
		}

		public static SquareMatrix operator /(SquareMatrix target, double diviser)
		{
			var resultMatrixElements = new double[target.Count, target.Count];
			for (var row = 0; row < target.Count; row++)
			{
				for (var column = 0; column < target.Count; column++)
					resultMatrixElements[row, column] = target[row, column] / diviser;
			}
			return new SquareMatrix(resultMatrixElements);
		}

		public static SquareMatrix operator *(SquareMatrix target, double multiplexor)
		{
			var resultMatrixElements = new double[target.Count, target.Count];
			for (var row = 0; row < target.Count; row++)
			{
				for (var column = 0; column < target.Count; column++)
					resultMatrixElements[row, column] = target[row, column] * multiplexor;
			}
			return new SquareMatrix(resultMatrixElements);
		}

		public static Vector operator *(SquareMatrix matrix, Vector vector)
		{
			if (vector.Count != matrix.Count)
				throw new ArgumentException("Vector count and matrix count are not equals!");
			var resultVectorElements = new double[vector.Count];
			for (var row = 0; row < vector.Count; row++)
			{
				var temp = 0d;
				for (var column = 0; column < matrix.Count; column++)
				{
					temp += matrix[row, column] * vector[column];
				}
				resultVectorElements[row] = temp;
			}

			return new Vector(resultVectorElements);
		}

		public static SquareMatrix operator *(SquareMatrix first, SquareMatrix second)
		{
			if (first.Count != second.Count)
				throw new ArgumentException("Matrixes count are not equals!");
			var resultMatrixElements = new double[first.Count, second.Count];
			for (var firstrow = 0; firstrow < first.Count; firstrow++)
			{
				var temp = 0d;
				for (var column = 0; column < first.Count; column++)
				{
					for (var secondrow = 0; secondrow < second.Count; secondrow++)
					{
						temp += first[firstrow, secondrow] * second[secondrow, column];
					}
					resultMatrixElements[firstrow, column] = temp;
				}

			}
			return new SquareMatrix(resultMatrixElements);
		}
		#endregion OverloadOperations
		public override string ToString()
		{
			var sb = new StringBuilder();
			for (var row = 0; row < Count; row++)
			{
				for (var column = 0; column < Count; column++)
				{
					sb.Append(MatrixElements[row, column]);
					sb.Append(' ');
				}
				sb.Append('\n');
			}
			return sb.ToString();
		}
	}



}
