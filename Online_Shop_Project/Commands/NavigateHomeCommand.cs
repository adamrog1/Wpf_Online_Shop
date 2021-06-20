using Online_Shop_Project.Stores;
using Online_Shop_Project.ViewModel;

namespace Online_Shop_Project.Commands
{
    public class NavigateHomeCommand:CommandBase
    {
        private readonly NavigationStore _navigationStore;

        public NavigateHomeCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.SelectedViewModel =new HomeViewModel();
        }
    }
}