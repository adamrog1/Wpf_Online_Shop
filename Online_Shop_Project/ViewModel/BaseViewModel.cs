using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Online_Shop_Project.ViewModel
{
    public class BaseViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}