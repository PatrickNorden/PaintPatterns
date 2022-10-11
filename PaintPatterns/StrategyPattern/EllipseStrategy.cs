using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace PaintPatterns.StrategyPattern
{
    internal class EllipseStrategy : IStrategy
    {
        public void Execute()
        {
            CommandInvoker.GetInstance().StartDraw(CommandInvoker.GetInstance().MainWindow.initialPosition, new Ellipse());
        }
    }
}
