using System;

namespace RestCalculator.Services
{
    public class Calculator : ICalculator
    {
        public double Add(double a, double b)
        {
            return a + b;
        }

        public double Divide(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("divisor should not be zero!");

            return a / b;
        }

        public double Multiply(double a, double b)
        {
            return a * b;
        }

        public double Subtract(double a, double b)
        {
            return a - b;
        }
    }
}
