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
            northPoint.Width = 10;
            northPoint.Height = 10;
            northPoint.VerticalAlignment = VerticalAlignment.Top;
            northPoint.HorizontalAlignment = HorizontalAlignment.Center;
            Panel.SetZIndex(northPoint, 2);

        }
        public override string ToString() => Number.ToString();
    }

}


