using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightGame
{
    class WinScene : Nez.Scene
    {
        public override void initialize()
        {
            base.initialize();
            addRenderer(new Nez.DefaultRenderer(0));

            var text_gone = new Nez.Text(Nez.Graphics.instance.bitmapFont, "You win! You've successfully reached Orlando!", new Microsoft.Xna.Framework.Vector2(30, 20), Microsoft.Xna.Framework.Color.DarkRed);
            var text = createEntity("text");
            text.scale = new Microsoft.Xna.Framework.Vector2(3);
            text.addComponent(text_gone);

        }
    }
}
