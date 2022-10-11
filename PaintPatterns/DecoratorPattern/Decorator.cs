using PaintPatterns.CompositePattern;
using PaintPatterns.VisitorPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace PaintPatterns.DecoratorPattern
{
    public class Decorator : Component
    {
        protected Component wrappee;
        public Decorator(Component component)
        {
            wrappee = component;
        }
        public override void Accept(IVisitor v)
        {
            wrappee.Accept(v);
        }

        public override void AddChild(Component c)
        {
            wrappee.AddChild(c);
        }

        public override void Clear()
        {
            wrappee.Clear();
        }

        public override void ClearShape()
        {
            wrappee.ClearShape();
        }

        public override void Display(int depth)
        {
            wrappee.Display(depth);
        }

        public override Point GetBeginPos()
        {
            return wrappee.GetBeginPos();
        }

        public override List<Component> GetChildren()
        {
            return wrappee.GetChildren();
        }

        public override Point GetEndPos()
        {
            return wrappee.GetEndPos();
        }

        public override Component GetParent()
        {
            return wrappee.GetParent();
        }

        public override Shape GetShape()
        {
            return wrappee.GetShape();
        }

        public override void RemoveChild(Component c)
        {
            wrappee.RemoveChild(c);
        }

        public override void setEndP(Point point)
        {
            wrappee.setEndP(point);
        }

        public override void setInitP(Point point)
        {
            wrappee.setInitP(point);
        }

        public override void setName(string name)
        {
            wrappee.setName(name);
        }

        public override void setParent(Component parent)
        {
            wrappee.setParent(parent);
        }

        public override void SetPos(Point initP, Point endP)
        {
            wrappee.SetPos(initP, endP);
        }

        public override void setShape(Shape shape)
        {
            wrappee.setShape(shape);
        }
    }
}
