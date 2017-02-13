using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightGame
{
    static class Util
    {
        public static float nfmod(float a, float b)
        {
            return a - b * Mathf.floor(a / b);
        }

        public static Vector2 ang_to_vec(float ang)
        {
            return new Vector2(Mathf.cos(ang), Mathf.sin(ang));
        }

        public static float vec_to_ang(Vector2 vec)
        {
            return Mathf.atan2(vec.Y, vec.X);
        }
    }
}
