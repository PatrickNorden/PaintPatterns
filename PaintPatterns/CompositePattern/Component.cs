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
        protected Component parent;

        public Component(String name, Shape shape, Component parent)
        {
            this.name = name;
            this.shape = shape;
            this.parent = parent;
        }

        public abstract void AddChild(Component c);
        public abstract void RemoveChild(Component c);

        public abstract List<Component> GetChildren();

        public abstract Component GetParent();

        public abstract Shape GetShape();

        public abstract void Clear();
        public abstract void Display(int depth);
    }
}
