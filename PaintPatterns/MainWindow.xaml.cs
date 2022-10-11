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
        public bool mouseButtonHeld;
        public Shape selected;
        public Composite root = new Composite();
        public Composite shape;
        public Component parent;
        public Composite selectBox;
        public List<Composite> groups = new List<Composite>();
        public List<Composite> shapes = new List<Composite>();


        private readonly CommandInvoker invoker;

        public MainWindow()
        {
            invoker = CommandInvoker.GetInstance();
            invoker.MainWindow = this;
            root.setName("root");
            parent = root;
            
        }

        #region Mouse button handling
        /// <summary>
        /// When the mouse button is being pressed down
        /// Register that the mousebutton is being held
        /// set the initial position to the position that has been clicked
        /// if the current action is a rectangle start drawing a rectangle
        /// if the currenct action is parent start the parent selection proces.
        /// if the current action is group start the grouping proces.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseButtonHeld = true;
            initialPosition = e.GetPosition(Canvas);
            if (currentAction == "rectangle")
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
            else if (currentAction == "group" )
            {
                invoker.StartGroup(initialPosition, new Rectangle());
            }
        }

        /// <summary>
        /// Register when the mouse button is not being pressed 
        /// after grouping update the shapes into this group
        /// after drawing update the shapes with their information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseButtonHeld = false;
            invoker.Init();
            if(currentAction == "group")
            {
                invoker.UpdateGroup(root);
            }
            if(currentAction == "ellipse" || currentAction == "rectangle")
            {
                invoker.UpdateShape(shape);
            }
        }

        /// <summary>
        /// When the mouse is moving, check if the mouse is being held
        /// If the current action is drawing a shape, draw this shape
        /// If the current action is selecting, Move the shape
        /// If the current action is group, draw a selectbox in the screen
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
                case "group":
                    invoker.Group(e.GetPosition(Canvas));
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
            invoker.Init();
        }

        /// <summary>
        /// When the redo button is clicked, redo the last undone action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RedoBtn_Click(object sender, RoutedEventArgs e)
        {
            invoker.Redo();
            invoker.Init();
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

        /// <summary>
        /// Set the current action to parent to make it able to select a parent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParentBtn_Click(object sender, RoutedEventArgs e)
        {
            currentAction = "parent";
        }

        private void group_Click(object sender, RoutedEventArgs e)
        {
            currentAction = "group";
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

        private void exportBtn_Click(object sender, RoutedEventArgs e)
        {
            invoker.Export();
        }
    }
}