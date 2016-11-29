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

namespace ActivityDiagram.View
{
    /// <summary>
    /// Interaction logic for LeftPanel.xaml
    /// </summary>
    public partial class LeftPanel : UserControl
    {
        public LeftPanel()
        {
            InitializeComponent();
        }

        private void Square_Button_Click(object sender, RoutedEventArgs e)
        {
            // Nok smartere med binding - bare temp
        }
        private void Triangle_Button_Click(object sender, RoutedEventArgs e)
        {
            // Nok smartere med binding
        }
        private void Circle_Button_Click(object sender, RoutedEventArgs e)
        {
            // Nok smartere med binding
        }
    }
}
