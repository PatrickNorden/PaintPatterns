using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Shapes;

namespace PaintPatterns.CompositePattern
{
    public class Composite : Component
    {
        List<Component> children = new List<Component>();
        System.Windows.Point initP, endP;

        public Composite(string name, Shape shape, Component parent, System.Windows.Point initP, System.Windows.Point endP)
            : base(name, shape, parent, initP, endP)
        {
            this.initP = initP;
            this.endP = endP;
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

        public override void SetPos(System.Windows.Point initP, System.Windows.Point endP)
        {
            this.initP = initP;
            this.endP = endP;
        }

        public override System.Windows.Point GetBeginPos()
        {
            return this.initP;
        }

        public override System.Windows.Point GetEndPos()
        {
            return this.endP;
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
