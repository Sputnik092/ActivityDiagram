using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityDiagram.Model
{
   public class Line : NotifyBase
    {
        private Circle from;

        public Circle From { get { return from; } set { from = value; NotifyPropertyChanged(); } }

        private Triangle start;

        public Triangle Start { get { return start; } set { start = value; NotifyPropertyChanged(); } }

        private Rectangle sourc;

        public Rectangle Sourc { get { return sourc; } set { sourc = value; NotifyPropertyChanged(); } }


        private Circle to;
        public Circle To { get { return to; } set { to = value; NotifyPropertyChanged(); } }

        private Triangle des;
        public Triangle Des { get { return des; } set { des = value; NotifyPropertyChanged(); } }

        private Rectangle target;
        public Rectangle Target { get { return target; } set { target = value; NotifyPropertyChanged(); } }
    }
}
