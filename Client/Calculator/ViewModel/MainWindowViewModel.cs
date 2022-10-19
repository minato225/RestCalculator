using Calculator.Model;
using Calculator.Service;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Threading.Tasks;

namespace Calculator.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ICalculatorService _calculatorService;

        private string _firstArgText = string.Empty;
        private string _secondArgText = string.Empty;
        private string _resultText = string.Empty;

        private string _evalErrorText = string.Empty;
        private bool _isEvalError;

        public string FirstArgText { get => _firstArgText; set => Set(ref _firstArgText, value); }
        public string SecondArgText { get => _secondArgText; set => Set(ref _secondArgText, value); }
        public string ResultText { get => _resultText; set => Set(ref _resultText, value); }
        public string EvalErrorText { get => _evalErrorText; set => Set(ref _evalErrorText, value); }
        public bool IsEvalError { get => _isEvalError; set => Set(ref _isEvalError, value); }

        public RelayCommand AddCommand { get; }
        public RelayCommand DivCommand { get; }
        public RelayCommand MulCommand { get; }
        public RelayCommand SubCommand { get; }

        public MainWindowViewModel(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
            AddCommand = new RelayCommand(async () => await EvalAsync(Operations.Add));
            DivCommand = new RelayCommand(async () => await EvalAsync(Operations.Div));
            MulCommand = new RelayCommand(async () => await EvalAsync(Operations.Mul));
            SubCommand = new RelayCommand(async () => await EvalAsync(Operations.Sub));
        }

        private async Task EvalAsync(Operations operations)
        {
            try
            {
                var a = int.Parse(FirstArgText);
                var b = int.Parse(SecondArgText);

                ResultText = (operations switch
                {
                    Operations.Add => await _calculatorService.Add(a, b),
                    Operations.Div => await _calculatorService.Divide(a, b),
                    Operations.Mul => await _calculatorService.Multiply(a, b),
                    Operations.Sub => await _calculatorService.Subtract(a, b),
                    _ => throw new ArgumentException(),
                }).Result.ToString();
                IsEvalError = false;
                EvalErrorText = string.Empty;
            }
            catch (ArgumentNullException e)
            {
                IsEvalError = true;
                EvalErrorText = e.Message;
            }
            catch (ArgumentException ex)
            {
                IsEvalError = true;
                EvalErrorText = ex.Message;
            }
            catch(Exception exp)
            {
                IsEvalError = true;
                EvalErrorText = exp.Message;
            }
        }
    }
}
