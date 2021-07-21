using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Wpf_Online_Shop.Model
{
    /// <summary>
    /// Kolory alertów
    /// </summary>
    public static class AlertBrushes
    {
        /// <summary>
        /// Kolor alertu przy operacji niedozwolonej
        /// </summary>
        /// <returns>System.Windows.Media.Brush</returns>
        public static Brush WrongBrush()
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString("#FFc43316");
            return brush;
        }

        /// <summary>
        /// Kolor alertu przy dozwolonej operacji
        /// </summary>
        /// <returns>System.Windows.Media.Brush</returns>
        public static Brush GoodBrush()
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString("#FF6fa836");
            return brush;
        }
    }
}
