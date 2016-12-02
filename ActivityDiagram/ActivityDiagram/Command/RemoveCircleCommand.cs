using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using ActivityDiagram.Model;
using System.Collections.Generic;

namespace ActivityDiagram.Command
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>

    public class RemoveCircleCommand : IUndoRedoCommand
    {
        // Regions can be used to make code foldable (minus/plus sign to the left).
        #region Fields

        // The 'shapes' field holds the current collection of shapes, 
        //  and the reference points to the same collection as the one the MainViewModel point to, 
        //  therefore when this collection is changed in a object of this class, 
        //  it also changes the collection that the MainViewModel uses.
        // For a description of an ObservableCollection see the MainViewModel class.
        private ObservableCollection<Circle> circles;
        // The 'shape' field holds a new shape, that is added to the 'shapes' collection, 
        //  and if undone, it is removed from the collection.

        #endregion

        #region Constructor


        public RemoveCircleCommand(ObservableCollection<Circle> _circles)
        {
            circles = _circles;
        }

        #endregion

        #region Methods


        public void Execute()
        {
            System.Console.WriteLine("test");
            List<int> toBeDeleted = new List<int>();
            for (int i = 0; i < circles.Count; i++)
            {
                
                if (circles[i].IsSelected == true)
                {
                    toBeDeleted.Add(i);
                }
            }
            for (int i = toBeDeleted.Count; i > 0; i++)
            {
                circles.RemoveAt(i);
            }

        }


        public void UnExecute()
        {
        }

        #endregion
    }
}