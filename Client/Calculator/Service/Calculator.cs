﻿using Calculator.Model;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Calculator.Service
{
    public class Calculator : ICalculator
    {
        // https://localhost:44317
        private readonly HttpClient _httpClient;

        public Calculator(HttpClient httpClient) =>
            _httpClient = httpClient;

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
                null => throw new ArgumentNullException(nameof(operationName), $"{operationName} return null response")
            };
        }
    }
}
