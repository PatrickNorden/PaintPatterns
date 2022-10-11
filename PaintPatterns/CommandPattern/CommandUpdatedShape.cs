using PaintPatterns.CompositePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintPatterns.CommandPattern
{
    internal class CommandUpdatedShape : ICommand
    {
        Composite composite;
        private readonly CommandInvoker invoker;

        public CommandUpdatedShape(Composite composite)
        {
            this.composite = composite;
            this.invoker = CommandInvoker.GetInstance();
            invoker.MainWindow.root.AddChild(composite);
            foreach(Composite child in composite.GetChildren())
            {
                child.GetShape().Stroke = System.Windows.Media.Brushes.PeachPuff;
                child.GetShape().StrokeThickness = 5;
            }
            invoker.MainWindow.shapes.Add(composite);
            
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
