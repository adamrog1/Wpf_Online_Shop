

using System;
using Online_Shop_Project.Stores;

namespace Online_Shop_Project.ViewModel
{
    public class MainViewModel: BaseViewModel
    {
        private NavigationStore _navigationStore = new NavigationStore();

        public BaseViewModel SelectedViewModel => _navigationStore.SelectedViewModel;

        public MainViewModel()
        {
            _navigationStore.SelectedViewModel = new LogInViewModel(_navigationStore);
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(SelectedViewModel));
        }
    }
}