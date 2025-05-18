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
        Rectangle Tank;
        Rectangle Wall;
        List<Rectangle> Map = new List<Rectangle>();
        Key LastKey = new Key();
        Rectangle DeletedeObject = new Rectangle()
        {
            Width = 20,
            Height = 20,
        };

        public MainWindow()
        {
            InitializeComponent();
            Game();
        }

        void Game()
        {
            Tank = new Rectangle()
            {
                Width = 20, 
                Height = 20,
                Fill = Brushes.Green
            };
            Canvas.SetLeft(Tank, 100);
            Canvas.SetTop(Tank, 100);
            BattleCity.Children.Add(Tank);
            for(int i = 0; i < 520; i +=20)
            {
                for (int j = 0; j < 600; j += 20)
                {
                    Wall = new Rectangle()
                    {
                        Width = 20,
                        Height = 20,
                        Fill = Brushes.Red,
                    };
                    if (i % 40 == 0 && j % 40 == 0)
                    {
                        Map.Add(Wall);
                        Canvas.SetLeft(Wall, j);
                        Canvas.SetTop(Wall, i);
                        BattleCity.Children.Add(Wall);
                    }
                }
            }

        }
        bool GetElementAt(double NextTankPoseX, double NextTankPoseY)
        {
            foreach (Rectangle Element in Map)
            {
                double Left = Canvas.GetLeft(Element);
                double Top = Canvas.GetTop(Element);
                if (NextTankPoseX == Left && NextTankPoseY == Top)
                {
                    return true;
                }
            }
            return false;
        }
        private void Window_KeyDown(object Sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.A:
                    LastKey = e.Key;
                    MoveTank(-20, 0);
                    break;
                case Key.D:
                    LastKey = e.Key;
                    MoveTank(20, 0);
                    break;
                case Key.W:
                    LastKey = e.Key;
                    MoveTank(0, -20);
                    break;
                case Key.S:
                    LastKey = e.Key;
                    MoveTank(0, 20);
                    break;
                case Key.Space:
                    ShotTank();
                    break;
            }
        }
        void ShotTank()
        {
            bool FoundedAim = false;
            double Left = Canvas.GetLeft(Tank);
            double Top = Canvas.GetTop(Tank);
            for(double i = Top; i >= 0 && LastKey == Key.W; i-=20)
            {
                foreach (Rectangle Element in Map)
                {
                    double LeftObject = Canvas.GetLeft(Element);
                    double TopObject = Canvas.GetTop(Element);
                    if (LeftObject == Left && TopObject == i)
                    {
                        Element.Fill = null;
                        Map.Remove(Element);
                        FoundedAim = true;
                        break;
                    }
                }
                if (FoundedAim)
                {
                    break;
                }
            }
            for (double i = Top; i < 520 && LastKey == Key.S; i += 20)
            {
                foreach (Rectangle Element in Map)
                {
                    double LeftObject = Canvas.GetLeft(Element);
                    double TopObject = Canvas.GetTop(Element);
                    if (LeftObject == Left && TopObject == i)
                    {
                        Element.Fill = null;
                        Map.Remove(Element);
                        FoundedAim = true;
                        break;
                    }
                }
                if(FoundedAim)
                {
                    break;
                }
            }
            for (double i = Left; i >= 0 && LastKey == Key.A; i -= 20)
            {
                foreach (Rectangle Element in Map)
                {
                    double LeftObject = Canvas.GetLeft(Element);
                    double TopObject = Canvas.GetTop(Element);
                    if (LeftObject == i && TopObject == Top)
                    {
                        Element.Fill = null;
                        Map.Remove(Element);
                        FoundedAim = true;
                        break;
                    }
                }
                if(FoundedAim)
                {
                    break;
                }
            }
            for (double i = Top; i < 600 && LastKey == Key.D; i += 20)
            {
                foreach (Rectangle Element in Map)
                {
                    double LeftObject = Canvas.GetLeft(Element);
                    double TopObject = Canvas.GetTop(Element);
                    if (LeftObject == i && TopObject == Top)
                    {
                        Element.Fill = null;
                        Map.Remove(Element);
                        FoundedAim = true;
                        break;
                    }
                }
                if(FoundedAim)
                {
                    break;
                }
            }
        }
        void MoveTank(int Movex, int Movey)
        {
            double Left = Canvas.GetLeft(Tank);
            double Top = Canvas.GetTop(Tank);
            double Nextx = Left + Movex;
            double Nexty = Top + Movey;
            bool IsNextPoseFree = GetElementAt(Nextx, Nexty);
            if (!IsNextPoseFree && Nextx >=0 && Nexty >= 0 && Nextx <= 600 && Nexty <= 520)
            {
                Canvas.SetLeft(Tank, Nextx);
                Canvas.SetTop(Tank, Nexty);
            }
        }
    }
}