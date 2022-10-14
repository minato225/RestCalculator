using Calculator.Service;
using GalaSoft.MvvmLight;

namespace Calculator.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ICalculatorService _calculatorService;

        public string AddFirstArgText = "e";

        //AddEvalCommand

        public MainWindowViewModel(ICalculatorService calculatorService) => 
            _calculatorService = calculatorService;

    }
}
