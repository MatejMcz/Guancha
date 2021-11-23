using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Entities
{
    internal class Entity
    {


        public readonly Dictionary<int, int[]> direction_map = new Dictionary<int, int[]>
        {
            { 0, new int[] { 0, -1 } },
            { 1, new int[] { -1, 0 } },
            { 2, new int[] { 0, 1 } },
            { 3, new int[] { 1, 0 } }
        };


        
        public int[]  Position { get; set; }
        public int Facing { get; set; }
        public int[] Color { get; set; }
        public int Speed { get; set; }
        public int Size { get; set; }
    }
}
