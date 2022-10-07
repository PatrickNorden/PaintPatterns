using System;
using System.Collections.Generic;
using System.Text;

namespace PaintPatterns.CommandPattern
{
    interface ICommand
    {
        /// <summary>
        /// Undo last action
        /// </summary>
        public void Undo();

        /// <summary>
        /// Redo last action
        /// </summary>
        public void Redo();

        /// <summary>
        /// Execute command
        /// </summary>
        public void Execute();
    }
}
