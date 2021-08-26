using System;
using System.Collections.Generic;
using shefing_c.Entities;

namespace shefing_c.Calculators
{

	/// <summary>
	/// A class with a static 'process' method that actually does the calculation (and handles exceptions)
	/// @author Zvi Lifshitz
	/// </summary>

	public interface ICalculate {
		public string process(CalcModel entity);
	}

	public class Calculate : ICalculate
	{
		// Initiaize a static calculator mapping
		private static readonly Dictionary<string, Calculator> calcMap = new HashMapAnonymousInnerClassHelper();

		private class HashMapAnonymousInnerClassHelper : Dictionary<string, Calculator>
		{
			public HashMapAnonymousInnerClassHelper()
			{
				Add("plus", new PlusCalculator());
				Add("minus", new MinusCalculator());
				Add("multiply", new MultiplyCalculator());
				Add("divide", new DivideCalculator());
			}
		}

		private const string NO_OPERATOR = "Operator is missing";
		private const string UNRECOGNIZED = "Unrecognized operator '{0}'";
		private const string INT_RESULT = "{0}{1}{2}={3:d}";
		private const string REAL_RESULT = "{0}{1}{2}={3}";

		/// <summary>
		/// The method that does the work </summary>
		/// <param name="oper">  can be "plus", "minus", "multiply", "divide" </param>
		/// <param name="left">      left operand (as a string) </param>
		/// <param name="right">     right operand (as a string) </param>
		/// <returns> the result string </returns>
		public string process(CalcModel entity)
		{
			int left = entity.Left;
			string oper = entity.Operator;
			int right = entity.Right;
			
			// Get the appropriate calculator. If not found return an error string
			if (oper == null)
			{
				return NO_OPERATOR;
			}

			Calculator calculator;
			try {
				calculator = calcMap[oper.ToLower()];
			} catch(KeyNotFoundException){
				return string.Format(UNRECOGNIZED, oper);
			}

			// Calculate and format the result, or return an error string in case of an error.
			// Format the result nicely (division can yield a non-integer value).
			try
			{
				double dResult = calculator.calculate(left, right);
				string op = calculator.Operator;
				return dResult == (long)dResult ?
					string.Format(INT_RESULT, left, op, right, (long)dResult) :
					string.Format(REAL_RESULT, left, op, right, dResult);
			}
			catch (ArithmeticException ex)
			{
				return ex.Message;
			}
		}
	}

}