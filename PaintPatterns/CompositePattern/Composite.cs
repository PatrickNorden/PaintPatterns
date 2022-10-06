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
        public Composite(String name, Shape shape)
            : base(name, shape)
        {
        }

        public override void Add(Component component)
        {
            children.Add(component);
        }

        public override void Remove(Component component)
        {
            children.Remove(component);
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
