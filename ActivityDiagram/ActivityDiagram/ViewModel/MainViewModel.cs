using GalaSoft.MvvmLight;
using ActivityDiagram.Model;
using System.Windows.Input;
//using GalaSoft.MvvmLight.Command;
using ActivityDiagram.Command;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GalaSoft.MvvmLight.CommandWpf;


namespace ActivityDiagram.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    /// 

   
    public class MainViewModel : ViewModelBase
    {

        public ObservableCollection<Rectangle> Rectangles { get; set; }
        public ObservableCollection<Circle> Circles { get; set; }
        public ObservableCollection<Triangle> Triangles { get; set; }

        public ICommand AddSquareCommand { get; }
        public ICommand AddCircleCommand { get; }
        public ICommand AddTriangleCommand { get; }


        private UndoRedoController undoRedoController = UndoRedoController.Instance;


        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
    

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {

            Rectangles = new ObservableCollection<Rectangle>(){
                new Rectangle() { X = 30, Y = 40, Width = 80, Height = 80 } };

            Circles = new ObservableCollection<Circle>(){
                new Circle() { X = 30, Y = 40, Width = 80, Height = 80 } };

            Triangles = new ObservableCollection<Triangle>(){
                new Triangle() { X = 30, Y = 40, Width = 80, Height = 80 } };

            AddSquareCommand = new RelayCommand(AddSquare);
            AddCircleCommand = new RelayCommand(AddCircle);
            AddTriangleCommand = new RelayCommand(AddTriangle);

            UndoCommand = new RelayCommand(undoRedoController.Undo, undoRedoController.CanUndo);
            RedoCommand = new RelayCommand(undoRedoController.Redo, undoRedoController.CanRedo);

        }

        private void AddSquare()
        {
            System.Console.WriteLine("Add!");
            undoRedoController.AddAndExecute(new AddSquareCommand(Rectangles, new Rectangle()));
        }

        private void AddCircle()
        {
             undoRedoController.AddAndExecute(new AddCircleCommand(Circles, new Circle()));
        }

        private void AddTriangle()
        {
            undoRedoController.AddAndExecute(new AddTriangleCommand(Triangles, new Triangle()));
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}