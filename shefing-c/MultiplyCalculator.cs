namespace shefing_c.Calculators
{

	/// <summary>
	/// Calculator implementation of multiplication
	/// @author Zvi Lifshitz
	/// </summary>
	public class MultiplyCalculator : Calculator
	{

		public virtual double calculate(int left, int right)
		{
			return left * right;
		}

		public virtual string Operator
		{
			get
			{
				return "*";
			}
		}
	}

}