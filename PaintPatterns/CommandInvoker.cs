using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using PaintPatterns.CommandPattern;
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

        /// <summary>
        /// Inits the application
        /// </summary>
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
            throw new NotImplementedException();
            //var cmd = new CommandResize(shape, e);
            //cmd.Execute();
            //commandsDone.Push(cmd);
        }

        /// <summary>
        /// 
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
        /// Save canvas state
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Save()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Load canvas state
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Load()
        {
            throw new NotImplementedException();
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
        /// Update groups
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void UpdateGroups()
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Add groups
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void AddGroups()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get an instance
        /// </summary>
        /// <returns></returns>
        public static CommandInvoker GetInstance()
        {
            return Instance;
        }
        public void Draw(System.Windows.Point p2)
        {
            var cmd = (CommandDraw)commandsDone.Pop();
            cmd.x2 = (int) Math.Round(p2.X);
            cmd.y2 = (int)Math.Round(p2.Y);
            cmd.Execute();
            commandsDone.Push(cmd);
        }

        public void StartDraw(System.Windows.Point p1, Shape shape)
        {
            ICommand cmd = new CommandDraw(p1, shape);
            commandsDone.Push(cmd);
        }

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
    }
}
