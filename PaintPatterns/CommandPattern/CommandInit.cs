using System;

namespace PaintPatterns.CommandPattern
{
    internal class CommandInit : ICommand
    {
        private readonly CommandInvoker invoker;

        public CommandInit()
        {
            this.invoker = CommandInvoker.GetInstance();
        }

        public void Execute()
        {
            invoker.MainWindow.SelectBtn.IsEnabled = false;
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
