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

            wall = new Rectangle()
            {
                Width = 20,
                Height = 20,
                Fill = Brushes.Brown,
            };
            Canvas.SetLeft(wall, 120);
            Canvas.SetTop(wall, 120);
            BattleCity.Children.Add(wall);
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
            Canvas.SetLeft(tank, left+movex);
            Canvas.SetTop(tank, top+movey);
        }
    }

}