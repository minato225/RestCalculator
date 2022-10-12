using System.Threading.Tasks;

namespace RestCalculator.Services
{
    public interface ICalculator
    {
        Task<double> Add(double a, double b);
        Task<double> Subtract(double a, double b);
        Task<double> Divide(double a, double b);
        Task<double> Multiply(double a, double b);
    }
}
