using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityDiagram.Command
{
    public class UndoRedoController
    {
        #region Fields

        private readonly Stack<IUndoRedoCommand> undoStack = new Stack<IUndoRedoCommand>();

        private readonly Stack<IUndoRedoCommand> redoStack = new Stack<IUndoRedoCommand>();
        #endregion

        #region Properties

        public static UndoRedoController Instance { get; } = new UndoRedoController();

        #endregion

        #region Constructor

        private UndoRedoController() { }

        #endregion

        #region Method

        public void AddAndExecute(IUndoRedoCommand command)
        {
            undoStack.Push(command);
            redoStack.Clear();
            command.Execute();
        }

        public void ClearAndExecute(IUndoRedoCommand command)
        {
            undoStack.Clear();
            redoStack.Clear();
            command.Execute();
        }

        public bool CanUndo() => undoStack.Any();

        public void Undo()
        {
            if (!undoStack.Any()) throw new InvalidOperationException();
            // This uses 'var' which is an implicit type variable (https://msdn.microsoft.com/en-us/library/bb383973.aspx).
            var command = undoStack.Pop();
            redoStack.Push(command);
            command.UnExecute();
        }

        public bool CanRedo() => redoStack.Any();
        public void Redo()
        {
            if (!redoStack.Any()) throw new InvalidOperationException();
            // This uses 'var' which is an implicit type variable (https://msdn.microsoft.com/en-us/library/bb383973.aspx).
            var command = redoStack.Pop();
            undoStack.Push(command);
            command.Execute();
        }


        #endregion

    }
}
