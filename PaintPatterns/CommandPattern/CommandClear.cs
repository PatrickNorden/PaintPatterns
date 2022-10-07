using System;

namespace PaintPatterns.CommandPattern
{
    internal class CommandClear : ICommand
    {
        private readonly CommandInvoker invoker;

        public CommandClear()
        {
            this.invoker = CommandInvoker.GetInstance();
        }

        /// <summary>
        /// Clear the canvas
        /// </summary>
        public void Execute()
        {
            invoker.MainWindow.Canvas.Children.Clear();
            invoker.MainWindow.root.Clear();
            invoker.MainWindow.parent = invoker.MainWindow.root;
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
