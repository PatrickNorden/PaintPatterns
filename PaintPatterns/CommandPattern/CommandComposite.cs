using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        int beginX, beginY;
        int endX, endY;
        System.Windows.Point beginP;
        System.Windows.Point endP;

        public CommandComposite(string name, Shape shape, Component parent, Point beginP, Point endP)
        {
            this.invoker = CommandInvoker.GetInstance();
            this.name = name;
            this.shape = shape;
            this.parent = parent;
            this.beginX = (int)Math.Round(beginP.X);
            this.beginY = (int)Math.Round(beginP.Y);
            this.endX = (int)Math.Round(endP.X);
            this.endY = (int)Math.Round(endP.Y);
            this.beginP = beginP;
            this.endP = endP;
        }

        /// <summary>
        /// Create a new composite with a name, shape and its parent
        /// </summary>
        public void Execute()
        {
            if(parent != null)
            {
                newShape = new Composite(name, shape, parent, beginP, endP);
                parent.AddChild(newShape);
            }
        }

        public void Redo()
        {
            if (parent != null)
            {
                parent.AddChild(newShape);
            }
        }

        public void Undo()
        {
            if(parent != null)
            {
                parent.RemoveChild(newShape);
            }
        }
    }
}
