using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightGame
{
    class BackgroundEntity : Entity, IMovable
    {
        private string bg_name;

        private ScrollingSprite bg;
        private ScrollingSprite clouds;
        private ScrollingSprite grass;

        public Vector2 m_position
        {
            get { return _m_position; }
            set {
                clouds.scrollX = (int)(value.X / 2);
                grass.scrollX = (int)value.X;
                grass.localOffset = new Vector2(0, 608 + value.Y);
                clouds.localOffset = new Vector2(0, 250 + value.Y / 2);
                var n = -100.625f + Math.Min(100.625f, value.Y);
                bg.localOffset = new Vector2(0, n);
                _m_position = value;
            }
        }
        private Vector2 _m_position = Vector2.Zero;
        public BackgroundEntity(string bg_name)
        {
            this.bg_name = bg_name;
            name = "background";
            scale = new Vector2(1280 / 4096f);
        }
        public override void onAddedToScene()
        {
            var bg_texture = scene.content.Load<Texture2D>("bg/" + bg_name);
            var cloud_texture = scene.content.Load<Texture2D>("bg/clouds_" + bg_name);
            var grass_texture = scene.content.Load<Texture2D>("bg/grass_" + bg_name);

            bg = addComponent<ScrollingSprite>(new ScrollingSprite(bg_texture));
            bg.origin = Vector2.Zero;
            bg.localOffset = new Vector2(0, -322 * 1280 / 4096);
            bg.layerDepth = 10;
            clouds = addComponent<ScrollingSprite>(new ScrollingSprite(cloud_texture));
            clouds.origin = Vector2.Zero;
            clouds.localOffset = new Vector2(0, 250);
            //clouds.scrollSpeedX = 40;
            grass = addComponent<ScrollingSprite>(new ScrollingSprite(grass_texture));
            grass.origin = Vector2.Zero;
            grass.localOffset = new Vector2(0, 608);
            grass.layerDepth = -1;
            //grass.scrollSpeedX = 200;
        }
    }
}
