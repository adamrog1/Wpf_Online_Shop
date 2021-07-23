using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.Model
{
    /// <summary>
    /// Klasa przechowująca aktualne dane aplikacji
    /// </summary>
    static public class CurrentState
    {
        private static UserModel loggedUser = null;

        /// <summary>
        /// Model zalogowanego aktualnie użytkownika
        /// </summary>
        public static UserModel LoggedUser
        {
            get { return loggedUser; }
            set { loggedUser = value; }
        }


    }
}
