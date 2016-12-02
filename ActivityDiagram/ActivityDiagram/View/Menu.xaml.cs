using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;

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
          //    SaveFileDialog savefiledialog = new SaveFileDialog();

           //    savefiledialog.Filter = "Text file (*.txt)|*.txt|C# file (*.cs)|*.cs";

           //   if (savefiledialog.ShowDialog() == true)
            //  {

            //    using (Stream s = File.Open(savefiledialog.FileName, FileMode.CreateNew))
             //   using (StreamWriter sw = new StreamWriter(s))
             //   {
            //        sw.Write(RichTextBox.TextInputEvent);

                    //      File.WriteAllText(savefiledialog.FileName, txtEditor.Text);



            //    }
           // }

        }

        private void mnuOpen_Click(object sender, RoutedEventArgs e)
        {
           // OpenFileDialog openfiledialog = new OpenFileDialog();
            //openfiledialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //if (openfiledialog.ShowDialog()==true)
            //{
             //   string strfilename = openfiledialog.FileName;
              //  string filetext = File.ReadAllText(strfilename);
                

            //}
        }
    }

}

