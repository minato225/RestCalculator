using Calculator.Model;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Calculator.Service
{
    public class CalculatorService : ICalculatorService
    {
        private readonly HttpClient _httpClient;

        public CalculatorService(HttpClient httpClient, IOptions<AppSettings> options)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.BasePath);
        }

        public async Task<CalculatorResult> Add(double a, double b) => await EvalExpression("Add", a, b);
        public async Task<CalculatorResult> Subtract(double a, double b) => await EvalExpression("Sub", a, b);
        public async Task<CalculatorResult> Multiply(double a, double b) => await EvalExpression("Mul", a, b);
        public async Task<CalculatorResult> Divide(double a, double b) => await EvalExpression("Div", a, b);


        private async Task<CalculatorResult> EvalExpression(string operationName, double a, double b)
        {
            var result = await _httpClient.GetFromJsonAsync<CalculatorResult>($"/Calculator/{operationName}?a={a}&b={b}");

            return result switch
            {
                { ErrorMessage: null } => result,
                { ErrorMessage: not null } => throw new ArgumentException(result.ErrorMessage),
                _ => throw new ArgumentNullException(nameof(operationName), $"{operationName} return null response")
            };
        }
    }
}
