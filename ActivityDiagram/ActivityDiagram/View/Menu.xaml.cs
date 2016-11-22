using Microsoft.Win32;
using System;
using System.IO;
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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
       


        public Menu()
        {
            InitializeComponent();
   
        }

     


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnuSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog savefiledialog = new SaveFileDialog();

            savefiledialog.Filter = "Text file (*.txt)|*.txt|C# file (*.cs)|*.cs";

            if (savefiledialog.ShowDialog() == true)
            {
                File.WriteAllText(savefiledialog.FileName, txtEditor.Text);

               

            }
    }

        private void mnuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();

            if(openfiledialog.ShowDialog()==true)
            {
                txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }
    }

}

