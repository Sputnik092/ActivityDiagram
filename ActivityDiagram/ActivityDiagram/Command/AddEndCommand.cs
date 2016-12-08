using ActivityDiagram.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityDiagram.Command
{
    public class AddEndCommand : IUndoRedoCommand
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
    private End end;

    #endregion

    #region Constructor


    public AddEndCommand(ObservableCollection<Shape> _shapes, End _end)
    {
        shapes = _shapes;
        end = _end;
    }

    #endregion

    #region Methods


    public void Execute()
    {
        System.Console.WriteLine("Execute!");
        shapes.Add(end);
    }


    public void UnExecute()
    {
        shapes.Remove(end);
    }

        #endregion
    }
}
