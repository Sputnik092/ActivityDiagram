﻿using ActivityDiagram.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityDiagram.Command
{
    // Undo/Redo command for moving a Shape.
    public class MoveCircleCommand : IUndoRedoCommand
    {
        // Regions can be used to make code foldable (minus/plus sign to the left).
        #region Fields

        // The 'shape' field holds an existing shape, 
        //  and the reference points to the same object, 
        //  as one of the objects in the MainViewModels 'Shapes' ObservableCollection.
        // This shape is moved by changing its coordinates (X and Y), 
        //  and if undone the coordinates are changed back to the original coordinates.
        private Circle circle;

        // The 'offsetX' field holds the offset (difference) between the original and final X coordinate.
        private double offsetX;
        // The 'offsetY' field holds the offset (difference) between the original and final Y coordinate.
        private double offsetY;

        #endregion

        #region Constructor

        // For changing the current state of the diagram.
        public MoveCircleCommand(Circle _circle, double _offsetX, double _offsetY)
        {
            circle = _circle;
            offsetX = _offsetX;
            offsetY = _offsetY;
        }

        #endregion

        #region Methods

        // For doing and redoing the command.
        public void Execute()
        {
            circle.CanvasCenterX += offsetX;
            circle.CanvasCenterY += offsetY;
        }

        // For undoing the command.
        public void UnExecute()
        {
            circle.CanvasCenterX -= offsetX;
            circle.CanvasCenterY -= offsetY;
        }

        #endregion
    }
}
