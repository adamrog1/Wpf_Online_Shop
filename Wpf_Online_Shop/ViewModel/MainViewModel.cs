  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf_Online_Shop.View;
using Wpf_Online_Shop.Model;

namespace Wpf_Online_Shop.ViewModel
{
    using BaseClass;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Główny obiekt warstwy ViewModel, odpowiadający za nawigację między pozostałymi widokami i komunikację między obiektami ViewModel
    /// </summary>
    public class MainViewModel : BaseClass.ViewModel
    {
        private ViewModel selectedViewModel;

        //Instance obiektów warstwy ViewModel
        public LoginViewModel loginVM;
        public RegisterViewModel registerVM;
        public ProductsViewModel productsVM;
        public CartViewModel cartVM;
        public ProfileViewModel profileVM;
        public OrderExecuteViewModel orderExecuteVM;
        public OrderProductsViewModel orderproductsVM;

        public HomeViewModel homeVM;

        /// <summary>
        /// Wybrany aktualnie obiekt ViewModel, a co za tym idzie - korespondujący do niego widok
        /// </summary>
        public ViewModel SelectedViewModel
        {
            get
            {
                return selectedViewModel;
            }
            set
            {
                selectedViewModel = value;
                onPropertyChange(nameof(selectedViewModel));
            }
        }

        /// <summary>
        /// Obiekt zalogowanego aktualnie użytkownika
        /// </summary>
        public UserModel LoggedUser
        {
            get
            {
                return CurrentState.LoggedUser;
            }
            set
            { 
                CurrentState.LoggedUser = value;
                onPropertyChange(nameof(LoggedUser));
                onPropertyChange(nameof(LoggedUserString));
            }
        }
        /// <summary>
        /// Login zalogowanego użytkownika
        /// </summary>
        public string LoggedUserString { get { if (LoggedUser is null) return "nie zalogowano"; else return LoggedUser.Login; } set { }}


        public ICommand SwitchViewCommand { get; set; }

        /// <summary>
        /// Handler zdarzenia, gdy nastąpi poprawne zalogowanie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnLoginSuccess(object sender, Templates.LoginData args)
        {
            LoggedUser = args.UserModel;
            SelectedViewModel = homeVM;
        }
        /// <summary>
        /// Handler - przy wybraniu zamówienia w profilu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnOrderProductsCheck(object sender,EventArgs e)
        {
            orderproductsVM = new OrderProductsViewModel();
            orderproductsVM.setorderid(profileVM.SelectedOrderId);
            
            SelectedViewModel = orderproductsVM;
        }
        /// <summary>
        /// Handler - przy potwierdzeniu zawartości koszyka
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCartConfirmed(object sender, EventArgs e)
        {
            orderExecuteVM = new OrderExecuteViewModel();
            orderExecuteVM.OrderDoneEvent += OnOrderDone;
            SelectedViewModel = orderExecuteVM;
        }
        /// <summary>
        /// Handler - przy poprawnym zakończeniu zamawiania
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnOrderDone(object sender, EventArgs e)
        {
            CartContent.CartItemsList = new List<CartItemModel>(); //Remove all items from the cart
            productsVM = new ProductsViewModel(); //Refresh Products Viewmodel so it has refreshed list of products
            SelectedViewModel = homeVM; //After correct execution come back to home
        }
        /// <summary>
        /// Handler - przy wylogowaniu użytkownika w profilu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnUserLogout(object sender, EventArgs e)
        {
            LoggedUser = null;
            SelectedViewModel = homeVM;
        }

        private void OnUserRegistered(object sender, EventArgs e)
        {
            SelectedViewModel = homeVM;
        }

        public MainViewModel()
        {
            loginVM = new LoginViewModel();
            registerVM = new RegisterViewModel();
            productsVM = new ProductsViewModel();
            cartVM = new CartViewModel();
            profileVM = new ProfileViewModel();


            homeVM = new HomeViewModel();
            selectedViewModel = homeVM;

            SwitchViewCommand = new Commands.SwitchViewCommand(this);
            loginVM.LoginChangeView += OnLoginSuccess;
            cartVM.CartConfirmedEvent += OnCartConfirmed;
            profileVM.checktheproducts += OnOrderProductsCheck;
            profileVM.LogoutEvent += OnUserLogout;
            registerVM.UserRegisteredEvent += OnUserRegistered;
        }
    }
}