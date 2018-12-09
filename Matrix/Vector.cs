using System;
using System.Collections.Generic;
using System.Text;

namespace Matrix
{
	public class Vector
	{
		public int Count { get; protected set; }
		public double[] VectorElements { get; set; }

		#region Constructors
		public Vector(int n)
		{
			Count = n;
			VectorElements = new double[Count];
		}

		public Vector(int n, int min, int max)
		{
			Count = n;
			VectorElements = new double[Count];
		}

		public Vector(Vector elements)
		{
			Count = elements.Count;
			VectorElements = new double[Count];
			Array.Copy(elements.VectorElements, VectorElements, Count);
		}

		public Vector(double[] elements)
		{
			Count = elements.Length;
			VectorElements = new double[Count];
			Array.Copy(elements, VectorElements, elements.Length);
		}
		#endregion Constructors



		#region AdditionalMethods
		public double this[int number]
		{
			get { return VectorElements[number]; }
			set { VectorElements[number] = value; }
		}
		#endregion AdditionalMethods


		#region OverloadOperations
		public static Vector operator *(Vector target, double multiplexor)
		{
			var resultVectorElements = new double[target.Count];
			for (int count = 0; count < target.Count; count++)
			{
				resultVectorElements[count] = target[count] * multiplexor;
			}
			return new Vector(resultVectorElements);
		}

		public static Vector operator *(Vector vector, SquareMatrix matrix)
		{
			if (vector.Count != matrix.Count)
				throw new ArgumentException("Vector count and matrix count are not equals!");
			var resultVectorElements = new double[vector.Count];
			for (int row = 0; row < vector.Count; row++)
			{
				var temp = 0d;
				for (int column = 0; column < matrix.Count; column++)
				{
					temp += vector[row] * matrix[column, row];
				}
				resultVectorElements[row] = temp;
			}

			return new Vector(resultVectorElements);
		}

		public static Vector operator +(Vector target, Vector vector)
		{
			var resultVectorElements = new double[target.Count];
			for (int element = 0; element < target.Count; element++)
			{
				resultVectorElements[element] = target[element] + vector[element];
			}
			return new Vector(resultVectorElements);
		}

		public static Vector operator -(Vector target, Vector vector)
		{
			var resultVectorElements = new double[target.Count];
			for (int element = 0; element < target.Count; element++)
			{
				resultVectorElements[element] = target[element] - vector[element];
			}
			return new Vector(resultVectorElements);
		}

		public static Vector operator /(Vector target, double diviser)
		{
			var resultVectorElements = new double[target.Count];
			for (int element = 0; element < target.Count; element++)
			{
				resultVectorElements[element] = target[element] / diviser;
			}
			return new Vector(resultVectorElements);
		}

		#endregion OverloadOperations

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			foreach (var item in VectorElements)
			{
				sb.Append(item);
				sb.Append(' ');
			}
			return sb.ToString();
		}

	}
}
