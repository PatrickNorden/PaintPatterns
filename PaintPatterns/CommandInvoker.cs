using PaintPatterns.CommandPattern;
using System;
using System.Collections.Generic;
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
        public void Resize()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Move drawing
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Move()
        {
            throw new NotImplementedException();
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
    }
}
