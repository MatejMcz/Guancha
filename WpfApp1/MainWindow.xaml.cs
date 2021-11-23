using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using WpfApp1.Utilities;
using WpfApp1.Entities;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            EventManager.RegisterClassHandler(typeof(Window), Keyboard.KeyDownEvent, new KeyEventHandler(keyDown), true);
            EventManager.RegisterClassHandler(typeof(Window), Keyboard.KeyUpEvent, new KeyEventHandler(keyUp), true);
        }

        int height, width;
        WriteableBitmap writeableBmp;

        Entity player = new Entities.Entity()
        {
            Position = new int[] { 100, 100 },
            Facing = 0
        };

        Entity player2 = new Entity()
        {
            Position = new int[] { 200, 200 },
            Facing = 0
        };

        Entity enemy = new Entities.Entity()
        {
            Position = new int[] { 100, 100 },
            Facing = 0
        };

        int size = 5;

        int[] color = { 0, 0, 0 };

        Random random = new Random();


        private void skerkos_Loaded(object sender, RoutedEventArgs e)
        {
            width = (int)this.ViewportWrapper.ActualWidth;
            height = (int)this.ViewportWrapper.ActualHeight;
            writeableBmp = BitmapFactory.New(width, height);
            Viewport.Source = writeableBmp;

            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        bool l, r, u, d, w, a, s, pismenodoprava = false;
        int fastness = 5;
        

        private void keyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left: 
                    l = true;
                    break;
                case Key.Right: 
                    r = true;
                    break;
                case Key.Up:
                    u = true;
                    break;
                case Key.Down:
                    d = true;
                    break;
                case Key.A:
                    a = true;
                    break;
                case Key.D:
                    pismenodoprava = true;
                    break;
                case Key.W:
                    w = true;
                    break;
                case Key.S:
                    s = true;
                    break;
                default: break;
            }
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    l = false;
                    break;
                case Key.Right:
                    r = false;
                    break;
                case Key.Up:
                    u = false;
                    break;
                case Key.Down:
                    d = false;
                    break;
                case Key.A:
                    a = false;
                    break;
                case Key.D:
                    pismenodoprava = false;
                    break;
                case Key.W:
                    w = false;
                    break;
                case Key.S:
                    s = false;
                    break;
                case Key.Space:
                    int[] offset = WpfApp1.Utilities.Coords.RadToCart(90, 30);
                    enemy.Position[0] += offset[0];
                    enemy.Position[1] += offset[1];
                    break;
                default: break;
            }
        }

        

        private void skerkos_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        int go = 0;

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (l && u)
            {
                player.Position[0] -= Convert.ToInt32(Math.Sqrt((fastness * fastness)/2));
                player.Position[1] -= Convert.ToInt32(Math.Sqrt((fastness * fastness) / 2));
            }
            else if (l && d)
            {
                player.Position[0] -= Convert.ToInt32(Math.Sqrt((fastness * fastness) / 2));
                player.Position[1] += Convert.ToInt32(Math.Sqrt((fastness * fastness) / 2));
            }
            else if (r && u)
            {
                player.Position[0] += Convert.ToInt32(Math.Sqrt((fastness * fastness) / 2));
                player.Position[1] -= Convert.ToInt32(Math.Sqrt((fastness * fastness) / 2));
            }
            else if (r && d)
            {
                player.Position[0] += Convert.ToInt32(Math.Sqrt((fastness * fastness) / 2));
                player.Position[1] += Convert.ToInt32(Math.Sqrt((fastness * fastness) / 2));
            }
            else if (l)
            {
                player.Position[0] -= fastness;
            }
            else if (r)
            {
                player.Position[0] += fastness;
            }
            else if (d)
            {
                player.Position[1] += fastness;
            }
            else if (u)
            {
                player.Position[1] -= fastness;
            }

            if (a && w)
            {
                player2.Position[0] -= Convert.ToInt32(Math.Sqrt((fastness * fastness) / 2));
                player2.Position[1] -= Convert.ToInt32(Math.Sqrt((fastness * fastness) / 2));
            }
            else if (a && s)
            {
                player2.Position[0] -= Convert.ToInt32(Math.Sqrt((fastness * fastness) / 2));
                player2.Position[1] += Convert.ToInt32(Math.Sqrt((fastness * fastness) / 2));
            }
            else if (pismenodoprava && w)
            {
                player2.Position[0] += Convert.ToInt32(Math.Sqrt((fastness * fastness) / 2));
                player2.Position[1] -= Convert.ToInt32(Math.Sqrt((fastness * fastness) / 2));
            }
            else if (pismenodoprava && s)
            {
                player2.Position[0] += Convert.ToInt32(Math.Sqrt((fastness * fastness) / 2));
                player2.Position[1] += Convert.ToInt32(Math.Sqrt((fastness * fastness) / 2));
            }
            else if (a)
            {
                player2.Position[0] -= fastness;
            }
            else if (pismenodoprava)
            {
                player2.Position[0] += fastness;
            }
            else if (s)
            {
                player2.Position[1] += fastness;
            }
            else if (w)
            {
                player2.Position[1] -= fastness;
            }




            writeableBmp.Clear();
            
            writeableBmp.FillEllipseCentered(player.Position[0], player.Position[1], size, size, Color.FromRgb(Convert.ToByte(color[0]), Convert.ToByte(color[1]), Convert.ToByte(color[2])));
            writeableBmp.DrawEllipseCentered(player.Position[0], player.Position[1], size, size, Colors.Black);

            writeableBmp.FillEllipseCentered(player2.Position[0], player2.Position[1], size, size, Color.FromRgb(Convert.ToByte(color[0]), Convert.ToByte(color[1]), Convert.ToByte(color[2])));
            writeableBmp.DrawEllipseCentered(player2.Position[0], player2.Position[1], size, size, Colors.Black);

            writeableBmp.FillEllipseCentered(enemy.Position[0], enemy.Position[1], 25, 25, Colors.Red);

            int entityDistance = Coords.DistanceOfPoints(new int[] {player.Position[0], player.Position[1] }, new int[] { enemy.Position[0], enemy.Position[1] });

            if (entityDistance < 25 + size)
            {
                enemy.Position[0] = Convert.ToInt32(random.Next(600))+100;
                enemy.Position[1] = Convert.ToInt32(random.Next(250))+100;
                size++;
                color[0] = Convert.ToByte(random.Next(256));
                color[1] = Convert.ToByte(random.Next(256));
                color[2] = Convert.ToByte(random.Next(256));
            }
        }
    }
}
