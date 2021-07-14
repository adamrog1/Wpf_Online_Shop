using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.ViewModel
{
    using BaseClass;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Wpf_Online_Shop.Model;

    public class ProfileViewModel : ViewModel
    {
        public string ChangedName { get;  set; }
        public string ChangedLastname { get;  set; }
        public string ChangedEmail { get;  set; }
        public string ChangedLogin { get;  set; }
        public string ChangedPassword { get; set; }

        private string loggeduserstring;
        public string LoggedUserString
        {
            get {
                if (CurrentState.LoggedUser == null)
                {
                    return loggeduserstring = "Nie jesteś zalogowany, przejdź do logowania, aby sprawdzić informacje o twoim profilu";
                }
                else
                {
                    return loggeduserstring = $"Zalogowany użytwkonik to: {CurrentState.LoggedUser.Login}";
                }
            }
            set {
                loggeduserstring = value;
            }
        }
        private string name;
        public string Name
        {
            get {
                if (CurrentState.LoggedUser == null) { return null; }

                else { return CurrentState.LoggedUser.FirstName; }
            }
            set {
                
                name = value;
                onPropertyChange(nameof(Name));
            }
        }
        private string lastname;
        public string Lastname
        {
            get
            {
                if (CurrentState.LoggedUser == null) { return null; }
                else { return CurrentState.LoggedUser.LastName; }
            }
            set
            {
                onPropertyChange(nameof(Lastname));
                lastname = value;
            }
        }

        private string email;
        public string Email
        {
            get
            {
                if (CurrentState.LoggedUser == null) { return null; }
                else { return CurrentState.LoggedUser.Email; }
            }
            set
            {
                onPropertyChange(nameof(Email));
                email = value;
            }
        }

        private string login;
        public string Login
        {
            get
            {
                if (CurrentState.LoggedUser == null) { return null; }
                else { return CurrentState.LoggedUser.Login; }
            }
            set
            {
                onPropertyChange(nameof(Login));
                login = value;
            }
        }

        private string password;
        public string Password
        {
            get
            {
                if (CurrentState.LoggedUser == null) { return null; }
                else { return CurrentState.LoggedUser.Password; }
            }
            set
            {
                onPropertyChange(nameof(Password));
                login = value;
            }
        }


        private List<OrderModel> userorders = new List<OrderModel>();
        public List<OrderModel> UserOrders
        {
            get
            {
                //tu bedzie lista zamówień danego użytkownika
                return userorders;
            }
            set
            {
                userorders = value;
            }
        }

        private bool checkifchanged()
        {
            if (CurrentState.LoggedUser != null)
            {
                if (ChangedName==null && ChangedLastname==null && ChangedEmail==null && ChangedLogin==null && ChangedPassword==null) return false;

                else return true;
            }
            return false;
        }

        private ICommand confirmchanges;
        public ICommand ConfirmChanges {
            get
            {
                return confirmchanges ?? (confirmchanges = new RelayCommand(
                    (p) => {

                            var passbox = p as PasswordBox;
                            var password = passbox.Password;
                            bool status = false;
                            if (ChangedName != null) status=UpdateVerification.verify_name(this.ChangedName);
                            if (ChangedLastname != null) status = UpdateVerification.verify_lastname(this.ChangedLastname);
                            if (ChangedEmail != null) status = UpdateVerification.verify_email(ChangedEmail);
                            if (ChangedLogin != null) status = UpdateVerification.verify_login(ChangedLogin);
                            if (ChangedPassword != null) status = UpdateVerification.verify_password(ChangedPassword);

                            if (status == true)
                            {
                                MessageBox.Show("Dokonano edycji danych");
                            }
                            else
                            {
                                MessageBox.Show("Dane muszą spełniać wymagania jak przy rejestracji");
                            }
                    }, p => checkifchanged()));

               
            }
        }


        public ProfileViewModel()
        {

        }
    }
}
