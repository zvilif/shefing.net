namespace shefing_c.Calculators
{

	/// <summary>
	/// An interface for specific arithmetic calculators
	/// @author Zvi Lifshitz
	/// </summary>
	public interface Calculator
	{

		/// <summary>
		/// Implement this method to provide calculators for the four arithmetic operations </summary>
		/// <param name="left">      the left operand </param>
		/// <param name="right">     the right operand </param>
		/// <returns> returns the result of the operation </returns>
		/// <exception cref="ArithmeticException"> if the implementation encounters an arithmetic exception, such as divide by zero </exception>

		double calculate(int left, int right);

		/// <summary>
		/// Implement this method to return the operator for this calculator </summary>
		/// <returns> '+', '-', '*' or '/' </returns>
		string Operator {get;}
	}

}