using Microsoft.Extensions.DependencyInjection;

namespace Calculator.ViewModel
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainViewModel => 
            App.ServiceProvider.GetRequiredService<MainWindowViewModel>();
    }
}
