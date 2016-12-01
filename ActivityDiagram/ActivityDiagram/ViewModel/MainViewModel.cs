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
        private bool isAddingLine;

        private Circle addingLineFromCircle;

        private Point initialMousePosition;

        private Point initialCirclePosition;

        public double ModeOpacity => isAddingLine ? 0.4 : 1.0;
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

        public ICommand MouseDownCircleCommand { get; }
        public ICommand MouseMoveCircleCommand { get; }
        public ICommand MouseUpCircleCommand { get; }

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>

        // Keeps track of the state, depending on whether a line is being added or not.
        // Used for saving the shape that a line is drawn from, while it is being drawn.
        private Circle addingLineFrom;


        



        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {

            Rectangles = new ObservableCollection<Rectangle>(){
                //  new Rectangle() { X = 30, Y = 40, Width = 80, Height = 80 } 
            };

            Circles = new ObservableCollection<Circle>(){
                new Circle() { X = 30, Y = 40, Width = 80, Height = 100 }, 
                new Circle()  { X = 30, Y = 40, Width = 50, Height = 50 }
            };

          

            Triangles = new ObservableCollection<Triangle>(){
                new Triangle() { X = 30, Y = 40, Width = 100, Height = 100 }
            };

            AddSquareCommand = new RelayCommand(AddSquare);
            AddCircleCommand = new RelayCommand(AddCircle);
            AddTriangleCommand = new RelayCommand(AddTriangle);

            UndoCommand = new RelayCommand(undoRedoController.Undo, undoRedoController.CanUndo);
            RedoCommand = new RelayCommand(undoRedoController.Redo, undoRedoController.CanRedo);

            MouseDownCircleCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownCircle);
            MouseMoveCircleCommand = new RelayCommand<MouseEventArgs>(MouseMoveCircle);
            MouseUpCircleCommand = new RelayCommand<MouseButtonEventArgs>(MouseUpCircle);

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
        private void MouseDownCircle(MouseButtonEventArgs e)
        {
            // Checks that a line is not being drawn.
            if (!isAddingLine)
            {
                // The Shape is gotten from the mouse event.
                var shape = TargetCircle(e);
                // The mouse position relative to the target of the mouse event.
                var mousePosition = RelativeMousePosition(e);
                System.Console.WriteLine(mousePosition);
                // When the shape is moved with the mouse, the MouseMoveShape method is called many times, 
                //  for each part of the movement.
                // Therefore to only have 1 Undo/Redo command saved for the whole movement, the initial position is saved, 
                //  during the start of the movement, so that it together with the final position, 
                //  from when the mouse is released, can become one Undo/Redo command.
                // The initial shape position is saved to calculate the offset that the shape should be moved.
                initialMousePosition = mousePosition;
                initialCirclePosition = new Point(shape.X, shape.Y);

                // The mouse is captured, so the current shape will always be the target of the mouse events, 
                //  even if the mouse is outside the application window.
                e.MouseDevice.Target.CaptureMouse();
            }
        }

        // This is only used for moving a Shape, and only if the mouse is already captured.
        // This uses 'var' which is an implicit type variable (https://msdn.microsoft.com/en-us/library/bb383973.aspx).
        private void MouseMoveCircle(MouseEventArgs e)
        {
           
            // Checks that the mouse is captured and that a line is not being drawn.
            if (Mouse.Captured != null && !isAddingLine)
            {
                // The Shape is gotten from the mouse event.
                var circle = TargetCircle(e);
                // The mouse position relative to the target of the mouse event.
                var mousePosition = RelativeMousePosition(e);
                // temp
                // The Shape is moved by the offset between the original and current mouse position.
                // The View (GUI) is then notified by the Shape, that its properties have changed.

                if (initialCirclePosition.X + (mousePosition.X - initialMousePosition.X) > 0)
                {
                    circle.X = initialCirclePosition.X + (mousePosition.X - initialMousePosition.X);
                    circle.Y = initialCirclePosition.Y + (mousePosition.Y - initialMousePosition.Y);
                }

            }
        }

        // There are two reasons for doing a 'MouseUp'.
        // Either a Line is being drawn, and the second Shape has just been chosen
        //  or a Shape is being moved and the move is now done.
        // This uses 'var' which is an implicit type variable (https://msdn.microsoft.com/en-us/library/bb383973.aspx).
        private void MouseUpCircle(MouseButtonEventArgs e)
        {
            // Used for adding a Line.
            if (isAddingLine)
            {
                // Because a MouseUp event has happened and a Line is currently being drawn, 
                //  the Shape that the Line is drawn from or to has been selected, and is here retrieved from the event parameters.
                var shape = TargetCircle(e);
                // This checks if this is the first Shape chosen during the Line adding operation, 
                //  by looking at the addingLineFrom variable, which is empty when no Shapes have previously been choosen.
                // If this is the first Shape choosen, and if so, the Shape is saved in the AddingLineFrom variable.
                //  Also the Shape is set as selected, to make it look different visually.
                if (addingLineFromCircle == null) { addingLineFromCircle = shape; addingLineFromCircle.IsSelected = true; }
                // If this is not the first Shape choosen, and therefore the second, 
                //  it is checked that the first and second Shape are different.
                else if (addingLineFromCircle.Number != shape.Number)
                {
                    // Now that it has been established that the Line adding operation has been completed succesfully by the user, 
                    //  a Line is added using an 'AddLineCommand', with a new Line given between the two shapes chosen.
                    //undoRedoController.AddAndExecute(new AddLineCommand(Lines, new Line() { From = addingLineFromCircle, To = shape }));
                    // The property used for visually indicating that a Line is being Drawn is cleared, 
                    //  so the View can return to its original and default apperance.
                    addingLineFromCircle.IsSelected = false;
                    // The 'isAddingLine' and 'addingLineFrom' variables are cleared, 
                    //  so the MainViewModel is ready for another Line adding operation.
                    isAddingLine = false;
                    addingLineFromCircle = null;
                    // The property used for visually indicating which Shape has already chosen are choosen is cleared, 
                    //  so the View can return to its original and default apperance.
                    RaisePropertyChanged(() => ModeOpacity);
                }
            }
            // Used for moving a Shape.
            else
            {
                // The Shape is gotten from the mouse event.
                var circle = TargetCircle(e);
                // The mouse position relative to the target of the mouse event.
                var mousePosition = RelativeMousePosition(e);

                // The Shape is moved back to its original position, so the offset given to the move command works.
                circle.X = initialCirclePosition.X;
                circle.Y = initialCirclePosition.Y;

                // Now that the Move Shape operation is over, the Shape is moved to the final position, 
                //  by using a MoveNodeCommand to move it.
                // The MoveNodeCommand is given the offset that it should be moved relative to its original position, 
                //  and with respect to the Undo/Redo functionality the Shape has only been moved once, with this Command.
                undoRedoController.AddAndExecute(new MoveCircleCommand(circle, mousePosition.X - initialMousePosition.X, mousePosition.Y - initialMousePosition.Y));

                // The mouse is released, as the move operation is done, so it can be used by other controls.
                e.MouseDevice.Target.ReleaseMouseCapture();
            }
        }

        // Gets the shape that was clicked.
        private Circle TargetCircle(MouseEventArgs e)
        {
            // Here the visual element that the mouse is captured by is retrieved.
            var shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
            // From the shapes visual element, the Shape object which is the DataContext is retrieved.
            return (Circle)shapeVisualElement.DataContext;
        }

        // Gets the mouse position relative to the canvas.
        private Point RelativeMousePosition(MouseEventArgs e)
        {
            // Here the visual element that the mouse is captured by is retrieved.
            var shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
            // The canvas holding the shapes visual element, is found by searching up the tree of visual elements.
            var canvas = FindParentOfType<Canvas>(shapeVisualElement);
            // The mouse position relative to the canvas is gotten here.
            return Mouse.GetPosition(canvas);
        }

        // Recursive method for finding the parent of a visual element of a certain type, 
        //  by searching up the visual tree of parent elements.
        // The '() ? () : ()' syntax, returns the second part if the first part is true, otherwise it returns the third part.
        // This uses 'dynamic' which is an dynamic type variable (https://msdn.microsoft.com/en-us/library/dd264736.aspx).
        private static T FindParentOfType<T>(DependencyObject o)
        {
            dynamic parent = VisualTreeHelper.GetParent(o);
            return parent.GetType().IsAssignableFrom(typeof(T)) ? parent : FindParentOfType<T>(parent);
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