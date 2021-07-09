using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.Model
{
    static public class CurrentState
    {
        private static string loggedUser = null;

        public static string LoggedUser
        {
            get { return loggedUser ?? "nie zalogowano"; }
            set { loggedUser = value; }
        }


    }
}
