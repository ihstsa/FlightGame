using Nez;
using Nez.Textures;
using Nez.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez.Tweens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightGame
{
    class IntroScene : Scene
    {
        Texture2D texture_1;
        Texture2D texture_2;
        Texture2D texture_3;
        Texture2D texture_4;

        Sprite current;
        Sprite cover;

        Entity e;
        public override void initialize()
        {
            base.initialize();
            addRenderer(new DefaultRenderer(0));


            e = createEntity("intro");
            texture_1 = content.Load<Texture2D>("intro/1");
            texture_2 = content.Load<Texture2D>("intro/2");
            texture_3 = content.Load<Texture2D>("intro/3");
            texture_4 = content.Load<Texture2D>("intro/4");
            current = e.addComponent<Sprite>(new Sprite(texture_1));
            current.origin = Vector2.Zero;
            current.layerDepth = 1;
            cover = e.addComponent<Sprite>(new Sprite(Graphics.createSingleColorTexture(1, 1, Color.Transparent)));
            cover.setEnabled(false);
            //cover.transform.scale = new Vector2(1280, 720);
            Core.startCoroutine(animate());
        }

        /*private IEnumerable<object> fade(Color from, Color to, float duration)
        {
            var transition = new FadeTransition();
            transition.onScreenObscured = myOnScreenObscuredMethod;
            Core.startSceneTransition(transition);

        }*/
        private IEnumerator<object> wait_with_break(float duration)
        {
            var elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                if (Input.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space, Microsoft.Xna.Framework.Input.Keys.Escape))
                {
                    while (Input.isKeyDown(Microsoft.Xna.Framework.Input.Keys.Space, Microsoft.Xna.Framework.Input.Keys.Escape)) yield return null;
                    yield break;
                };
                yield return null;
            }
        }
        private IEnumerator<object> animate()
        {
            yield return Core.startCoroutine(wait_with_break(4));
            //yield return fade(Color.Transparent, Color.Black, 0.5f);
            current.subtexture = new Subtexture(texture_2);
            current.origin = Vector2.Zero;
            //yield return fade(Color.Black, Color.Transparent, 0.5f);
            yield return Core.startCoroutine(wait_with_break(4));
            //yield return fade(Color.Transparent, Color.Black, 0.5f);
            current.subtexture = new Subtexture(texture_3);
            current.origin = Vector2.Zero;
            //yield return fade(Color.Black, Color.Transparent, 0.5f);
            yield return Core.startCoroutine(wait_with_break(2));
            //yield return fade(Color.Transparent, Color.Black, 0.5f);
            current.subtexture = new Subtexture(texture_4);
            current.origin = Vector2.Zero;
            //yield return fade(Color.Black, Color.Transparent, 0.5f);
            yield return Core.startCoroutine(wait_with_break(2));
            Core.scene = new TrimScene();
        }
    }
}
