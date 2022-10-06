using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using PaintPatterns.CompositePattern;

namespace PaintPatterns.CommandPattern
{
    internal class CommandComposite : ICommand
    {
        private readonly CommandInvoker invoker;
        String name;
        Shape shape;
        Composite newShape;

        public CommandComposite(String name, Shape shape)
        {
            this.invoker = CommandInvoker.GetInstance();
            this.name = name;
            this.shape = shape;
        }

        public void Execute()
        {
            newShape = new Composite(name, shape);
            invoker.MainWindow.root.Add(newShape);
        }

        public void Redo()
        {
            invoker.MainWindow.root.Add(newShape);
        }

        public void Undo()
        {
            invoker.MainWindow.root.Remove(newShape);
        }
    }
}
