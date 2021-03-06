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
    using System.Windows.Media;
    using Wpf_Online_Shop.Model;

    public class ProductsViewModel : ViewModel
    {
        public string[] Categories
        {
            get { return ProductCategories.Get(); }
        }

        private List<ProductModel> productlist;

        public List<ProductModel> ProductList
        {
            get
            {
                return productlist;
            }
            set
            {
                productlist = value;
                onPropertyChange(nameof(productlist));
            }
        }

        private System.Windows.Media.Brush alertColor;
        /// <summary>
        /// Określa kolor alertu
        /// </summary>
        public System.Windows.Media.Brush AlertColor
        {
            get { return alertColor; }
            set { alertColor = value; onPropertyChange(nameof(AlertColor)); }
        }

        private string alertText;
        /// <summary>
        /// Określa tekst alertu
        /// </summary>
        public string AlertText
        {
            get { return alertText; }
            set { alertText = value; onPropertyChange(nameof(AlertText)); }
        }



        private int productAmount = 0;
        /// <summary>
        /// Dotyczy ilości produktów wybieranej przez użytkownika
        /// </summary>
        public int ProductAmount
        {
            get { return Convert.ToInt32(productAmount); }
            set
            {
                productAmount = Convert.ToInt32(value);
                onPropertyChange(nameof(productAmount));
            }
        }

        /// <summary>
        /// Filtruje listę produktów po kategorii
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        private List<ProductModel> ProductListByCategory(int categoryId = 0)
        {
            try
            {
                List<ProductModel> fullProductList = Model.DatabaseConnection.SqliteSelect.GetProducts();
                if (categoryId <= 0) return fullProductList;
                return fullProductList.Where(el => el.Category == categoryId).ToList();
            }
            catch
            {
                return new List<ProductModel>();
            }
        }
        /// <summary>
        /// Określa wybrany produkt z listy
        /// </summary>
        public ProductModel SelectedProduct { get; set; } //Binded to selectedItem property in datagrid.

        /// <summary>
        /// Zmienia alert
        /// </summary>
        /// <param name="message"></param>
        /// <param name="isWrong"></param>
        private void SetAlert(string message, bool isWrong = true)
        {
            AlertText = message;
            if (isWrong)
                AlertColor = AlertBrushes.WrongBrush();
            else
                AlertColor = AlertBrushes.GoodBrush();
        }


        private ICommand productSwitchCategoryCommand;

        /// <summary>
        /// Komenda obsługująca filtrowanie kategorii produktów
        /// </summary>
        public ICommand ProductSwitchCategoryCommand
        {
            get
            {
                return productSwitchCategoryCommand ?? (productSwitchCategoryCommand = new RelayCommand(
                    (p) => {
                        ProductList = ProductListByCategory(Convert.ToInt32(p));
                    }, p => true));
            }
        }

        private ICommand addToCartCommand;

        /// <summary>
        /// Komenda dodania produktów do koszyka
        /// </summary>
        public ICommand AddToCartCommand
        {
            get
            {
                return addToCartCommand ?? (addToCartCommand = new RelayCommand(
                    (p) => {
                        /* 
                        if (CurrentState.LoggedUser==null)
                        {
                            AlertText = "Nie jesteś zalogowany";
                            AlertColor = AlertBrushes.WrongBrush();
                            return;
                        }
                        */
                        if (SelectedProduct is null)
                        {
                            SetAlert("Nie wybrano produktu");
                            return;
                        }
                        CartItemModel existingItem = CartContent.GetExistingItemById(SelectedProduct.Id);
                        if (productAmount <= 0)
                        {
                            SetAlert("Ilość musi być dodatnia");
                            return;
                        }
                        if (existingItem == null)
                        {

                            CartItemModel newItem = new CartItemModel(SelectedProduct, ProductAmount);
                            if (SelectedProduct.CheckAmount(ProductAmount))
                            {
                                CartContent.CartItemsList.Add(newItem);
                                SetAlert("Dodano do koszyka.", false);
                            }
                            else
                            {
                                SetAlert("Przekroczono dostępną ilość");
                            }
                        }
                        else
                        {
                            if (existingItem.CartAmountIncrease(ProductAmount))
                            {
                                SetAlert("Edytowano element w koszyku.", false);
                            }
                            else
                            {
                                SetAlert("Przekroczono dostępną ilość");
                            }
                        }
                        ProductAmount = 0;
                    }, p => true));
            }
        }


        public ProductsViewModel()
        {
            ProductList = ProductListByCategory(0);
        }

    }
}
