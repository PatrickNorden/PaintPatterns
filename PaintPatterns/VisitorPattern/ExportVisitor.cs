using PaintPatterns.CompositePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintPatterns.VisitorPattern
{
    internal class ExportVisitor : IVisitor
    {
        public void VisitComposite(Composite composite)
        {
            composite.GetChildren();
            composite.GetEndPos();
            composite.GetBeginPos();
            composite.GetParent();
            composite.GetShape();
            System.Diagnostics.Debug.WriteLine("Visited composite");
        }
    }
}
