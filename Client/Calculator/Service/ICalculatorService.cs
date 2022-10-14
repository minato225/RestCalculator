using Calculator.Model;
using System.Threading.Tasks;

namespace Calculator.Service
{
    public interface ICalculatorService
    {
        Task<CalculatorResult> Add(double a, double b);
        Task<CalculatorResult> Subtract(double a, double b);
        Task<CalculatorResult> Divide(double a, double b);
        Task<CalculatorResult> Multiply(double a, double b);
    }
}
