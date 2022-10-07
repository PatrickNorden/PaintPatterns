﻿using PaintPatterns.CompositePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PaintPatterns.CommandPattern
{
    internal class CommandSetParent : ICommand
    {
        private readonly CommandInvoker invoker;
        private MouseButtonEventArgs e;
        private Component parent;
        public CommandSetParent(MouseButtonEventArgs e)
        {
            this.invoker = CommandInvoker.GetInstance();
            this.e = e;
            this.parent = invoker.MainWindow.parent;
        }

        private void setParent(Composite child)
        {
            if(child.GetChildren().Count() > 0)
            {
                foreach(Composite under in child.GetChildren())
                {
                    setParent(under);
                }
            }
            if (e.Source == child.GetShape())
            {
                invoker.MainWindow.parent = child;
            }
        }
        public void Execute()
        {
            foreach (Composite child in invoker.MainWindow.root.GetChildren())
            {
                setParent(child);
            }
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}