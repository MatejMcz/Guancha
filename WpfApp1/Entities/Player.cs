using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Entities;
using System.Windows.Input;
using System.Windows;

namespace WpfApp1.Entities
{
    internal class Player : Entity
    {
        public Key[] Controls { get; set; }
        public void Tick()
        {
        }
        public void Move(HashSet<Key> pressed_keys)
        {
            for(int i =0; i < Controls.Length; i++)
            {
                if (pressed_keys.Contains(Controls[i]))
                {
                    Position[0] += direction_map[i][0] * Speed;
                    Position[1] += direction_map[i][1] * Speed;
                }
            }
        }
    }
}
