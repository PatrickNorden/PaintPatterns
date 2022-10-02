using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace PaintPatterns
{
    public partial class MainWindow : Window
    {
        public string currentAction = "select";
        public Point initialPosition;
        private bool mouseButtonHeld;
        public Shape selected;

        private readonly CommandInvoker invoker;

        public MainWindow()
        {
            invoker = CommandInvoker.GetInstance();
            invoker.MainWindow = this;
        }

        #region Mouse button handling

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //invoker.Init();
            mouseButtonHeld = true;
            initialPosition = e.GetPosition(Canvas);
            if (currentAction != "rectangle" && currentAction != "ellipse") return;
            else if (currentAction == "rectangle")
            {
                invoker.StartDraw(initialPosition, new Rectangle());
            }
            else if(currentAction == "ellipse")
            {
                invoker.StartDraw(initialPosition, new Ellipse());
            }
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseButtonHeld = false;
        }

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
                    if (selected != null) invoker.Move(e);
                    break;
            }
        }

        private void Canvase_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (selected != null) invoker.Resize(selected, e);
        }
        #endregion

        #region Button Handling
        
        private void UndoBtn_Click(object sender, RoutedEventArgs e)
        {
            invoker.Undo();
        }

        private void RedoBtn_Click(object sender, RoutedEventArgs e)
        {
            invoker.Redo();
        }

        private void SelectBtn_Click(object sender, RoutedEventArgs e)
        {
            currentAction = "select";
        }

        private void RectangleBtn_Click(object sender, RoutedEventArgs e)
        {
            currentAction = "rectangle";
            //invoker.StartDraw(initialPosition, new Rectangle());
        }

        private void EllipseBtn_Click(object sender, RoutedEventArgs e)
        {
            currentAction = "ellipse";
            //invoker.StartDraw(initialPosition, new Ellipse());
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            invoker.Clear();
        }

        #endregion

        public void SetCanvasOffset(System.Drawing.Point offset, Shape shape)
        {
            Canvas.SetLeft(shape, offset.X);
            Canvas.SetTop(shape, offset.Y);
        }
    }
}