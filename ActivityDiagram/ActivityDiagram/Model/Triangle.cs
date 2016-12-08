using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ActivityDiagram.Model
{
    public class Triangle : Shape
    {
        public Triangle()
        {
            Polygon p = new Polygon();
            PointCollection myPointCollection = new PointCollection();
            myPointCollection.Add(new System.Windows.Point(1, 50));
            myPointCollection.Add(new System.Windows.Point(10, 80));
            myPointCollection.Add(new System.Windows.Point(50, 50));
            p.Points = myPointCollection;
             //<Polygon Points="100,100 0,100 50,0" Stroke="Black" StrokeThickness="4" Fill="White">

        }
        public override string ToString() => Number.ToString();
    }

}


