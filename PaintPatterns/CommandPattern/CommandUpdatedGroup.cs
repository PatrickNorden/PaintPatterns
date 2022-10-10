using PaintPatterns.CompositePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintPatterns.CommandPattern
{
    internal class CommandUpdatedGroup : ICommand
    {
        Composite composite;
        private readonly CommandInvoker invoker;
        public CommandUpdatedGroup(Composite composite)
        {
            this.composite = composite;
            this.invoker = CommandInvoker.GetInstance();
            GetComponents(composite);

        }

        private void GetComponents(Component component)
        {
            foreach (Component instance in component.GetChildren())
            {
                if (instance.GetChildren().Count() > 0)
                {
                    GetComponents(instance);
                }
                if (true)
                {
                    invoker.MainWindow.selectBox.AddChild(instance);
                }
            }
            invoker.MainWindow.selectBox.Display(0);
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
