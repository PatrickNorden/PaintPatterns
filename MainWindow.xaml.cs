using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesignPatterns
{
    public partial class MainWindow : Window
    {
        private string shape = "none";
        public Point InitialPosition;
        public Point SelectPos;
        private bool mouseButtonHeld;

        UIElement selectedElement = null;
        Shape selectedShape;

        Point firstPos;
        bool drawing = false;
        Shape shapeDrawing;

        public MainWindow()
        {
            InitializeComponent();
            SelectBtn.IsEnabled = false;
        }

        private void SelectBtn_Click(object sender, RoutedEventArgs e)
        {
            shape = "none";
            SelectBtn.IsEnabled = false;
            RectangleBtn.IsEnabled = true;
            EllipseBtn.IsEnabled = true;
        }
        private void RectangleBtn_Click(object sender, RoutedEventArgs e)
        {
            shape = "rectangle";
            SelectBtn.IsEnabled = true;
            RectangleBtn.IsEnabled = false;
            EllipseBtn.IsEnabled = true;
        }
        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            Canvas.Children.Clear();
        }

        private void EllipseBtn_Click(object sender, RoutedEventArgs e)
        {
            shape = "ellipse";
            SelectBtn.IsEnabled = true;
            RectangleBtn.IsEnabled = true;
            EllipseBtn.IsEnabled = false;
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                mouseButtonHeld = true;
                InitialPosition = e.GetPosition(Canvas);
                if (shape == "none")
                {
                    if (selectedElement != null)
                        draw.unselect(selectedElement);
                    if (e.Source != Canvas)
                    {
                        drawing = false;
                        selectedShape = e.Source as Shape;
                        selectedElement = e.Source as UIElement;
                        draw.select(selectedElement, e.GetPosition(Canvas), InitialPosition, Canvas);
                    }
                }
                else
                {
                    if (selectedElement != null)
                        draw.unselect(selectedElement);

                    selectedShape = null;
                    selectedElement = null;

                    if (drawing == false)
                    {
                        firstPos = InitialPosition;
                        drawing = true;
                        if (shape == "ellipse")
                        {
                            if (InitialPosition.X < firstPos.X && InitialPosition.Y > firstPos.Y)
                            {
                                shapeDrawing = draw.ellipse((int)InitialPosition.X, (int)firstPos.Y, (int)firstPos.X - (int)InitialPosition.X, (int)InitialPosition.Y - (int)firstPos.Y, Canvas);
                            }
                            else if (InitialPosition.X > firstPos.X && InitialPosition.Y < firstPos.Y)
                            {
                                shapeDrawing = draw.ellipse((int)firstPos.X, (int)InitialPosition.Y, (int)InitialPosition.X - (int)firstPos.X, (int)firstPos.Y - (int)InitialPosition.Y, Canvas);
                            }
                            else if (InitialPosition.X < firstPos.X && InitialPosition.Y < firstPos.Y)
                            {
                                shapeDrawing = draw.ellipse((int)InitialPosition.X, (int)InitialPosition.Y, (int)firstPos.X - (int)InitialPosition.X, (int)firstPos.Y - (int)InitialPosition.Y, Canvas);
                            }
                            else
                            {
                                shapeDrawing = draw.ellipse((int)firstPos.X, (int)firstPos.Y, (int)InitialPosition.X - (int)firstPos.X, (int)InitialPosition.Y - (int)firstPos.Y, Canvas);
                            }
                        }
                        else if (shape == "rectangle")
                        {
                            if (InitialPosition.X < firstPos.X && InitialPosition.Y > firstPos.Y)
                            {
                                shapeDrawing = draw.rectangle((int)InitialPosition.X, (int)firstPos.Y, (int)firstPos.X - (int)InitialPosition.X, (int)InitialPosition.Y - (int)firstPos.Y, Canvas);
                            }
                            else if (InitialPosition.X > firstPos.X && InitialPosition.Y < firstPos.Y)
                            {
                                shapeDrawing = draw.rectangle((int)firstPos.X, (int)InitialPosition.Y, (int)InitialPosition.X - (int)firstPos.X, (int)firstPos.Y - (int)InitialPosition.Y, Canvas);
                            }
                            else if (InitialPosition.X < firstPos.X && InitialPosition.Y < firstPos.Y)
                            {
                                shapeDrawing = draw.rectangle((int)InitialPosition.X, (int)InitialPosition.Y, (int)firstPos.X - (int)InitialPosition.X, (int)firstPos.Y - (int)InitialPosition.Y, Canvas);
                            }
                            else
                            {
                                shapeDrawing = draw.rectangle((int)firstPos.X, (int)firstPos.Y, (int)InitialPosition.X - (int)firstPos.X, (int)InitialPosition.Y - (int)firstPos.Y, Canvas);
                            }
                        }
                    }
                }
            }
            else if (Mouse.RightButton == MouseButtonState.Pressed)
            {
                mouseButtonHeld = true;
                InitialPosition = e.GetPosition(Canvas);
                if (shape == "none")
                {
                    if (selectedElement != null)
                        draw.unselect(selectedElement);
                    if (e.Source != Canvas)
                    {
                        drawing = true;
                        selectedShape = e.Source as Shape;
                        selectedElement = e.Source as UIElement;
                        draw.select(selectedElement, e.GetPosition(Canvas), InitialPosition, Canvas);
                    }
                }
            }
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseButtonHeld = false;
            firstPos = new Point();
            drawing = false;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (drawing)
                {
                    if (e.GetPosition(Canvas).X < firstPos.X && e.GetPosition(Canvas).Y > firstPos.Y)
                    {
                        shapeDrawing.SetValue(Canvas.LeftProperty, (double)e.GetPosition(Canvas).X);
                        shapeDrawing.Width = (int)firstPos.X - (int)e.GetPosition(Canvas).X;
                        shapeDrawing.Height = (int)e.GetPosition(Canvas).Y - (int)firstPos.Y;
                    }
                    else if (e.GetPosition(Canvas).X > firstPos.X && e.GetPosition(Canvas).Y < firstPos.Y)
                    {
                        shapeDrawing.SetValue(Canvas.TopProperty, (double)e.GetPosition(Canvas).Y);
                        shapeDrawing.Width = (int)e.GetPosition(Canvas).X - (int)firstPos.X;
                        shapeDrawing.Height = (int)firstPos.Y - (int)e.GetPosition(Canvas).Y;
                    }
                    else if (e.GetPosition(Canvas).X < firstPos.X && e.GetPosition(Canvas).Y < firstPos.Y)
                    {
                        shapeDrawing.SetValue(Canvas.LeftProperty, (double)e.GetPosition(Canvas).X);
                        shapeDrawing.SetValue(Canvas.TopProperty, (double)e.GetPosition(Canvas).Y);
                        shapeDrawing.Width = (int)firstPos.X - (int)e.GetPosition(Canvas).X;
                        shapeDrawing.Height = (int)firstPos.Y - (int)e.GetPosition(Canvas).Y;
                    }
                    else if (e.GetPosition(Canvas).X > firstPos.X && e.GetPosition(Canvas).Y > firstPos.Y)
                    {
                        shapeDrawing.Width = (int)e.GetPosition(Canvas).X - (int)firstPos.X;
                        shapeDrawing.Height = (int)e.GetPosition(Canvas).Y - (int)firstPos.Y;
                    }
                }
                else if (mouseButtonHeld && selectedElement != null)
                {
                    if (e.Source != Canvas)
                    {
                        Point position = Mouse.GetPosition(Canvas);
                        draw.move(selectedElement, e.GetPosition(Canvas));
                    }
                }
            }
            else if (Mouse.RightButton == MouseButtonState.Pressed && drawing)
            {
                if (shape == "none")
                {
                    draw.resize(selectedElement, e.GetPosition(Canvas), InitialPosition, Canvas);
                }
            }
        }

        public class draw
        {
            private static List<Shape> shapes = new List<Shape>();
            private static Point Diff;
            private static Point RelativePoint;


            public static Ellipse ellipse(int x, int y, int width, int height, Canvas cv)
            {
                Ellipse ellipse = new Ellipse()
                {
                    Width = width,
                    Height = height,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                    Fill = randColor()
                };

                cv.Children.Add(ellipse);
                ellipse.SetValue(Canvas.LeftProperty, (double)x);
                ellipse.SetValue(Canvas.TopProperty, (double)y);

                shapes.Add(ellipse);
                return ellipse;
            }

            public static Rectangle rectangle(int x, int y, int width, int height, Canvas cv)
            {
                Rectangle rectangle = new Rectangle()
                {
                    Width = width,
                    Height = height,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                    Fill = randColor()
                };

                cv.Children.Add(rectangle);
                rectangle.SetValue(Canvas.LeftProperty, (double)x);
                rectangle.SetValue(Canvas.TopProperty, (double)y);

                shapes.Add(rectangle);
                return rectangle;
            }

            public static void move(UIElement selectedElement, Point getPosition)
            {
                selectedElement.SetValue(Canvas.LeftProperty, (double)getPosition.X - Diff.X);
                selectedElement.SetValue(Canvas.TopProperty, (double)getPosition.Y - Diff.Y);
            }

            public static void select(UIElement selectedElement, Point getPosition, Point InitialPosition, Canvas canvas)
            {
                Point relativePoint = selectedElement.TransformToAncestor(canvas).Transform(new Point(0, 0));
                Diff.X = InitialPosition.X - relativePoint.X;
                Diff.Y = InitialPosition.Y - relativePoint.Y;

                selectedElement.GetType().GetProperty("Stroke").SetValue(selectedElement, Brushes.Blue);
                RelativePoint = selectedElement.TransformToAncestor(canvas).Transform(new Point(0, 0));
            }

            public static void unselect(UIElement selectedElement)
            {
                selectedElement.GetType().GetProperty("Stroke").SetValue(selectedElement, Brushes.Black);
            }

            public static Brush randColor()
            {
                Brush result = Brushes.Transparent;

                Random rnd = new Random();

                Type brushesType = typeof(Brushes);

                PropertyInfo[] properties = brushesType.GetProperties();

                int random = rnd.Next(properties.Length);
                result = (Brush)properties[random].GetValue(null, null);

                return result;
            }
            public static void resize(UIElement selectedElement, Point getPosition, Point initialPosition, Canvas canvas)
            {
                if (getPosition.X < RelativePoint.X && getPosition.Y > RelativePoint.Y)
                {
                    selectedElement.SetValue(Canvas.LeftProperty, (double)getPosition.X);
                    selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)RelativePoint.X - getPosition.X);
                    selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)getPosition.Y - (int)RelativePoint.Y);
                }
                else if (getPosition.X > RelativePoint.X && getPosition.Y < RelativePoint.Y)
                {
                    selectedElement.SetValue(Canvas.TopProperty, (double)getPosition.Y);
                    selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)getPosition.X - (int)RelativePoint.X);
                    selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)RelativePoint.Y - (int)getPosition.Y);
                }
                else if (getPosition.X < RelativePoint.X && getPosition.Y < RelativePoint.Y)
                {
                    selectedElement.SetValue(Canvas.LeftProperty, (double)getPosition.X);
                    selectedElement.SetValue(Canvas.TopProperty, (double)getPosition.Y);
                    selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)RelativePoint.X - (int)getPosition.X);
                    selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)RelativePoint.Y - (int)getPosition.Y);
                }
                else if (getPosition.X > RelativePoint.X && getPosition.Y > RelativePoint.Y)
                {
                    selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)getPosition.X - (int)RelativePoint.X);
                    selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)getPosition.Y - (int)RelativePoint.Y);
                }
            }
        }
    }
}