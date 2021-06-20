

using System;

namespace Online_Shop_Project.ViewModel
{
    public class MainViewModel: BaseViewModel
    {


        public BaseViewModel SelectedViewModel
        {
            get;
        }

        public MainViewModel()
        {
            SelectedViewModel = new HomeViewModel();
        }
    }
}