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
        HashSet<Key> pressed_keys = new HashSet<Key>();
        HashSet<Player> players = new HashSet<Player> {
            new Player()
            {
            Position = new int[] { 100, 100 },
            Facing = 0,
            Size = 5,
            Controls = new Key[] { Key.Up, Key.Left, Key.Down, Key.Right },
            Speed = 5,
            Color = new int[] {0,0,0}
            },

            new Player()
            {
            Position = new int[] { 100, 100 },
            Facing = 0,
            Size = 5,
            Controls = new Key[] { Key.W, Key.A, Key.S, Key.D },
            Speed = 5,
            Color = new int[] {0,0,0}
            }
        };

        Entity enemy = new Entities.Entity()
        {
            Position = new int[] { 100, 100 },
            Facing = 0
        };

        Random random = new Random();


        private void skerkos_Loaded(object sender, RoutedEventArgs e)
        {
            width = (int)this.ViewportWrapper.ActualWidth;
            height = (int)this.ViewportWrapper.ActualHeight;
            writeableBmp = BitmapFactory.New(width, height);
            Viewport.Source = writeableBmp;

            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            pressed_keys.Add(e.Key);
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            pressed_keys.Remove(e.Key);
            switch (e.Key)
            {
                case Key.Space:
                    string br = "keys: ";
                    foreach (var k in pressed_keys)
                    {
                        br += Convert.ToString(k);
                    }
                    MessageBox.Show(br);
                    break;
                default: break;
            }
        }

        

        private void skerkos_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }


        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            writeableBmp.Clear();

            foreach (var player in players)
            {
                player.Move(pressed_keys);

                writeableBmp.FillEllipseCentered(player.Position[0], player.Position[1], player.Size, player.Size, Color.FromRgb(Convert.ToByte(player.Color[0]), Convert.ToByte(player.Color[1]), Convert.ToByte(player.Color[2])));
                writeableBmp.DrawEllipseCentered(player.Position[0], player.Position[1], player.Size, player.Size, Colors.Black);

                int entityDistance = Coords.DistanceOfPoints(new int[] { player.Position[0], player.Position[1] }, new int[] { enemy.Position[0], enemy.Position[1] });

                if (entityDistance < 25 + player.Size)
                {
                    enemy.Position[0] = Convert.ToInt32(random.Next(600)) + 100;
                    enemy.Position[1] = Convert.ToInt32(random.Next(250)) + 100;

                    player.Size++;

                    player.Color[0] = Convert.ToByte(random.Next(256));
                    player.Color[1] = Convert.ToByte(random.Next(256));
                    player.Color[2] = Convert.ToByte(random.Next(256));
                }
            }


            writeableBmp.FillEllipseCentered(enemy.Position[0], enemy.Position[1], 25, 25, Colors.Red);
        }
    }
}
