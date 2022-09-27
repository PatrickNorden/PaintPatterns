using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
