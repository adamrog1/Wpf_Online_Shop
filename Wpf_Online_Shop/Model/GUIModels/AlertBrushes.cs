using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Wpf_Online_Shop.Model
{
    public static class AlertBrushes
    {
        public static Brush WrongBrush()
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString("#FFc43316");
            return brush;
        }

        public static Brush GoodBrush()
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString("#FF6fa836");
            return brush;
        }
    }
}
