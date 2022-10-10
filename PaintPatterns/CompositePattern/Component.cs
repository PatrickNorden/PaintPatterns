using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace PaintPatterns.CompositePattern
{
    public abstract class Component
    {
        protected string name;
        protected Shape shape;
        protected Component parent;
        protected System.Windows.Point initP, endP;

        public Component(string name, Shape shape, Component parent, System.Windows.Point initP, System.Windows.Point endP)
        {
            this.name = name;
            this.shape = shape;
            this.parent = parent;
            this.initP = initP;
            this.endP = endP;
        }

        public abstract void AddChild(Component c);
        public abstract void RemoveChild(Component c);

        public abstract List<Component> GetChildren();

        public abstract Component GetParent();

        public abstract Shape GetShape();

        public abstract void Clear();
        public abstract void Display(int depth);

        public abstract void SetPos(System.Windows.Point initP, System.Windows.Point endP);

        public abstract System.Windows.Point GetBeginPos();

        public abstract System.Windows.Point GetEndPos();
    }
}
