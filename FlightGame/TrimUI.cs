using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nez.UI;
using Nez;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Nez.Sprites;

namespace FlightGame
{
    class TrimUI : UICanvas
    {
        static float thrust = 200;
        static float trim = 0;
        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
            var skin = Skin.createDefaultSkin();

            var main_table = stage.addElement(new Table());

            //main_table.defaults().setPadTop(10).setMinWidth(800).setMinHeight(600).setMaxWidth(1280).setMaxHeight(720);
            //main_table.defaults().setPrefWidth(600).setPrefHeight(400);
            main_table.setFillParent(true).center();
            /*var pl = entity.scene.content.Load<Texture2D>("plane");
            var im = new Image(pl);
            var m = main_table.add(im);
            main_table.row();*/
            main_table.add(new Label("Starting thrust"));
            var tsl = new Slider(50, 275, 1, false, SliderStyle.create(Color.Gray, Color.Black));
            tsl.setValue(thrust);
            var thr_label = new Label(thrust.ToString());
            tsl.onChanged += (val) => { thrust = val; thr_label.setText(val.ToString()); };
            main_table.add(tsl);
            main_table.add(thr_label);
            main_table.row();
            main_table.add(new Label("Trim"));
            var sl = new Slider(-1, 1, 0.02f, false, SliderStyle.create(Color.Gray, Color.Black));
            sl.setValue(trim);
            var trm_label = new Label(trim.ToString());
            sl.onChanged += (val) => { trim = val; trm_label.setText(val.ToString()); };
            main_table.add(sl);
            main_table.add(trm_label);
            main_table.row();
            var go_button = new TextButton("Fly!", TextButtonStyle.create(Color.LightGray, Color.DarkGray, Color.Gray));
            go_button.setWidth(100);
            go_button.setHeight(50);
            go_button.onClicked += (but) => { Console.WriteLine("ui " + trim + " " + thrust);  Core.scene = new PlaneScene(trim, thrust); };
            main_table.add(go_button);
        }
    }

}