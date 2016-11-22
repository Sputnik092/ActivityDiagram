namespace ActivityDiagram.Command
{
   public interface IUndoRedoCommand
    {
        #region Methods (that has to be implemented)
        void Execute();
        void UnExecute();

        #endregion
    }
}