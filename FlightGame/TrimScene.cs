using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightGame
{
    class TrimScene : Scene
    {
        public override void initialize()
        {
            base.initialize();
            addRenderer<DefaultRenderer>(new DefaultRenderer(0));
            var e = createEntity("trim_ui");
            e.addComponent<TrimUI>(new TrimUI());
        }
    }
}
