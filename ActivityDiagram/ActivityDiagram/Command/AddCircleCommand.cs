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

    public class AddCircleCommand : IUndoRedoCommand
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
        private Circle circle;

        #endregion

        #region Constructor


        public AddCircleCommand(ObservableCollection<Shape> _shapes, Circle _circle)
        {
            shapes = _shapes;
            circle = _circle;
        }

        #endregion

        #region Methods


        public void Execute()
        {
            System.Console.WriteLine("Execute!");
            shapes.Add(circle);
        }


        public void UnExecute()
        {
            shapes.Remove(circle);
        }

        #endregion
    }
}