using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PaintPatterns.CommandPattern
{
    internal class CommandDraw : ICommand
    {
        private readonly CommandInvoker invoker;
        private readonly int x1, y1;
        public int x2, y2;
        Shape shape;

        public CommandDraw(System.Windows.Point p1, Shape shape)
        {
            this.x1 = (int)Math.Round(p1.X);
            this.y1 = (int)Math.Round(p1.Y);
            this.invoker = CommandInvoker.GetInstance();
            this.shape = shape;
            shape.MouseDown += Select;
            shape.Stroke = shape.Fill = CommandInvoker.RandColor();
            shape.StrokeThickness = 3;
            invoker.MainWindow.Canvas.Children.Add(shape);
        }

        public void Select(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is Shape) || invoker.MainWindow.currentAction != "select") return;
            throw new NotImplementedException();

        }
        public void Execute()
        {
            int x = (int)Math.Min(x1, x2);
            int y = (int)Math.Min(y1, y2);

            int w = (int)Math.Max(x1, x2) - x;
            int h = (int)Math.Max(y1, y2) - y;

            System.Drawing.Point pos = new System.Drawing.Point(x, y);
            invoker.MainWindow.SetCanvasOffset(pos, shape);
            shape.Width = w;
            shape.Height = h;
        }

        public void Redo()
        {
            invoker.MainWindow.Canvas.Children.Add(shape);
        }

        public void Undo()
        {
            invoker.MainWindow.Canvas.Children.Remove(shape);
        }
    }
}

