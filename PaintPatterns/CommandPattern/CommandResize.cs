﻿using System;
using System.Windows.Input;
using System.Windows.Shapes;

namespace PaintPatterns.CommandPattern
{
    internal class CommandResize : ICommand
    {
        private const double mp = 0.01;
        private Shape shape;
        private int mouseDelta;

        public CommandResize(Shape shape, MouseWheelEventArgs currMouseWheelEventArgs)
        {
            this.shape = shape;
            this.mouseDelta = -currMouseWheelEventArgs.Delta;
        }

        /// <summary>
        /// Check if the mousewheel was scrolled up or down and resize according to the amount scrolled
        /// </summary>
        public void Execute()
        {
            double factor = mouseDelta;
            if (Math.Sign(factor) != -1)
            {
                shape.Width *= factor * mp;
                shape.Height *= factor * mp;
            }
            else
            {
                shape.Width /= Math.Abs(factor) * mp;
                shape.Height /= Math.Abs(factor) * mp;
            }
        }

        public void Redo()
        {
            Undo();
        }
        /// <summary>
        /// just do opposite of what the last scroll action was
        /// </summary>
        public void Undo()
        {
            mouseDelta *= -1;
            Execute();
        }
    }
}
