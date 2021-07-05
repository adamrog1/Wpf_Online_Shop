using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Online_Shop.Templates
{
    public class LoginData : EventArgs
    {
        public string Login { get; set; }
        public DateTime DateTime { get; set; }
    }
}
