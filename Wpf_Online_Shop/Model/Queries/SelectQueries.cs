using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.Model.Queries
{
    /// <summary>
    /// Klasa opisująca niektóre kwerendy wykonywane w bazie danych
    /// </summary>
    public static class SelectQueries
    {
        /// <summary>
        /// Kwerenda pobierająca produkty, później umożliwiając konwersję danych do listy obiektów typu ProductModel
        /// </summary>
        /// <returns>string</returns>
        public static string getProducts()
        {
            return @"select p.id as Id, p.name as Name, p.Category as Category,p.pln as PLN, p.Grosz as Grosz, p.Description as Description, m.name as Manufacturer, m.Address as Address, m.City as City, c.name as Country, p.Amount 
from Produkty as p 
INNER join Manufacturers m 
on p.Manufacturer = m.Id 
inner join Countries c 
on m.CountryCode = c.Code";
        }
    }
}
