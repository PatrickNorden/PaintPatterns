using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace PaintPatterns.CommandPattern
{
    internal class CommandMove : ICommand
    {
        private Point offset = new Point(0, 0);
        private readonly Shape shape;
        private readonly MainWindow mainWindow;
        private System.Drawing.Point oldP = new System.Drawing.Point(0,0);
        public MouseEventArgs CurrMouseEventArgs;
        public CommandMove(Shape shape, Point p2, MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.shape = shape;
            oldP.X = (int)MathF.Round((float)Canvas.GetLeft(shape));
            oldP.Y = (int)MathF.Round((float)Canvas.GetTop(shape));
            offset.X = (int)(p2.X - Canvas.GetLeft(shape));
            offset.Y = (int)(p2.Y - Canvas.GetTop(shape));
        }
        public void Execute()
        {
            Point absoluteP = CurrMouseEventArgs.GetPosition(mainWindow.Canvas);

            int x = Convert.ToInt32(absoluteP.X - offset.X);
            int y = Convert.ToInt32(absoluteP.Y - offset.Y);
            System.Drawing.Point newP = new System.Drawing.Point(x, y);
            mainWindow.SetCanvasOffset(newP, shape);
            
        }

        public void Redo()
        {
            System.Drawing.Point newP = oldP;
            oldP = new System.Drawing.Point((int)MathF.Round((float)Canvas.GetLeft(shape)), (int)MathF.Round((float)Canvas.GetTop(shape)));
            mainWindow.SetCanvasOffset(newP, shape);
        }

        public void Undo()
        {
            System.Drawing.Point newPos = oldP;
            oldP = new System.Drawing.Point((int)MathF.Round((float)Canvas.GetLeft(shape)), (int)MathF.Round((float)Canvas.GetTop(shape)));
            mainWindow.SetCanvasOffset(newPos, shape);
        }
    }
}
