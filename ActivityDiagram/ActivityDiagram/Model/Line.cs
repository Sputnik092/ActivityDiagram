using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace ActivityDiagram.Model
{
   public class Line : NotifyBase
    {
        private Shape from;

        public Shape From { get { return from; } set { from = value; NotifyPropertyChanged(); } }

        private Shape to;
        public Shape To { get { return to; } set { to = value; NotifyPropertyChanged(); } }

    }
}
