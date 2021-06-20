using System;
using Online_Shop_Project.ViewModel;

namespace Online_Shop_Project.Stores
{
    public class NavigationStore
    {
        public event Action CurrentViewModelChanged;
        private BaseViewModel _currentviewmodel;

        public BaseViewModel SelectedViewModel
        {
            get => _currentviewmodel;
            set
            {
                _currentviewmodel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}