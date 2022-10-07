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
        string name;
        Shape shape;
        Composite newShape;
        Component parent;

        public CommandComposite(string name, Shape shape, Component parent)
        {
            this.invoker = CommandInvoker.GetInstance();
            this.name = name;
            this.shape = shape;
            this.parent = parent;
        }

        /// <summary>
        /// Create a new composite with a name, shape and its parent
        /// </summary>
        public void Execute()
        {
            newShape = new Composite(name, shape, parent);
            parent.AddChild(newShape);
        }

        public void Redo()
        {

            parent.AddChild(newShape);
        }

        public void Undo()
        {
            parent.RemoveChild(newShape);
        }
    }
}
