using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace PaintPatterns.CompositePattern
{
    public class Composite : Component
    {
        List<Component> children = new List<Component>();
        public Composite(String name, Shape shape, Component parent)
            : base(name, shape, parent)
        {
        }

        public override void AddChild(Component component)
        {
            children.Add(component);
        }

        public override void RemoveChild(Component component)
        {
            children.Remove(component);
        }

        public override List<Component> GetChildren()
        {
            return children;
        }

        public override Component GetParent()
        {
            return parent;
        }

        public override Shape GetShape()
        {
            return shape;
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + name);
            foreach (Component component in children)
            {
                component.Display(depth + 2);
            }
        }
    }
}
