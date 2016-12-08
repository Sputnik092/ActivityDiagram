using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Data;

namespace ActivityDiagram.Model
{


    public class Shape : NotifyBase
    {
        private static int counter;

        public int Number { get; }

       // public Grid rootGrid;

       // public Ellipse northPoint = new Ellipse(), southPoint = new Ellipse(), eastPoint = new Ellipse(), westPoint = new Ellipse();

        private String visibility = "Hidden";

        public String Visibility { get { return visibility; } set { visibility = value; NotifyPropertyChanged(); } }

        private double x= 200;

        public double X { get { return x; } set { x = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => CanvasCenterX); } }

        private double y = 200;

        public double Y { get { return y; } set { y = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => CanvasCenterY); } }

        private double width = 100;

        public double Width { get { return width; } set { width = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => CanvasCenterX); NotifyPropertyChanged(() => CenterX); } }

        private double height = 100;

        public double Height { get { return height; } set { height = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => CanvasCenterY); NotifyPropertyChanged(() => CenterY); } }


        public double CanvasCenterX { get { return X + Width / 2; } set { X = value - Width / 2; NotifyPropertyChanged(() => X); } }


        public double CanvasCenterY { get { return Y + Height / 2; } set { Y = value - Height / 2; NotifyPropertyChanged(() => Y); } }


        public double CenterX => Width / 2;

        public double CenterY => Height / 2;


        private bool isSelected;

        public bool IsSelected { get { return isSelected; } set { isSelected = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => SelectedColor); } }

        public Brush SelectedColor => IsSelected ? Brushes.Red : Brushes.Yellow;

        public Shape()
        {
            //rootGrid = new Grid();
            Number = ++counter;
        }

      /*  private void MouseExitPoint(object sender, MouseEventArgs e)
        {
            
        }

        private void MouseEnterPoint(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MouseDownPoint(object sender, MouseButtonEventArgs e)
        {
            
        }
        */
        public override string ToString() => Number.ToString();
    }
   

}
