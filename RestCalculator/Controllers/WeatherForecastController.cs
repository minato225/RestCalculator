using Microsoft.AspNetCore.Mvc;
using RestCalculator.Services;
using System;

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
        public IActionResult Add(double a, double b)
        {
            var result = CalculatorService.Add(a, b);
            return Ok(new
            {
                Result = result
            });
        }

        [HttpGet("Div")]
        public IActionResult Divide(double a, double b)
        {
            double result;

            try
            {
                result = CalculatorService.Divide(a, b);
            }
            catch (DivideByZeroException e)
            {
                return BadRequest(new { message = e.Message });
            }

            return Ok(new
            {
                Result = result
            });
        }

        [HttpGet("Sub")]
        public IActionResult Subtract(double a, double b)
        {
            var result = CalculatorService.Subtract(a, b);
            return Ok(new
            {
                Result = result
            });
        }

        [HttpGet("Mul")]
        public IActionResult Multiply(double a, double b)
        {
            var result = CalculatorService.Multiply(a, b);
            return Ok(new
            {
                Result = result
            });
        }

    }
}
