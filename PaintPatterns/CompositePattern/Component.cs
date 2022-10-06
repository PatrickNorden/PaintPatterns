using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace PaintPatterns.CompositePattern
{
    public abstract class Component
    {
        protected String name;
        protected Shape shape;

        public Component(String name, Shape shape)
        {
            this.name = name;
            this.shape = shape;
        }

        public abstract void Add(Component c);
        public abstract void Remove(Component c);
        public abstract void Display(int depth);
    }
}
