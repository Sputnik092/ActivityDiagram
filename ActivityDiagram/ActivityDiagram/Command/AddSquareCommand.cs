using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using ActivityDiagram.Model;

namespace ActivityDiagram.Command
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AddSquareCommand : IUndoRedoCommand
    {
        // Regions can be used to make code foldable (minus/plus sign to the left).
        #region Fields

        // The 'shapes' field holds the current collection of shapes, 
        //  and the reference points to the same collection as the one the MainViewModel point to, 
        //  therefore when this collection is changed in a object of this class, 
        //  it also changes the collection that the MainViewModel uses.
        // For a description of an ObservableCollection see the MainViewModel class.
        private ObservableCollection<Rectangle> rectangles;
        // The 'shape' field holds a new shape, that is added to the 'shapes' collection, 
        //  and if undone, it is removed from the collection.
        private Rectangle rectangle;

        #endregion

        #region Constructor

        // For changing the current state of the diagram.
        public AddSquareCommand(ObservableCollection<Rectangle> _rectangles, Rectangle _rectangle)
        {
            rectangles = _rectangles;
            rectangle = _rectangle;
        }

        #endregion

        #region Methods

        // For doing and redoing the command.
        public void Execute()
        {
            System.Console.WriteLine("Execute!");
            rectangles.Add(rectangle);
        }

        // For undoing the command.
        public void UnExecute()
        {
            rectangles.Remove(rectangle);
        }

        #endregion
    }
}