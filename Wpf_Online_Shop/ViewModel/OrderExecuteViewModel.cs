using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Wpf_Online_Shop.ViewModel
{
    using BaseClass;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Model;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Obiekt obsługujący widok składania zamówienia
    /// </summary>
    public class OrderExecuteViewModel : ViewModel
    {
        #region definiowanie pól formularza
        private string street;

        public string Street
        {
            get { return (street is null) ? null : street.Trim(); }
            set { street = value; onPropertyChange(nameof(Street)); }
        }

        private string country;

        public string Country
        {
            get { return (country is null) ? null : country.Trim(); }
            set { country = value; onPropertyChange(nameof(Country)); }
        }

        private int houseNumber;

        public int HouseNumber
        {
            get { return Convert.ToInt32(houseNumber); }
            set { houseNumber = Convert.ToInt32(value); onPropertyChange(nameof(houseNumber)); }
        }

        private int? apartmentNumber;

        public int? ApartmentNumber
        {
            get { return Convert.ToInt32(apartmentNumber); }
            set { apartmentNumber = Convert.ToInt32(value); onPropertyChange(nameof(apartmentNumber)); }
        }

        private string city;

        public string City
        {
            get { return (city is null) ? null : city.Trim(); }
            set { city = value; onPropertyChange(nameof(City)); }
        }

        private string postcode;

        public string Postcode
        {
            get { return (postcode is null) ? null : postcode.Trim(); }
            set { postcode = value; onPropertyChange(nameof(Postcode)); }
        }

        private string firstName = (CurrentState.LoggedUser is null) ? null : CurrentState.LoggedUser.FirstName;

        public string FirstName
        {
            get { return (firstName is null) ? null : firstName.Trim(); }
            set { firstName  = value; }
        }

        private string lastName = (CurrentState.LoggedUser is null) ? null : CurrentState.LoggedUser.LastName;

        public string LastName
        {
            get { return (lastName is null)?null:lastName.Trim(); }
            set { lastName = value; }
        }

        public string Email { get; set; } = (CurrentState.LoggedUser is null) ? null : CurrentState.LoggedUser.Email;
        public string PhoneNumber { get; set; } = (CurrentState.LoggedUser is null) ? null : CurrentState.LoggedUser.Phone;

        #endregion

        public string SelectedCountry { get; set; }

        public string[] CountryList
        {
            get { return Countries.Get(); }
        }

        /// <summary>
        /// Tekst pokazujący cenę za zamówienie
        /// </summary>
        public string OrderCostText
        {
            get { return CartContent.GetCartItemsCostText; }
        }

        /// <summary>
        /// Lista przechowująca metody płatności
        /// </summary>
        public List<String> PaymentMethods { get; set; } = PaymentOptions.Get().ToList();

        /// <summary>
        /// Metoda przygotowująca i sprawdzająca obiekt zamówienia
        /// </summary>
        /// <returns></returns>
        private OrderModel SetNewOrder()
        {
            try
            {
                OrderModel neworder = new OrderModel();
                neworder.ListofProducts = CartContent.CartItemsList;
                neworder.UserId = CurrentState.LoggedUser.Id;
                if (Street is null || Street.Length<=0 || Postcode is null || Postcode.Length<=0 || City is null || City.Length<=0 || SelectedCountry is null || SelectedCountry.Length<=0)
                {
                    throw new Exception("Nie wszystkie pola obowiązkowe w danych adresowych są wypełnione.");
                }
                if (houseNumber <= 0)
                {
                    throw new Exception("Numer budynku jest niepoprawny.");
                }
                if (FirstName is null || FirstName.Length <= 0 || LastName is null || LastName.Length <= 0)
                {
                    throw new Exception("Nie wszystkie pola obowiązkowe w danych osobowych są wypełnione.");
                }
                neworder.Street = Regex.Replace(Street, @"\s+", " "); ;
                neworder.House = HouseNumber;
                if (ApartmentNumber is null || ApartmentNumber <= 0) neworder.Apartment = null;
                else neworder.Apartment = ApartmentNumber;
                neworder.Postcode = Regex.Replace(Postcode, @"\s+", " ");
                neworder.City = Regex.Replace(City, @"\s+", " ");
                neworder.Country = SelectedCountry;
                neworder.FirstName = Regex.Replace(FirstName, @"\s+", " ");
                neworder.LastName = Regex.Replace(LastName, @"\s+", " ");
                neworder.Cost = CartContent.GetCartItemsCost;
                int validation_result = OrderValidation.CheckOrder(neworder);
                if (validation_result == 1) throw new Exception("Niektóre pola są puste.");
                if (validation_result == 2) throw new Exception("Niepoprawne imię lub/i nazwisko.");
                if (validation_result == 3) throw new Exception("Imię lub/i nazwisko jest za długie lub za krótkie.");
                if (validation_result == 4) throw new Exception("Niepoprawna nazwa ulicy.");
                if (validation_result == 5) throw new Exception("Niepoprawny kod pocztowy.");
                if (validation_result == 6) throw new Exception("Za duży nr domu lub/i mieszkania.");
                if (validation_result == 7) throw new Exception("Nazwa miasta powinna zawierać jedynie litery.");
                return neworder;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Metoda czyszcząca formularz
        /// </summary>
        private void ClearForm()
        {
            Street = null;
            City = null;
            HouseNumber = 0;
            ApartmentNumber = null;
            Postcode = null;
            Country = null;
        }
        /// <summary>
        /// Zdarzenie wywoływane przy poprawnie zakończonym zamawianiu
        /// </summary>
        public event EventHandler<EventArgs> OrderDoneEvent;

        private ICommand sendOrderCommand;

        /// <summary>
        /// Komenda obsługująca wysyłanie zamówienia do bazy
        /// </summary>
        public ICommand SendOrderCommand
        {
            get
            {
                return sendOrderCommand ?? (sendOrderCommand = new RelayCommand(
                    (p) => {
                        try
                        {
                            if (CartContent.GetCartItemsCost > CurrentState.LoggedUser.Cash)
                            {
                                MessageBox.Show("Masz za mało środków na koncie by zrealizować zamówienie.");
                                return;
                            }
                            OrderModel neworder = SetNewOrder();
                            if (neworder != null)
                            {
                                try
                                {
                                    neworder.Id = Model.DatabaseConnection.SqliteSelect.GetOrderId();
                                    if (neworder.Id is null) neworder.Id = 1;
                                    else neworder.Id = neworder.Id + 1;
                                
                                    if (Model.DatabaseConnection.SqliteTrans.OrderInsertTransaction(neworder))
                                    {
                                        MessageBox.Show("Zamówienie zostało przyjęte do realizacji.");
                                        CurrentState.LoggedUser.Cash = Model.DatabaseConnection.SqliteSelect.GetUserCash(CurrentState.LoggedUser.Id);
                                        ClearForm();
                                        OrderDoneEvent?.Invoke(this, EventArgs.Empty);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Błąd. Zamówienie niezrealizowane.");
                                    }
                                }
                                catch(Exception e)
                                {
                                    MessageBox.Show(e.Message);
                                }
                                
                            }
                            else
                            {
                                MessageBox.Show("błąd przy walidacji.");
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    }, p => true));
            }
        }

        public OrderExecuteViewModel()
        {

        }
    }
}