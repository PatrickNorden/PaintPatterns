using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using PaintPatterns.CompositePattern;

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
            throw new NotImplementedException();
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
