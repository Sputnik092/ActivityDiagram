using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;

namespace ActivityDiagram.Model
{
    public class Rectangle : Shape
    {
        public Rectangle()
        {
            /*northPoint.Width = 10;
            northPoint.Height = 10;
            northPoint.VerticalAlignment = VerticalAlignment.Bottom;
            northPoint.HorizontalAlignment = HorizontalAlignment.Center;
            Panel.SetZIndex(northPoint, 2);
            northPoint.Visibility = System.Windows.Visibility.Visible;
            northPoint.StrokeThickness = 10;
            northPoint.Stroke = Brushes.Green;
            northPoint.Fill = Brushes.Green;

            rootGrid.Children.Add(northPoint);
            */
        }
        public override string ToString() => Number.ToString();
    }

}


