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

        Player player = new Entities.Player()
        {
            Position = new int[] { 100, 100 },
            Facing = 0,
            Controls = new Key[] { Key.Up, Key.Left, Key.Down, Key.Right },
            Speed = 5
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

        int go = 0;

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {

            player.Move(pressed_keys);

            writeableBmp.Clear();
            
            writeableBmp.FillEllipseCentered(player.Position[0], player.Position[1], size, size, Color.FromRgb(Convert.ToByte(color[0]), Convert.ToByte(color[1]), Convert.ToByte(color[2])));
            writeableBmp.DrawEllipseCentered(player.Position[0], player.Position[1], size, size, Colors.Black);

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
