using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightGame
{
    class PitchVecComponent : Component, IUpdatable
    {
        public Vector2 pitch {
            get { return new Vector2(Mathf.cos(pitch_n), Mathf.sin(pitch_n)); }
            set { pitch_n = Mathf.atan2(value.Y, value.X); }
        }
        public float pitch_n;
        private float offset_pitch = 0;
        //private Vector2 _pitch = new Vector2(1, 0);
        public void update() {
            /*var oop = offset_pitch;
            var l = Input.isKeyDown(Keys.Left);
            var r = Input.isKeyDown(Keys.Right);
            if (l)
            {
                offset_pitch = (offset_pitch * 49 + (float)-Math.PI / 9) / 50f;
            }
            if (r)
            {
                offset_pitch = (offset_pitch * 49 + (float)Math.PI / 9) / 50f;
            }
            if (!(l || r)) offset_pitch = offset_pitch * 99 / 100;
            pitch_n += offset_pitch - oop;
            Console.WriteLine(oop);*/
            entity.rotation = pitch_n;
        }
    }
}
