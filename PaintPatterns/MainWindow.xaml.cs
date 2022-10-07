using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Shapes;
using PaintPatterns.CompositePattern;

namespace PaintPatterns
{
    public partial class MainWindow : Window
    {
        public string currentAction = "select";
        public Point initialPosition;
        private bool mouseButtonHeld;
        public Shape selected;
        public Composite root = new Composite("root", null, null);
        public Component parent;

        private readonly CommandInvoker invoker;

        public MainWindow()
        {
            invoker = CommandInvoker.GetInstance();
            invoker.MainWindow = this;
            parent = root;
        }

        #region Mouse button handling
        /// <summary>
        /// When the mouse button is being pressed down
        /// Register that the mousebutton is being held
        /// set the initial position to the position that has been clicked
        /// if the current action is a rectangle start drawing a rectangle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseButtonHeld = true;
            initialPosition = e.GetPosition(Canvas);
            if (currentAction != "rectangle" && currentAction != "ellipse" && currentAction !="parent") return;
            else if (currentAction == "rectangle")
            {
                invoker.StartDraw(initialPosition, new Rectangle());
            }
            else if (currentAction == "ellipse")
            {
                invoker.StartDraw(initialPosition, new Ellipse());
            }
            else if (currentAction == "parent")
            {
                invoker.SetParent(e);
            }
        }

        /// <summary>
        /// Register when the mouse button is not being pressed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseButtonHeld = false;
        }

        /// <summary>
        /// When the mouse is moving, check if the mouse is being held
        /// If the current action is drawing a shape, draw this shape
        /// If the current action is selecting, Move the shape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseButtonHeld) return;
            switch (currentAction)
            {
                case "ellipse":
                case "rectangle":
                    invoker.Draw(e.GetPosition(Canvas));
                    break;
                case "select":
                    if (selected != null)
                    {
                        invoker.Move(e);
                    }
                    break;
            }
        }

        /// <summary>
        /// If hte mousewheel is being turned, resize the shape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (selected != null) invoker.Resize(selected, e);
        }
        #endregion

        #region Button Handling
        /// <summary>
        /// When the undo button is clicked, undo the last action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UndoBtn_Click(object sender, RoutedEventArgs e)
        {
            invoker.Undo();
        }

        /// <summary>
        /// When the redo button is clicked, redo the last undone action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RedoBtn_Click(object sender, RoutedEventArgs e)
        {
            invoker.Redo();
        }

        /// <summary>
        /// When the select button is clicked, set the current action to "select"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectBtn_Click(object sender, RoutedEventArgs e)
        {
            currentAction = "select";
        }

        /// <summary>
        /// When the rectangle button is clicked, set current action to the shape name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RectangleBtn_Click(object sender, RoutedEventArgs e)
        {
            Button s = (Button)sender;
            currentAction = s.Name;
            if (selected == null) return;
            selected = null;
        }

        /// <summary>
        /// When the ellipse button is clicked, set current action to the shape name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EllipseBtn_Click(object sender, RoutedEventArgs e)
        {
            Button s = (Button)sender;
            currentAction = s.Name;
            if (selected == null) return;
            selected = null;
        }

        /// <summary>
        /// when the clear button is clicked, clear the canvas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            invoker.Clear();
        }

        private void ParentBtn_Click(object sender, RoutedEventArgs e)
        {
            currentAction = "parent";
        }

        #endregion

        /// <summary>
        /// Correct the offset of a shape on the canvas
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="shape"></param>
        public void SetCanvasOffset(System.Drawing.Point offset, Shape shape)
        {
            Canvas.SetLeft(shape, offset.X);
            Canvas.SetTop(shape, offset.Y);
        }
    }
}