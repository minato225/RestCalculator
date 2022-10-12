namespace RestCalculator.Model
{
    public class CalculatorResult
    {
        public double Result { get; set; }

        public CalculatorResult(double result) => 
            Result = result;
    }
}
