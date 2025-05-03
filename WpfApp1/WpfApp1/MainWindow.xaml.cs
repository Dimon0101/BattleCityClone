using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Rectangle tank;
        Rectangle wall;

        public MainWindow()
        {
            InitializeComponent();
            Game();
        }

        void Game()
        {
            tank = new Rectangle()
            {
                Width = 20, 
                Height = 20,
                Fill = Brushes.Green
            };
            Canvas.SetLeft(tank, 100);
            Canvas.SetTop(tank, 100);
            BattleCity.Children.Add(tank);

            
            for (int i = 0; i < 520; i += 20)
            {
                for (int j = 0; j < 600; j += 20)
                {
                    wall = new Rectangle()
                    {
                        Width = 20,
                        Height = 20,
                        Fill = Brushes.Brown,
                    };
                    if (i %40 == 0 && j %40 == 0)
                    {
                        Canvas.SetLeft(wall, j);
                        Canvas.SetTop(wall, i);
                    }
                    else
                    {
                        Canvas.SetLeft(wall, 0);
                    }
                    BattleCity.Children.Add(wall);
                }
            }

        }
        UIElement GetElementAt(double x, double y)
        {
            foreach (UIElement element in BattleCity.Children)
            {
                double left = Canvas.GetLeft(element);
                double top = Canvas.GetTop(element);
                double width = (element as FrameworkElement)?.ActualWidth ?? 0;
                double height = (element as FrameworkElement)?.ActualHeight ?? 0;

                if (x >= left && x < left + width &&
                    y >= top && y < top + height)
                {
                    return element;
                }
            }
            return null;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.A:
                    MoveTank(-20, 0);
                    break;
                case Key.D:
                    MoveTank(20, 0);
                    break;
                case Key.W:
                    MoveTank(0, -20);
                    break;
                case Key.S:
                    MoveTank(0, 20);
                    break;
            }
        }
        void MoveTank(int movex, int movey)
        {
            double left = Canvas.GetLeft(tank);
            double top = Canvas.GetTop(tank);
            var found = GetElementAt(left+movex, top+movey);
            if (found != null)
            {
            }
            else
            {
                Canvas.SetLeft(tank, left + movex);
                Canvas.SetTop(tank, top + movey);
            }
        }
    }

}