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

        private string _FirstArgText = string.Empty;
        private string _SecondArgText = string.Empty;
        private string _ResultText = string.Empty;

        public string FirstArgText { get => _FirstArgText; set => Set(ref _FirstArgText, value); }
        public string SecondArgText { get => _SecondArgText; set => Set(ref _SecondArgText, value); }
        public string ResultText { get => _ResultText; set => Set(ref _ResultText, value); }

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
            var a = int.Parse(FirstArgText);
            var b = int.Parse(SecondArgText);

            var result =  operations switch
            {
                Operations.Add => await _calculatorService.Add(a, b),
                Operations.Div => await _calculatorService.Divide(a, b),
                Operations.Mul => await _calculatorService.Multiply(a, b),
                Operations.Sub => await _calculatorService.Subtract(a, b),
                _ => throw new NotImplementedException(),
            };

            ResultText = result.Result.ToString();
        }
    }
}
