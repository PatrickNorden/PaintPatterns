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
        Component parent;

        public CommandComposite(String name, Shape shape, Component parent)
        {
            this.invoker = CommandInvoker.GetInstance();
            this.name = name;
            this.shape = shape;
            this.parent = parent;
        }

        public void Execute()
        {
            newShape = new Composite(name, shape, parent);
            invoker.MainWindow.parent.AddChild(newShape);
        }

        public void Redo()
        {
            invoker.MainWindow.parent.AddChild(newShape);
        }

        public void Undo()
        {
            invoker.MainWindow.parent.RemoveChild(newShape);
        }
    }
}
