using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nez;
using Microsoft.Xna.Framework;

namespace FlightGame
{
    class VelocityComponent : Component, IUpdatable
    {
        public Vector2 position;
        public IMovable mover;
        public float rotation
        {
            get { return entity.rotation; }
            set { entity.rotation = value; }
        }

        public Vector2 velocity;
        public float angular_velocity = 0;

        public void update()
        {
            position += velocity * Time.deltaTime;
            entity.position = new Vector2(entity.position.X, Math.Max(position.Y, 20));
            mover.m_position = new Vector2(position.X, 20 - Math.Min(position.Y, 20));
            rotation += angular_velocity * Time.deltaTime;
        }
    }
}
