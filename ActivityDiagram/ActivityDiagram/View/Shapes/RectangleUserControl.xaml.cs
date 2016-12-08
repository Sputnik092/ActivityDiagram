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

namespace ActivityDiagram.View.Shapes
{
    /// <summary>
    /// Interaction logic for CircleUserControl.xaml
    /// </summary>
    public partial class RectangleUserControl : UserControl
    {
        public RectangleUserControl()
        {

            InitializeComponent();

        }
        //  <Ellipse Name="southPoint" HorizontalAlignment="Center" VerticalAlignment="Bottom" Width="10" Height="10" Margin="-4" Fill="Gray" Panel.ZIndex="2" Visibility="{Binding Visibility}"></Ellipse>
        protected void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
