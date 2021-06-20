using System.Windows.Input;
using Online_Shop_Project.Commands;
using Online_Shop_Project.Stores;

namespace Online_Shop_Project.ViewModel
{
    public class LogInViewModel:BaseViewModel
    {
        public ICommand NavigateHomeCommand { get; }

        public LogInViewModel(NavigationStore navigationStore)
        {
            NavigateHomeCommand = new NavigateHomeCommand(navigationStore);
        }
    }
}