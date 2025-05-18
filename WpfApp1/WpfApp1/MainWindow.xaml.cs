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
        Rectangle Tank1;
        Rectangle Tank2;
        Rectangle Wall;
        List<Rectangle> Map = new List<Rectangle>();
        List<Rectangle> Tanks = new List<Rectangle>();
        Key TankLastKey1 = new Key();
        Key TankLastKey2 = new Key();

        public MainWindow()
        {
            InitializeComponent();
            Game();
        }

        void Game()
        {
            Tank1 = new Rectangle()
            {
                Width = 20, 
                Height = 20,
                Fill = Brushes.Green
            };
            Canvas.SetLeft(Tank1, 100);
            Canvas.SetTop(Tank1, 100);
            BattleCity.Children.Add(Tank1);
            Tank2 = new Rectangle()
            {
                Width = 20,
                Height = 20,
                Fill = Brushes.Green
            };
            Canvas.SetLeft(Tank2, 200);
            Canvas.SetTop(Tank2, 100);
            BattleCity.Children.Add(Tank2);
            for (int i = 0; i < 520; i +=20)
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
                    TankLastKey1 = e.Key;
                    MoveTank(-20, 0, Tank1);
                    break;
                case Key.D:
                    TankLastKey1 = e.Key;
                    MoveTank(20, 0, Tank1);
                    break;
                case Key.W:
                    TankLastKey1 = e.Key;
                    MoveTank(0, -20, Tank1);
                    break;
                case Key.S:
                    TankLastKey1 = e.Key;
                    MoveTank(0, 20, Tank1);
                    break;
                case Key.Left:
                    TankLastKey2 = e.Key;
                    MoveTank(-20, 0, Tank2);
                    break;
                case Key.Right:
                    TankLastKey2 = e.Key;
                    MoveTank(20, 0, Tank2);
                    break;
                case Key.Up:
                    TankLastKey2 = e.Key;
                    MoveTank(0, -20, Tank2);
                    break;
                case Key.Down:
                    TankLastKey2 = e.Key;
                    MoveTank(0, 20, Tank2);
                    break;
                case Key.Space:
                    ShotTank(Tank1);
                    break;
                case Key.LeftCtrl:
                    ShotTank(Tank2);
                    break;
            }
        }
        void ShotTank(Rectangle Tank)
        {
            bool FoundedAim = false;
            double Left = Canvas.GetLeft(Tank);
            double Top = Canvas.GetTop(Tank);
            for(double i = Top; i >= 0 && (TankLastKey1 == Key.W || TankLastKey2 == Key.Up); i-=20)
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
            for (double i = Top; i < 520 && (TankLastKey1 == Key.S || TankLastKey2 == Key.Down); i += 20)
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
            for (double i = Left; i >= 0 && (TankLastKey1 == Key.A || TankLastKey2 == Key.Left); i -= 20)
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
            for (double i = Top; i < 600 && (TankLastKey1 == Key.D || TankLastKey2 == Key.Right); i += 20)
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
        void MoveTank(int Movex, int Movey, Rectangle Tank)
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