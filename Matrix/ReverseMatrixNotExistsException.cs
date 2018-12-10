using System;
using System.Collections.Generic;
using System.Text;

namespace Matrix
{
	internal class ReverseMatrixNotExistsException:ApplicationException
	{
		public ReverseMatrixNotExistsException():base("Matrix has no reverse!")
		{
				
		}
	}
}
