using PaintPatterns.CompositePattern;
using PaintPatterns.VisitorPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintPatterns.CommandPattern
{
    internal class CommandExport : ICommand
    {
        private readonly CommandInvoker invoker;
        private ExportVisitor exportVisitor;

        public CommandExport()
        {
            this.invoker = CommandInvoker.GetInstance();
            exportVisitor = new ExportVisitor();
        }
        public void Execute()
        {
            foreach(Composite composite in invoker.MainWindow.shapes)
            {
                composite.Accept(exportVisitor);
            }
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
