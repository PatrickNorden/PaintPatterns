using PaintPatterns.CompositePattern;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace PaintPatterns.CommandPattern
{
    internal class CommandGroup : ICommand
    {
        private readonly CommandInvoker invoker;
        private System.Windows.Point initialPos;
        public readonly int initPosX, initPosY;
        public int endPosX;
        public int endPosY;
        Shape shape;
        private Composite composite;
        public CommandGroup(System.Windows.Point initialPos, Shape selectRect)
        {
            this.invoker = CommandInvoker.GetInstance();
            this.initialPos = initialPos;
            this.initPosX = (int)Math.Round(initialPos.X);
            this.initPosY = (int)Math.Round(initialPos.Y);
            this.shape = selectRect;
            shape.Fill = System.Windows.Media.Brushes.Transparent;
            shape.Stroke = System.Windows.Media.Brushes.Black;
            invoker.MainWindow.Canvas.Children.Add(shape);
            invoker.MainWindow.selectBox = new Composite();
            invoker.MainWindow.selectBox.setName("group");
            invoker.MainWindow.selectBox.setShape(shape);
            invoker.MainWindow.selectBox.setInitP(this.initialPos);
            invoker.MainWindow.selectBox.setEndP(new System.Windows.Point(0, 0));
        }

        public void Execute()
        {
            int x = (int)Math.Min(initPosX, endPosX);
            int y = (int)Math.Min(initPosY, endPosY);

            int w = (int)Math.Max(initPosX, endPosX) - x;
            int h = (int)Math.Max(initPosY, endPosY) - y;

            System.Drawing.Point pos = new System.Drawing.Point(x, y);
            invoker.MainWindow.SetCanvasOffset(pos, shape);
            shape.Width = w;
            shape.Height = h;
            invoker.MainWindow.selectBox.SetPos(initialPos, new System.Windows.Point(endPosX, endPosY));
        }

        public void Redo()
        {
        }

        public void Undo()
        {
        }
    }
}
