using NUnit.Framework;
using RestCalculator.Services;
using System;
using System.Threading.Tasks;

namespace RestCalculator.Test
{
    [TestFixture]
    public class RestCalculatorTest
    {
        private Calculator calc;

        [SetUp]
        public void Setup() => calc = new Calculator();

        [Test]
        [TestCase(1, 2, ExpectedResult = 3)]
        [TestCase(0, 0, ExpectedResult = 0)]
        [TestCase(100, 0, ExpectedResult = 100)]
        [TestCase(123, 2, ExpectedResult = 125)]
        public double Add_Test(double a, double b)
        {
            var result = calc.Add(a, b);
            return result.Result;
        }

        [Test]
        [TestCase(2, 1, ExpectedResult = 2)]
        [TestCase(10, 2, ExpectedResult = 5)]
        [TestCase(0, 1, ExpectedResult = 0)]
        [TestCase(125, 5, ExpectedResult = 25)]
        public double Div_Test(double a, double b)
        {
            var result = calc.Divide(a, b);
            return result.Result;
        }

        [Test]
        [TestCase(2, 1, ExpectedResult = 2)]
        [TestCase(10, 2, ExpectedResult = 20)]
        [TestCase(0, 1, ExpectedResult = 0)]
        [TestCase(25, 5, ExpectedResult = 125)]
        public double Mul_Test(double a, double b)
        {
            var result = calc.Multiply(a, b);
            return result.Result;
        }

        [Test]
        [TestCase(2, 1, ExpectedResult = 1)]
        [TestCase(10, 2, ExpectedResult = 8)]
        [TestCase(0, 1, ExpectedResult = -1)]
        [TestCase(25, 5, ExpectedResult = 20)]
        public double Sub_Test(double a, double b)
        {
            var result = calc.Subtract(a, b);
            return result.Result;
        }

        [Test]
        public void Divide_Zero_DivideByZeroException() =>
            Assert.ThrowsAsync<DivideByZeroException>(
                async () => await calc.Divide(1, 0),
                message: "divisor should not be zero!");
    }
}
