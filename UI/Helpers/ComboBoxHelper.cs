using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace UI.Helpers
{
    public static class ComboBoxHelper
    {
        public static void Open(ComboBox combo)
        {
            combo.Focus();

            combo.Dispatcher.InvokeAsync(() =>
            {
                combo.IsDropDownOpen = true;
            }, System.Windows.Threading.DispatcherPriority.Background);
        }

        public static void Open(ComboBox combo, KeyEventArgs e)
        {
            Open(combo);
            e.Handled = true;
        }
    }
}
