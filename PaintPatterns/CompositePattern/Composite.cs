using System;
using System.Collections.Generic;
using System.Windows.Shapes;

namespace PaintPatterns.CompositePattern
{
    public class Composite : Component
    {
        List<Component> children = new List<Component>();
        public Composite(string name, Shape shape, Component parent)
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

        public override void Clear()
        {
            children.Clear();
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

        /// <summary>
        /// Write the current parent-child structure of this composite in the output console.
        /// </summary>
        /// <param name="depth"></param>
        public override void Display(int depth)
        {
            System.Diagnostics.Debug.WriteLine(new string('-', depth) + name);
            foreach (Component component in children)
            {
                component.Display(depth + 2);
            }
        }
    }
}
