using System;
using System.Windows.Input;

namespace Online_Shop_Project.Commands
{
    public abstract class CommandBase:ICommand
    {
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) => true;


        public abstract void Execute(object parameter);
   

        protected void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this,new EventArgs());
        }
    
    }
}