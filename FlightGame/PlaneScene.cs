using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nez;
using Nez.Svg;
using Microsoft.Xna.Framework.Graphics;
using Nez.Sprites;
using Microsoft.Xna.Framework;

namespace FlightGame
{
    class PlaneScene : Scene
    {

        Entity plane;
        private bool dead = false;
        private float trim = 0;
        private float thrust = 0;

        private Text text_left;
        private Text text_gone;
        private static int level = 0;
        private int end = 10000;
        public PlaneScene(float trim, float thrust)
        {
            Console.WriteLine("scene " + trim + " " + thrust);
            end = 10000 + level * 1000;
            this.trim = trim;
            this.thrust = thrust;
        }
        public override void initialize()
        {
            base.initialize();
            addRenderer(new DefaultRenderer(0));
            setDesignResolution(1280, 720, SceneResolutionPolicy.ShowAllPixelPerfect);

            Screen.setSize(1280, 720);

        }
        public override void onStart()
        {
            base.onStart();



            /*Texture2D[] backgrounds = new Texture2D[5];
            for(int i = 0; i < 5; i++)
            {
                backgrounds[i] = content.Load<Texture2D>("background_");
            }*/
            BackgroundEntity background = null;
            if (level == 0) background = new BackgroundEntity("sunrise");
            if (level == 1) background = new BackgroundEntity("day");
            if (level == 2) background = new BackgroundEntity("sunset");
            if (level == 3) background = new BackgroundEntity("night");
            //var background = new BackgroundEntity("day");
            background.attachToScene(this);
            //background.addComponent(new Sprite(bg));
            //background.getComponent<Sprite>().transform.scale = new Vector2(.3125f);
            //background.getComponent<Sprite>().origin = Vector2.Zero;
            var texture = content.Load<Texture2D>("plane");
            plane = createEntity("plane");
            plane.addComponent(new Sprite(texture));
            plane.getComponent<Sprite>().flipX = true;
            //plane.getComponent<Sprite>().transform.scale = new Vector2(2);
            Console.WriteLine("scenecall " + trim + " " + thrust);
            plane.addComponent(new AeroComponent(trim, thrust));
            plane.position = new Vector2(640, 0);
            plane.getComponent<VelocityComponent>().position = new Vector2(0, 360);
            //plane.getComponent<VelocityComponent>().velocity = new Vector2(4, 0);
            plane.getComponent<VelocityComponent>().mover = background;
            text_left = new Text(Nez.Graphics.instance.bitmapFont, "Distance Left: infinity", new Vector2(1150, 20), Color.DarkRed);
            text_gone = new Text(Nez.Graphics.instance.bitmapFont, "Distance Traveled: zero", new Vector2(30, 20), Color.DarkRed);
            var text = createEntity("text");
            text.addComponent(text_left);
            text.addComponent(text_gone);
            //Console.Write(plane.getComponent<AeroComponent>().enabled);
            //entity.addComponent(new SvgDebugComponent("plane.svg"));
        }

        public override void update()
        {
            base.update();
            var yp = plane.getComponent<VelocityComponent>().position.Y;
            var xp = plane.getComponent<VelocityComponent>().position.X;
            text_left.setText("Distance Left: " + (end - xp));
            text_gone.setText("Distance Gone: " + xp);
            if ((yp > 625 || yp < -950) && !dead)
            {
                dead = true;
                Core.startSceneTransition(new FadeTransition(() => new TrimScene()));
            }
            else if(xp > end && !dead)
            {
                dead = true;
                level++;
                if(level == 4)
                {
                    Core.startSceneTransition(new FadeTransition(() => new WinScene()));
                } else
                {
                    Core.startSceneTransition(new FadeTransition(() => new TrimScene()));
                }
                
            }
        }
    }
}
