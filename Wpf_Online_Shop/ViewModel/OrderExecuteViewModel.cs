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

    public class OrderExecuteViewModel : ViewModel
    {
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

        public string OrderCostText
        {
            get { return CartContent.GetCartItemsCostText; }
        }

        public List<String> PaymentMethods { get; set; } = PaymentOptions.Get().ToList();


        private OrderModel SetNewOrder()
        {
            try
            {
                OrderModel neworder = new OrderModel();
                neworder.ListofProducts = CartContent.CartItemsList;
                neworder.UserId = CurrentState.LoggedUser.Id;
                if (Street is null || Street.Length<=0 || Postcode is null || Postcode.Length<=0 || City is null || City.Length<=0 || Country is null || Country.Length<=0)
                {
                    throw new Exception("Nie wszystkie pola obowiązkowe w danych adresowych są wypełnione.");
                }
                if (houseNumber <= 0)
                {
                    throw new Exception("Numer budynku jest niepoprawny.");
                }
                if (FirstName is null || FirstName.Length <= 0)
                {
                    throw new Exception("Nie wszystkie pola obowiązkowe w danych osobowych są wypełnione.");
                }
                neworder.Street = Street;
                neworder.House = HouseNumber;
                if (ApartmentNumber is null || ApartmentNumber <= 0) neworder.Apartment = null;
                else neworder.Apartment = ApartmentNumber;
                neworder.Postcode = Postcode;
                neworder.City = City;
                neworder.Country = Country;
                neworder.FirstName = FirstName;
                neworder.LastName = LastName;
                neworder.Cost = CartContent.GetCartItemsCost;
                //VALIDATION if wrong return null or throw exceptions
                return neworder;
            }
            catch
            {
                throw;
            }
        }

        private void ClearForm()
        {
            Street = null;
            City = null;
            HouseNumber = 0;
            ApartmentNumber = null;
            Postcode = null;
            Country = null;
        }

        public event EventHandler<EventArgs> OrderDoneEvent;

        private ICommand sendOrderCommand;

        public ICommand SendOrderCommand
        {
            get
            {
                return sendOrderCommand ?? (sendOrderCommand = new RelayCommand(
                    (p) => {
                        try
                        {
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
