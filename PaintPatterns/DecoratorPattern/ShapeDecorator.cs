using PaintPatterns.CompositePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintPatterns.DecoratorPattern
{
    public class ShapeDecorator : Decorator
    {
        public ShapeDecorator(Component component) : base(component)
        {
            
        }
    }
}
