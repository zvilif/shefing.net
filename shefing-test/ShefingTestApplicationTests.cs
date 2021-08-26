using System;
using Xunit;
using System.Text.Json;
using System.Text.Json.Serialization;
using shefing_c.Controllers;
using shefing_c.Entities;

namespace shefing_test
{
	public class ShefingTestApplicationTests
	{
		CalcController _controller;
		
		public ShefingTestApplicationTests()
		{
			_controller = new CalcController();
		}
	
		[Fact]
		public void testPlus()
		{
			CalcModel calc = JsonSerializer.Deserialize<CalcModel>("{  \n" + "    \"Operator\":\"plus\",  \n" + "    \"Left\": 5,  \n" + "    \"Right\": 7  \n" + "}");
			string createdResponse = _controller.Post(calc);
			Assert.Contains("5+7=12", createdResponse);
		}

		[Fact]
		public void testMinus()
		{
			CalcModel calc = JsonSerializer.Deserialize<CalcModel>("{  \n" + "    \"Operator\":\"minus\",  \n" + "    \"Left\": 5,  \n" + "    \"Right\": 7  \n" + "}");
			string createdResponse = _controller.Post(calc);
			Assert.Contains("5-7=-2", createdResponse);
		}

		[Fact]
		public void testMultiply()
		{
			CalcModel calc = JsonSerializer.Deserialize<CalcModel>("{  \n" + "    \"Operator\":\"multiply\",  \n" + "    \"Left\": 5,  \n" + "    \"Right\": 7  \n" + "}");
			string createdResponse = _controller.Post(calc);
			Assert.Contains("5*7=35", createdResponse);
		}

		[Fact]
		public void testDivide()
		{
			CalcModel calc = JsonSerializer.Deserialize<CalcModel>("{  \n" + "    \"Operator\":\"divide\",  \n" + "    \"Left\": 5,  \n" + "    \"Right\": 7  \n" + "}");
			string createdResponse = _controller.Post(calc);
			Assert.Contains("5/7=0.7142857142857143", createdResponse);
		}

		[Fact]
		public void testDivideByZero()
		{
			CalcModel calc = JsonSerializer.Deserialize<CalcModel>("{  \n" + "    \"Operator\":\"divide\",  \n" + "    \"Left\": 5,  \n" + "    \"Right\": 0  \n" + "}");
			string createdResponse = _controller.Post(calc);
			Assert.Contains("Divide by zero", createdResponse);
		}

		[Fact]
		public void testWrongOp()
		{
			CalcModel calc = JsonSerializer.Deserialize<CalcModel>("{  \n" + "    \"Operator\":\"power\",  \n" + "    \"Left\": 5,  \n" + "    \"Right\": 0  \n" + "}");
			string createdResponse = _controller.Post(calc);
			Assert.Contains("Unrecognized operator 'power'", createdResponse);
		}

		[Fact]
		public void testMissingOp()
		{
			CalcModel calc = JsonSerializer.Deserialize<CalcModel>("{  \n" + "    \"Operators\":\"power\",  \n" + "    \"Left\": 5,  \n" + "    \"Right\": 0  \n" + "}");
			string createdResponse = _controller.Post(calc);
			Assert.Contains("Operator is missing", createdResponse);
		}

	}

}