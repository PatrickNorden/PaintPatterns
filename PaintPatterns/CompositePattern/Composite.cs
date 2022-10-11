using PaintPatterns.VisitorPattern;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Shapes;

namespace PaintPatterns.CompositePattern
{
    public class Composite : Component
    {
        private List<Component> children = new List<Component>();
        private System.Windows.Point initP, endP;
        private Shape shape;
        private string name;
        private Component parent;

        public override void setName(string name)
        {
            this.name = name;
        }

        public override void setShape(Shape shape)
        {
            this.shape = shape;
        }

        public override void setParent(Component parent)
        {
            this.parent = parent;
        }

        public override void setInitP(System.Windows.Point point)
        {
            this.initP = point;
        }

        public override void setEndP(System.Windows.Point point)
        {
            this.endP = point;
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

        public override void ClearShape()
        {
            this.shape.Stroke = System.Windows.Media.Brushes.Transparent;
            this.shape.Visibility = System.Windows.Visibility.Hidden;
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

        public override void Accept(IVisitor v)
        {
            v.VisitComposite(this);
        }
    }
}
