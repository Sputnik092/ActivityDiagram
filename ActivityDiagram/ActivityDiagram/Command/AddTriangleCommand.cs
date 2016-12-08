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

    public class AddTriangleCommand : IUndoRedoCommand
    {
        // Regions can be used to make code foldable (minus/plus sign to the left).
        #region Fields

        // The 'shapes' field holds the current collection of shapes, 
        //  and the reference points to the same collection as the one the MainViewModel point to, 
        //  therefore when this collection is changed in a object of this class, 
        //  it also changes the collection that the MainViewModel uses.
        // For a description of an ObservableCollection see the MainViewModel class.
        private ObservableCollection<Shape> shapes;
        // The 'shape' field holds a new shape, that is added to the 'shapes' collection, 
        //  and if undone, it is removed from the collection.
        private Triangle triangle;

        #endregion

        #region Constructor


        public AddTriangleCommand(ObservableCollection<Shape> _shapes, Triangle _triangle)
        {
            shapes = _shapes;
            triangle = _triangle;
        }

        #endregion

        #region Methods


        public void Execute()
        {
            System.Console.WriteLine("Execute!");
            shapes.Add(triangle);
        }


        public void UnExecute()
        {
            shapes.Remove(triangle);
        }

        #endregion
    }
}