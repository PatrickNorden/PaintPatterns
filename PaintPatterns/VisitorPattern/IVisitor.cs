using PaintPatterns.CompositePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintPatterns.VisitorPattern
{
    public interface IVisitor
    {
        public void VisitComposite(Composite composite);
    }
}
