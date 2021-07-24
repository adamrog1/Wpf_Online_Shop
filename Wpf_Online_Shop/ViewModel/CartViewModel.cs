using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.ViewModel
{
    using BaseClass;
    using System.Windows;
    using System.Windows.Input;
    using Model;
    using System.Collections.ObjectModel;

    public class CartViewModel : ViewModel
    {
        /// <summary>
        /// Lista przedmiotów w koszyku
        /// </summary>
        public ObservableCollection<CartItemModel> CartItemsList
        {
            get
            {
                return new ObservableCollection<CartItemModel>(CartContent.CartItemsList);
            }
            private set
            {
                onPropertyChange(nameof(CartItemsList));
            }
        }

        /// <summary>
        /// Suma przedmiotów w koszyku w groszach
        /// </summary>
        public int ItemsCostSum
        {
            get
            {
                return CartContent.GetCartItemsCost;
            }
            set
            { }
        }

        /// <summary>
        /// Tekst pokazujący cenę przedmiotów w koszyku
        /// </summary>
        public string ItemsCostSumString
        {
            get
            {
                return CartContent.GetCartItemsCostText;
            }
            private set
            {
            }
        }

        /// <summary>
        /// Wybrany przedmiot w koszyku
        /// </summary>
        public CartItemModel SelectedCartItem { get; set; }

        /// <summary>
        /// Zdarzenie, wywołujące się w momencie gdy użytkownik chce przejść do zamawiania poprzez odpowiedni button
        /// </summary>
        public event EventHandler<EventArgs> CartConfirmedEvent;

        private ICommand cartConfirmedCommand;

        /// <summary>
        /// Komenda obsługująca kliknięcie buttona zatwierdzającego koszyk chcąc przejść do widoku zamawiania.
        /// </summary>
        public ICommand CartConfirmedCommand
        {
            get
            {
                return cartConfirmedCommand ?? (cartConfirmedCommand = new RelayCommand(
                    (p) => {
                        if (CartContent.CartItemsList is null || CartContent.CartItemsList.Count <=0)
                        {
                            MessageBox.Show("Twój koszyk jest pusty.");
                            return;
                        }
                        if (CurrentState.LoggedUser is null)
                        {
                            MessageBox.Show("Aby dokończyć zamówienie, musisz być zalogowany.");
                            return;
                        }
                        if (CurrentState.LoggedUser.Cash < ItemsCostSum)
                        {
                            MessageBox.Show("Masz za mało środków na koncie");
                            return;
                        }
                        CartConfirmedEvent?.Invoke(this,EventArgs.Empty);
                    }, p => true));
            }
        }

        private ICommand removeItemFromCartCommand;
        /// <summary>
        /// Komenda obsługująca usuwanie przedmiotu z koszyka
        /// </summary>
        public ICommand RemoveItemFromCartCommand
        {
            get
            {
                return removeItemFromCartCommand ?? (removeItemFromCartCommand = new RelayCommand(
                    (p) =>
                    {
                        CartContent.RemoveItemFromCart(SelectedCartItem);
                        onPropertyChange(nameof(CartItemsList));
                        onPropertyChange(nameof(ItemsCostSumString));
                    }, p => true));
            }
        }

        public CartViewModel()
        {

        }
    }
}
