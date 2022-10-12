using Microsoft.AspNetCore.Mvc;
using RestCalculator.Model;
using RestCalculator.Services;
using System;
using System.Threading.Tasks;

namespace RestCalculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        public ICalculator CalculatorService { get; }

        public CalculatorController(ICalculator calculatorService) =>
            CalculatorService = calculatorService;

        [HttpGet("Add")]
        public async Task<IActionResult> Add(double a, double b)
        {
            var result = await CalculatorService.Add(a, b);
            var calcResult = new CalculatorResult { Result = result };
            return Ok(calcResult);
        }

        [HttpGet("Div")]
        public async Task<IActionResult> Divide(double a, double b)
        {
            double result;
            CalculatorResult calculatorResult;

            try
            {
                result = await CalculatorService.Divide(a, b);
                calculatorResult = new() { Result = result };
            }
            catch (DivideByZeroException e)
            {
                calculatorResult = new() { ErrorMessage = e.Message };
            }

            return Ok(calculatorResult);
        }

        [HttpGet("Sub")]
        public async Task<IActionResult> Subtract(double a, double b)
        {
            var result = await CalculatorService.Subtract(a, b);
            var calcResult = new CalculatorResult { Result = result };
            return Ok(calcResult);
        }

        [HttpGet("Mul")]
        public async Task<IActionResult> Multiply(double a, double b)
        {
            var result = await CalculatorService.Multiply(a, b);
            var calcResult = new CalculatorResult { Result = result };
            return Ok(calcResult);
        }
    }
}
