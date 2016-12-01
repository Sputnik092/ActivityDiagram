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

        public ObservableCollection<Line> Lines { get; set; }

        public ICommand AddSquareCommand { get; }
        public ICommand AddCircleCommand { get; }
        public ICommand AddTriangleCommand { get; }
        public ICommand AddLineCommand { get; }
        public ICommand RemoveLinesCommand { get; }


        private UndoRedoController undoRedoController = UndoRedoController.Instance;


        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>

        // Keeps track of the state, depending on whether a line is being added or not.
        private bool isAddingLine;
        // Used for saving the shape that a line is drawn from, while it is being drawn.
        private Circle addingLineFrom;

        public double ModeOpacity => isAddingLine ? 0.4 : 1.0;

        

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {

            Rectangles = new ObservableCollection<Rectangle>(){
                new Rectangle() { X = 30, Y = 40, Width = 200, Height = 80 } };

            Circles = new ObservableCollection<Circle>(){
                new Circle() { X = 30, Y = 40, Width = 80, Height = 80 } };

            Triangles = new ObservableCollection<Triangle>(){
                new Triangle() { X = 30, Y = 40, Width = 80, Height = 80 } };

            AddSquareCommand = new RelayCommand(AddSquare);
            AddCircleCommand = new RelayCommand(AddCircle);
            AddTriangleCommand = new RelayCommand(AddTriangle);

            UndoCommand = new RelayCommand(undoRedoController.Undo, undoRedoController.CanUndo);
            RedoCommand = new RelayCommand(undoRedoController.Redo, undoRedoController.CanRedo);

            Lines = new ObservableCollection<Line>() {
                new Line() { From = Circles.ElementAt(0), To = Circles.ElementAt(1) }
            };

            AddLineCommand = new RelayCommand(AddLine);
            RemoveLinesCommand = new RelayCommand<IList>(RemoveLines, CanRemoveLines);

        }

        private void AddSquare()
        {
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

        private void AddLine()
        {
            isAddingLine = true;
            RaisePropertyChanged(() => ModeOpacity);
        }

        private bool CanRemoveLines(IList _edges) => _edges.Count >= 1;

        private void RemoveLines(IList _lines)
        {
            undoRedoController.AddAndExecute(new RemoveLinesCommand(Lines, _lines.Cast<Line>().ToList()));
        }

        private Circle TargetShape(MouseEventArgs e)
        {
            // Here the visual element that the mouse is captured by is retrieved.
            var shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
            // From the shapes visual element, the Shape object which is the DataContext is retrieved.
            return (Circle)shapeVisualElement.DataContext;
        }



        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}