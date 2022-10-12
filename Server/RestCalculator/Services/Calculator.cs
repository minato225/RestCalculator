using System;
using System.Threading.Tasks;

namespace RestCalculator.Services
{
    public class Calculator : ICalculator
    {
        public async Task<double> Add(double a, double b)
        {
            var result = await Task.FromResult(a + b);
            return result;
        }

        public async Task<double> Divide(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("divisor should not be zero!");

            var result = await Task.FromResult(a / b);
            return result;
        }

        public async Task<double> Multiply(double a, double b)
        {
            var result = await Task.FromResult(a * b);
            return result;
        }

        public async Task<double> Subtract(double a, double b)
        {
            var result = await Task.FromResult(a - b);
            return result;
        }
    }
}
