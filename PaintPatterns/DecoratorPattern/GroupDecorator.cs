using PaintPatterns.CompositePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintPatterns.DecoratorPattern
{
    internal class GroupDecorator : Decorator
    {
        public GroupDecorator(Component component) : base(component)
        {
        }
    }
}
