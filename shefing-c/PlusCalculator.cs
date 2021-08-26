namespace shefing_c.Calculators
{

	/// <summary>
	/// Calculator implementation of addition
	/// @author Zvi Lifshitz
	/// </summary>
	public class PlusCalculator : Calculator
	{

		public virtual double calculate(int left, int right)
		{
			return left + right;
		}

		public virtual string Operator
		{
			get
			{
				return "+";
			}
		}
	}

}