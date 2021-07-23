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

        string changedname;
        public string ChangedName {
            get { return changedname; }
            set
            {
                changedname = value;
                onPropertyChange(nameof(ChangedName));
            }
        }
        string changedlastname;
        public string ChangedLastname {
            get { return changedlastname; }
            set
            {
                changedlastname = value;
                onPropertyChange(nameof(ChangedLastname));
            }
        }
        string changedemail;
        public string ChangedEmail {
            get { return changedemail; }
            set
            {
                changedemail = value;
                onPropertyChange(nameof(ChangedEmail));
            }
        }

        string changedphone;
        public string ChangedPhone
        {
            get { return changedphone; }
            set
            {
                changedphone = value;
                onPropertyChange(nameof(ChangedPhone));
            }
        }

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

        public string AccountMoney
        {
            get
            {
                if (CurrentState.LoggedUser == null) { return "0 zł"; }
                else { return CurrentState.LoggedUser.GetCashText; }
            }

        }

        private string phone;
        public string Phone
        {
            get
            {
                if (CurrentState.LoggedUser == null) return null;
                else { return CurrentState.LoggedUser.Phone; }
            }
            set
            {
                onPropertyChange(nameof(Phone));
                phone = value;
            }
        }


        private List<OrderModel> userorders = new List<OrderModel>();
        public List<OrderModel> UserOrders
        {
            get
            {
                if (CurrentState.LoggedUser == null) return null;
                else userorders= Model.DatabaseConnection.SqliteSelect.GetUserOrders(CurrentState.LoggedUser);
                return userorders;
            }
            set
            {
                userorders = value;
                onPropertyChange(nameof(UserOrders));
            }
        }

        public void getOrderlist()
        {
            UserOrders= Model.DatabaseConnection.SqliteSelect.GetUserOrders(CurrentState.LoggedUser);
        }


        private List<int> cashoptions=new List<int>();
        public List<int> Add
        {
            get {    
                return cashoptions;
            }
        }

        /// <summary>
        /// Wybrana wartość pieniędzy
        /// </summary>
        private int selectedammount;
        public int SelectedAmmount
        {
            get { return selectedammount; }
            set { selectedammount = value; }
        }

        /// <summary>
        /// Sprawdzenie, czy zaszły zmiany w edycji profilu
        /// </summary>
        /// <returns></returns>
        private bool checkifchanged()
        {
            if (CurrentState.LoggedUser != null)
            {
                if ((ChangedName==null||ChangedName=="")
                    && (ChangedLastname==null || ChangedLastname=="")
                    && (ChangedEmail==null || ChangedEmail=="")
                    && (ChangedPhone==null || ChangedPhone=="")) return false;

                else return true;
            }
            return false;
        }
        /// <summary>
        /// komenda do potwierdzania zmian w profilu
        /// </summary>
        private ICommand confirmchanges;
        public ICommand ConfirmChanges {
            get
            {
                return confirmchanges ?? (confirmchanges = new RelayCommand(
                    (p) => {

                    bool status = false;
                        try
                        {
                            if (ChangedName != null)
                            {
                                status = UpdateVerification.verify_name(this.ChangedName);
                                if (status)
                                {

                                    if (Model.DatabaseConnection.SqlliteUpdateProfile.UpdateFirstName(CurrentState.LoggedUser, this.ChangedName))
                                    {
                                        CurrentState.LoggedUser.FirstName = this.ChangedName;
                                        Name = this.ChangedName;
                                        
                                        status = false;
                                    }
                                }
                                else { MessageBox.Show("Dane imienia muszą spełniać warunki jak przy rejestracji"); }
                            }
                            if (ChangedLastname != null)
                            {
                                status = UpdateVerification.verify_lastname(this.ChangedLastname);
                                if (status)
                                {
                                    if (Model.DatabaseConnection.SqlliteUpdateProfile.UpdateLastName(CurrentState.LoggedUser, this.ChangedLastname))
                                    {
                                        CurrentState.LoggedUser.LastName = this.ChangedLastname;
                                        Lastname = this.ChangedLastname;
                                        status = false;
                                    }
                                }
                                else { MessageBox.Show("Dane nazwiska muszą spełniać warunki jak przy rejestracji"); }
                            }
                            if (ChangedEmail != null)
                            {
                                status = UpdateVerification.verify_email(ChangedEmail);
                                if (status)
                                {
                                    if (Model.DatabaseConnection.SqlliteUpdateProfile.UpdateEmail(CurrentState.LoggedUser, this.ChangedEmail))
                                    {
                                        CurrentState.LoggedUser.Email = this.ChangedEmail;
                                        Email = this.ChangedEmail;
                                        status = false;
                                    }
                                }
                                else { MessageBox.Show("Dane emaila muszą spełniać warunki jak przy rejestracji"); }
                            }
                            if(Phone != null)
                            {
                                status = UpdateVerification.verify_phone(ChangedPhone);
                            if (status)
                                {
                                    if(Model.DatabaseConnection.SqlliteUpdateProfile.UpdatePhone(CurrentState.LoggedUser, this.ChangedPhone))
                                    {
                                        CurrentState.LoggedUser.Phone = this.ChangedPhone;
                                        Phone = this.ChangedPhone;
                                        status = false;
                                    }
                                }
                            else { MessageBox.Show("Telefon musi zawierać tylko cyfry i mieć dokładnie 9 znaków"); }
                            }
                        

                        }catch(Exception e)
                        {
                            if (e.Message.Substring(e.Message.Length - 5).Equals("Email"))
                            {
                                MessageBox.Show("Istnieje użytkownik o podanym adresie e-mail.");
                            }
                            else MessageBox.Show("Błąd przy edycji " + e.Message);
                        }
                    }, p => checkifchanged()));

               
            }
        }
        /// <summary>
        /// Sprawdzenie, czy użytkownik jest zalogowany
        /// </summary>
        /// <returns></returns>
        private bool checkiflogged()
        {
            if (CurrentState.LoggedUser == null) return false;
            else return true;
        }
        
        /// <summary>
        /// Sprawdzenie czy wybrano zamówienie w profilu
        /// </summary>
        /// <returns></returns>
        private bool checkifloggedandselected()
        {
            if (CurrentState.LoggedUser == null || SelectedOrder == null) return false;
            else return true;
        }

        /// <summary>
        /// komenda do dodawania pieniądzy na konto
        /// </summary>
        private ICommand addcash;
        public ICommand AddCash
        {
            get
            {
                return addcash ?? (addcash = new RelayCommand(
                    (p) => {
                       
                        try
                        {
                            if (Model.DatabaseConnection.SqliteAddCash.AddCash(CurrentState.LoggedUser, selectedammount*100))
                            {
                                CurrentState.LoggedUser.Cash += selectedammount*100;
                                //AccountMoney = CurrentState.LoggedUser.GetCashText;
                                onPropertyChange(nameof(AccountMoney));
                                MessageBox.Show("Doładowano ");
                            }
                        }catch(Exception e)
                        {
                            MessageBox.Show("Błąd przy dodawaniu środków" + e);
                        }
                        

                    }, p => checkiflogged() ));
            }
        }

        public OrderModel SelectedOrder { get; set; }
        public int? SelectedOrderId { get { return SelectedOrder.Id; } }

        /// <summary>
        /// event wywoływany przy sprawdzeniu szczegółów wybranego zamówienia
        /// </summary>
        public EventHandler<EventArgs> checktheproducts;

        /// <summary>
        /// komenda wywoływana przy sprawdzaniu szczegółów zamówienia
        /// </summary>
        public ICommand check;
        public ICommand Check
        {
            get
            {
                return check ?? (check = new RelayCommand(
                    (p) =>
                    {
                        if (UserOrders.Count <= 0)
                        {
                            MessageBox.Show("Brak zamówień.");
                            return;
                        }
                        if (SelectedOrder is null)
                        {
                            MessageBox.Show("Należy najpierw wybrać zamówienie z listy.");
                            return;
                        }
                        checktheproducts?.Invoke(this, EventArgs.Empty);
                    }
                    , p => checkifloggedandselected())
                    );
            }
        }

        /// <summary>
        /// event wywoływany podczas wylogowywania
        /// </summary>
        public event EventHandler<EventArgs> LogoutEvent;

        /// <summary>
        /// Komenda wylogowania
        /// </summary>
        private ICommand logoutCommand;
        public ICommand LogoutCommand
        {
            get
            {
                return logoutCommand ?? (logoutCommand = new RelayCommand(
                    (p) => {
                        LogoutEvent?.Invoke(this, EventArgs.Empty);

                    }, p => checkiflogged()));
            }
        }


        public ProfileViewModel()
        {
            foreach (int item in Enum.GetValues(typeof(AddingCashOptions)))
            {
                cashoptions.Add(item);
            }
        }
    }
}