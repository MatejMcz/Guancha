using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Utilities
{
    internal class Coords
    {
        public static int[] RadToCart(int angle, int distance)
        {
            int x, y;

            double angle_rad = (angle - 180) * (Math.PI / 180);


            x = Convert.ToInt32(Math.Cos(angle_rad) * distance);
            y = Convert.ToInt32(Math.Sin(angle_rad) * distance);


            return new int[] { x, y };
        }

        public static int DistanceOfPoints(int[] first_point, int[] second_point)
        {
            int y_distance = Math.Abs(first_point[0] - second_point[0]);
            int x_distance = Math.Abs(first_point[1] - second_point[1]);

            double result = Math.Sqrt(Math.Pow(y_distance, 2) + Math.Pow(x_distance, 2));

            return Convert.ToInt32(result);
        }
    }
}
