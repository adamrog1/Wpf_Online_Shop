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
        public string Street { get; set; }
        public string Country { get; set; }


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

        public string City { get; set; }
        public string Postcode { get; set; }


        public string FirstName { get; set; } = (CurrentState.LoggedUser is null)?null:CurrentState.LoggedUser.FirstName;
        public string LastName { get; set; } = (CurrentState.LoggedUser is null) ? null : CurrentState.LoggedUser.LastName;
        public string Email { get; set; } = (CurrentState.LoggedUser is null) ? null : CurrentState.LoggedUser.Email;
        public string PhoneNumber { get; set; } = (CurrentState.LoggedUser is null) ? null : CurrentState.LoggedUser.Phone;

        private string orderCostText = CartContent.GetCartItemsCostText;

        public string OrderCostText
        {
            get { return CartContent.GetCartItemsCostText; }
        }

        public List<String> PaymentMethods { get; set; } = PaymentOptions.Get().ToList();


        private bool SetNewOrder()
        {
            OrderModel neworder = new OrderModel();
            neworder.ListofProducts = CartContent.CartItemsList;
            neworder.UserId = CurrentState.LoggedUser.Id;
            neworder.Street = Street;
            neworder.House = HouseNumber;
            neworder.Apartment = ApartmentNumber;
            neworder.Postcode = Postcode;
            neworder.City = City;
            neworder.Country = Country;
            neworder.FirstName = FirstName;
            neworder.LastName = LastName;
            MessageBox.Show(neworder.House.ToString());
            MessageBox.Show(neworder.Apartment.ToString());
            if (Model.DatabaseConnection.SqliteInsert.InsertOrderRecord(neworder))
                return true;
            else return false;
        }

        private ICommand sendOrderCommand;

        public ICommand SendOrderCommand
        {
            get
            {
                return sendOrderCommand ?? (sendOrderCommand = new RelayCommand(
                    (p) => {
                        if (SetNewOrder())
                        {
                            MessageBox.Show("dodano.");
                        }
                        else
                        {
                            MessageBox.Show("błąd");
                        }
                        ApartmentNumber = null;
                    }, p => true));
            }
        }

        public OrderExecuteViewModel()
        {

        }
    }
}
