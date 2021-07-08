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
        private string[] categories;

        public string[] Categories
        {
            get { return ProductCategories.Get(); }
        }

        public List<ProductModel> ProductList
        {
            get
            {
                return Model.DatabaseConnection.SqliteSelect.GetProducts();
            }
        }


        public ICommand productSwitchCategoryCommand;

        public ICommand ProductSwitchCategoryCommand
        {
            get
            {
                return productSwitchCategoryCommand ?? (productSwitchCategoryCommand = new RelayCommand(
                    (p) => {
                        List<ProductModel> list = ProductList;

                    }, p => true));
            }
        }

    }
}
