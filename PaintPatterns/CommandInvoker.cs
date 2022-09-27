using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaintPatterns.CommandPattern;
using ICommand = PaintPatterns.CommandPattern.ICommand;

namespace PaintPatterns
{
    public class CommandInvoker
    {
        private readonly Stack<ICommand> commandsDone = new Stack<ICommand>();
        private readonly Stack<ICommand> commandsUndone = new Stack<ICommand>();
        private static readonly CommandInvoker Instance = new CommandInvoker();

        private CommandInvoker() { }

        public void Undo()
        {
            throw new NotImplementedException();
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void InitApp()
        {
            throw new NotImplementedException();
        }

        public void Resize()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Load()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void UpdateGroups()
        {
            throw new NotImplementedException();
        }

        public void AddGroups()
        {
            throw new NotImplementedException();
        }

        public static CommandInvoker GetInstance()
        {
            return Instance;
        }
    }
}
