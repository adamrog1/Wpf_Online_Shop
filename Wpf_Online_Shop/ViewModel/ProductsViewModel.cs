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

        private List<ProductModel> ProductListByCategory(int categoryId = 0)
        {
            List<ProductModel> fullProductList = Model.DatabaseConnection.SqliteSelect.GetProducts();
            if (categoryId <= 0) return fullProductList;
            return fullProductList.Where(el => el.Category == categoryId).ToList();
        }

        public ProductModel SelectedProduct { get; set; } //Binded to selectedItem property in datagrid.


        public ICommand productSwitchCategoryCommand;

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

        public ProductsViewModel()
        {
            ProductList = ProductListByCategory(0);
        }

    }
}
