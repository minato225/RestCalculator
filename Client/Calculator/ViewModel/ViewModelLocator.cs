using Microsoft.Extensions.DependencyInjection;

namespace Calculator.ViewModel
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => 
            App.ServiceProvider.GetRequiredService<MainWindowViewModel>();
    }
}
