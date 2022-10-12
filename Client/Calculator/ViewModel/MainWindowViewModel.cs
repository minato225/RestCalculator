using Calculator.Service;

namespace Calculator.ViewModel
{
    public class MainWindowViewModel
    {
        private readonly ICalculator _calculatorService;

        public string AddFirstArgText;

        //AddEvalCommand

        public MainWindowViewModel(ICalculator calculatorService) => 
            _calculatorService = calculatorService;

    }
}
