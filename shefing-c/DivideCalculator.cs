using System;

namespace shefing_c.Calculators
{

	/// <summary>
	/// Calculator implementation of multiplication
	/// @author Zvi Lifshitz
	/// </summary>
	public class DivideCalculator : Calculator
	{
		private const string DIVIDE_BY_ZERO = "Divide by zero";

		public virtual double calculate(int left, int right)
		{
			if (right == 0)
			{
				throw new ArithmeticException(DIVIDE_BY_ZERO);
			}
			return left / (double)right;
		}

		public virtual string Operator
		{
			get
			{
				return "/";
			}
		}
	}

}