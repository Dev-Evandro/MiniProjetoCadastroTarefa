using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI.Framework.Controls
{
    /// <summary>
    /// Interação lógica para StatusBadge.xam
    /// </summary>
    public partial class StatusBadge : UserControl
    {
        public StatusBadge()
        {
            InitializeComponent();
        }

        public string Status
        {
            get { return (string)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register(
                nameof(Status),
                typeof(string),
                typeof(StatusBadge),
                new PropertyMetadata(string.Empty));
    }
}
