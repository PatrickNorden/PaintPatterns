﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using PaintPatterns.CommandPattern;
using PaintPatterns.CompositePattern;
using ICommand = PaintPatterns.CommandPattern.ICommand;

namespace PaintPatterns
{
    public class CommandInvoker
    {
        private readonly Stack<ICommand> commandsDone = new Stack<ICommand>();
        private readonly Stack<ICommand> commandsUndone = new Stack<ICommand>();
        private static readonly CommandInvoker Instance = new CommandInvoker();
        public MainWindow MainWindow;

        private CommandInvoker() { }

        public void Init()
        {
            var cmd = new CommandInit();
            cmd.Execute();
        }

        /// <summary>
        /// Undo the last command that is on the commandsDone stack
        /// Push the command you want to undo to the commandsUndone stack
        /// </summary>
        public void Undo()
        {
            if (commandsDone.TryPop(out var cmd))
            {
                cmd.Undo();
                commandsUndone.Push(cmd);
            }
        }

        /// <summary>
        /// Redo the last command that is on the commandsUndone stack
        /// Push the command you want to undo to the commandsDone stack
        /// </summary>
        public void Redo()
        {
            if (commandsUndone.TryPop(out var cmd))
            {
                cmd.Redo();
                commandsDone.Push(cmd);
            }
        }

        /// <summary>
        /// Resize drawing
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Resize(Shape shape, MouseWheelEventArgs e)
        {
            var cmd = new CommandResize(shape, e);
            cmd.Execute();
            commandsDone.Push(cmd);
        }

        /// <summary>
        /// Start before moving the shape
        /// </summary>
        public void StartMove(Shape shape, Point pStart)
        {
            ICommand cmd = new CommandMove(shape, pStart, MainWindow);
            commandsDone.Push(cmd);
        }

        /// <summary>
        /// Move drawing
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Move(MouseEventArgs e)
        {
            var cmd = commandsDone.Pop();
            if (cmd is CommandMove cmdMove)
            {
                cmdMove.CurrMouseEventArgs = e;
                cmdMove.Execute();
                commandsDone.Push(cmdMove);
            }
        }

        /// <summary>
        /// Clear canvas
        /// </summary>
        public void Clear()
        {
            var cmd = new CommandClear();
            cmd.Execute();
            commandsDone.Push(cmd);
        }

        /// <summary>
        /// Get an instance
        /// </summary>
        /// <returns></returns>
        public static CommandInvoker GetInstance()
        {
            return Instance;
        }

        /// <summary>
        /// start drawing a shape from the original position to the given position the mouse is at
        /// </summary>
        /// <param name="p2"></param>
        public void Draw(System.Windows.Point p2)
        {
            var cmd = (CommandDraw)commandsDone.Pop();
            cmd.x2 = (int)Math.Round(p2.X);
            cmd.y2 = (int)Math.Round(p2.Y);
            cmd.Execute();
            commandsDone.Push(cmd);
        }

        /// <summary>
        /// Start the parent setting command with a clicked element
        /// </summary>
        /// <param name="e"></param>
        public void SetParent(MouseButtonEventArgs e)
        {
            var cmd = new CommandSetParent(e);
            cmd.Execute();
        }

        /// <summary>
        /// Start the composite adding command with a name, shape and its parent
        /// </summary>
        /// <param name="name"></param>
        /// <param name="shape"></param>
        /// <param name="parent"></param>
        public void AddComposite(string name, Shape shape, Component parent, Point beginP, Point endP)
        {
            var cmd = new CommandComposite(name, shape, parent, beginP, endP);
            cmd.Execute();
            commandsDone.Push(cmd);
        }

        /// <summary>
        /// When the current action is drawing and the canvas is clicked start with a drawing command
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="shape"></param>
        public void StartDraw(System.Windows.Point p1, Shape shape)
        {
            ICommand cmd = new CommandDraw(p1, shape);
            commandsDone.Push(cmd);
        }

        public void StartGroup(System.Windows.Point initPos, Shape selectRect)
        {
            var cmd = new CommandGroup(initPos, selectRect);
            commandsDone.Push(cmd);
        }

        public void Group(System.Windows.Point p2)
        {
            var cmd = (CommandGroup)commandsDone.Pop();
            cmd.endPosX = (int)Math.Round(p2.X);
            cmd.endPosY = (int)Math.Round(p2.Y);
            cmd.Execute();
            commandsDone.Push(cmd);
        }

        public void UpdateGroup(Composite composite)
        {
            var cmd = new CommandUpdatedGroup(composite);
        }

        public void UpdateShape(Composite composite)
        {
            var cmd = new CommandUpdatedShape(composite);
        }

        /// <summary>
        /// Return a random color
        /// </summary>
        /// <returns></returns>
        public static Brush RandColor()
        {
            Brush result = Brushes.Transparent;

            Random rnd = new Random();

            Type brushesType = typeof(Brushes);

            PropertyInfo[] properties = brushesType.GetProperties();

            int random = rnd.Next(properties.Length);
            result = (Brush)properties[random].GetValue(null, null);

            return result;
        }

        public void Export()
        {
            var cmd = new CommandExport();
            cmd.Execute();
        }
    }
}
