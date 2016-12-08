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
using Microsoft.Win32;
using System.IO;
using System;
using System.Windows.Shapes;

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

       


        public RelayCommand SaveDiagramCommand
        {
            get
            {
                return _saveDiagramCommand ?? (_saveDiagramCommand = new RelayCommand(() => {
                    var dialog = new SaveFileDialog() { Title = "Save Diagram", Filter = "XML Document (.xml)|*.xml", DefaultExt = "xml" };
                    if (dialog.ShowDialog() != true)
                        return;
                    using (var stream = dialog.OpenFile())
                    {
                        //write out file to disk
                    }
                }));
            }
        }
        Stream myStream = null;
        public RelayCommand openDiagramCommand
        {
            get
            {
                return _openDiagramCommand ?? (_openDiagramCommand = new RelayCommand(() => {
                    var opendialog = new Microsoft.Win32.OpenFileDialog();
                    if (opendialog.ShowDialog() != true)
                        return;
                    try
                    {
                        if ((myStream = opendialog.OpenFile()) != null)
                        {
                            using (myStream)
                            {
                                // Insert code to read the stream here.
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }




                    // using (var ostream = opendialog.OpenFile())
                    //{
                    //Open
                    // }
                }));
            }
        }

        private RelayCommand _openDiagramCommand;

        private RelayCommand _saveDiagramCommand;

        private Model.Shape lastEnteredShape;

        private bool isAddingLine;

        private Model.Shape addingLineFrom;

        private Point initialMousePosition;

        private Point initialShapePosition;

        public double ModeOpacity => isAddingLine ? 0.4 : 1.0;

        public ObservableCollection<Model.Shape> Shapes { get; set; }

        public ObservableCollection<Model.Line> Lines { get; set; }


      
        public ICommand AddSquareCommand { get; }
        public ICommand AddCircleCommand { get; }
        public ICommand AddTriangleCommand { get; }
        public ICommand AddLineCommand { get; }
        public ICommand RemoveLinesCommand { get; }

        public ICommand RemoveCircleCommand { get; }


        private UndoRedoController undoRedoController = UndoRedoController.Instance;

        public ICommand ClearCanvasCommand { get; }

        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }

        //public ICommand CopyCommand { get; }

        public ICommand MouseDownShapeCommand { get; }
        public ICommand MouseMoveShapeCommand { get; }
        public ICommand MouseUpShapeCommand { get; }
        public ICommand MouseHoverShapeCommand { get; }
        public ICommand MouseExitShapeCommand { get; }

        public ICommand MouseDownPointCommand { get; }
        public ICommand MouseHoverPointCommand { get; }
        public ICommand MouseExitPointCommand { get; }


        public MainViewModel()
        {
            
            Shapes = new ObservableCollection<Model.Shape>()
            {
                new Circle() { X = 30, Y = 40, Width = 120, Height = 140 },
                new Circle()  { X = 150, Y = 100, Width = 150, Height = 150 },
                new Model.Rectangle() { X = 30, Y = 40, Width = 80, Height = 20 },
                new Triangle() { X = 30, Y = 40, Width = 100, Height = 100 }
            };

     

            AddSquareCommand = new RelayCommand(AddSquare);
            AddCircleCommand = new RelayCommand(AddCircle);
            AddTriangleCommand = new RelayCommand(AddTriangle);

            
           

            ClearCanvasCommand = new RelayCommand(ClearCanvas);

            UndoCommand = new RelayCommand(undoRedoController.Undo, undoRedoController.CanUndo);
            RedoCommand = new RelayCommand(undoRedoController.Redo, undoRedoController.CanRedo);
           // CopyCommand = new RelayCommand(Copy);

            MouseDownShapeCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownShape);
            MouseMoveShapeCommand = new RelayCommand<MouseEventArgs>(MouseMoveShape);
            MouseUpShapeCommand = new RelayCommand<MouseButtonEventArgs>(MouseUpShape);
            MouseHoverShapeCommand = new RelayCommand<MouseEventArgs>(MouseHoverShape);
            MouseExitShapeCommand = new RelayCommand<MouseEventArgs>(MouseExitShape);

            MouseDownPointCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownPoint);
            MouseHoverPointCommand = new RelayCommand<MouseEventArgs>(MouseHoverShape);
            MouseExitPointCommand = new RelayCommand<MouseEventArgs>(MouseExitShape);
            // DoubleClickTextBlock = new RelayCommand<MouseButtonEventArgs>(DoubleClickText);

            Lines = new ObservableCollection<Model.Line>();

            AddLineCommand = new RelayCommand(AddLine);
            RemoveLinesCommand = new RelayCommand<IList>(RemoveLines, CanRemoveLines);
            RemoveCircleCommand = new RelayCommand(RemoveCircle);

        }

       

        private void ClearCanvas()
        {
            undoRedoController.ClearAndExecute(new ClearCanvasCommand(Shapes, Lines));

        }

        /*private void Copy()
        {
            undoRedoController.AddAndExecute(new CopyCommand(Circles));
        }*/

        private void AddSquare()
        {
            undoRedoController.AddAndExecute(new AddSquareCommand(Shapes, new Model.Rectangle()));
        }

        private void AddCircle()
        {
             undoRedoController.AddAndExecute(new AddCircleCommand(Shapes, new Circle()));
        }

        private void AddTriangle()
        {
            undoRedoController.AddAndExecute(new AddTriangleCommand(Shapes, new Triangle()));
        }

        private void MouseDownShape(MouseButtonEventArgs e)
        {
            // Checks that a line is not being drawn.
            if (!isAddingLine)
            {
                // The Shape is gotten from the mouse event.
                var shape = Target(e);

                shape.IsSelected = true;
                // The mouse position relative to the target of the mouse event.
                var mousePosition = RelativeMousePosition(e);
                // When the shape is moved with the mouse, the MouseMoveShape method is called many times, 
                //  for each part of the movement.
                // Therefore to only have 1 Undo/Redo command saved for the whole movement, the initial position is saved, 
                //  during the start of the movement, so that it together with the final position, 
                //  from when the mouse is released, can become one Undo/Redo command.
                // The initial shape position is saved to calculate the offset that the shape should be moved.
                initialMousePosition = mousePosition;
                initialShapePosition = new Point(shape.X, shape.Y);

                // The mouse is captured, so the current shape will always be the target of the mouse events, 
                //  even if the mouse is outside the application window.
                e.MouseDevice.Target.CaptureMouse();
            }
        }

        // This is only used for moving a Shape, and only if the mouse is already captured.
        // This uses 'var' which is an implicit type variable (https://msdn.microsoft.com/en-us/library/bb383973.aspx).
        private void MouseMoveShape(MouseEventArgs e)
        {
           
            // Checks that the mouse is captured and that a line is not being drawn.
            if (Mouse.Captured != null && !isAddingLine)
            {
                // The Shape is gotten from the mouse event.
                var shape = Target(e);
                // The mouse position relative to the target of the mouse event.
                var mousePosition = RelativeMousePosition(e);
                // temp
                // The Shape is moved by the offset between the original and current mouse position.
                // The View (GUI) is then notified by the Shape, that its properties have changed.

                if (initialShapePosition.X + (mousePosition.X - initialMousePosition.X) > -45 && initialShapePosition.Y + (mousePosition.Y - initialMousePosition.Y) > 0)
                {
                    shape.X = initialShapePosition.X + (mousePosition.X - initialMousePosition.X);
                    shape.Y = initialShapePosition.Y + (mousePosition.Y - initialMousePosition.Y);
                }

            }
        }

        private void MouseUpShape(MouseButtonEventArgs e)
        {
          
                // The Shape is gotten from the mouse event.
                var shape = Target(e);
                // The mouse position relative to the target of the mouse event.
                var mousePosition = RelativeMousePosition(e);

                // The Shape is moved back to its original position, so the offset given to the move command works.
                shape.X = initialShapePosition.X;
                shape.Y = initialShapePosition.Y;

                if (initialShapePosition.X + (mousePosition.X-initialMousePosition.X) > -45 && initialShapePosition.Y + (mousePosition.Y - initialMousePosition.Y) > 0)
                {
                    undoRedoController.AddAndExecute(new MoveShapeCommand(shape, mousePosition.X - initialMousePosition.X, mousePosition.Y - initialMousePosition.Y));
                }
                // Now that the Move Shape operation is over, the Shape is moved to the final position, 
                //  by using a MoveNodeCommand to move it.
                // The MoveNodeCommand is given the offset that it should be moved relative to its original position, 
                //  and with respect to the Undo/Redo functionality the Shape has only been moved once, with this Command.


                // The mouse is released, as the move operation is done, so it can be used by other controls.
                e.MouseDevice.Target.ReleaseMouseCapture();
     
        }

        private void MouseHoverShape(MouseEventArgs e)
        {
            var shape = Target(e);
            lastEnteredShape = shape;
            shape.Visibility = "Visible";

        }

        private void MouseExitShape(MouseEventArgs e)
        {
            lastEnteredShape.Visibility = "Hidden";
        }

        private void MouseDownPoint(MouseButtonEventArgs e)
        {

            if (!isAddingLine)
            {
                addingLineFrom = Target(e);
                isAddingLine = true;
            }else
            {
                if (addingLineFrom != Target(e))
                {
                    isAddingLine = false;
                    Model.Line line = new Model.Line() {From = addingLineFrom, To = Target(e) };
                    //System.Console.WriteLine( addingLineFrom.VerticalAlignment);

                    undoRedoController.AddAndExecute(new AddLineCommand(Lines, line));
                }
                
            }
            

            
        }
        private Model.Shape Target(MouseEventArgs e)
        {
            // Here the visual element that the mouse is captured by is retrieved.
            var shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
            // From the shapes visual element, the Shape object which is the DataContext is retrieved.

            return (Model.Shape)shapeVisualElement.DataContext;
        }

        private Ellipse TargetPoint(MouseEventArgs e)
        {
            // Here the visual element that the mouse is captured by is retrieved.
            var shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
            // From the shapes visual element, the Shape object which is the DataContext is retrieved.
           
           return (Ellipse)shapeVisualElement;
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
            
        }

        private bool CanRemoveLines(IList _edges) => _edges.Count >= 1;

        private void RemoveLines(IList _lines)
        {
            undoRedoController.AddAndExecute(new RemoveLinesCommand(Lines, _lines.Cast<Model.Line>().ToList()));
        }

        private void RemoveCircle()
        {
            System.Console.WriteLine("test2");
           // undoRedoController.AddAndExecute(new RemoveCircleCommand(Shapes));
        }
    }
}