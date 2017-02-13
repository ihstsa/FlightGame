using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;



namespace FlightGame
{
    class AeroComponent : Component, IUpdatable
    {
        private float trim = 0;
        private float initial_thrust = 0;
        public AeroComponent(float trim, float thrust)
        {
            Console.WriteLine("cc " + trim + " " + thrust);

            this.trim = trim;
            charge = thrust;
            initial_thrust = thrust;
        }
        public float charge = 230;

        private Vector2 velocity
        {
            get { return vc.velocity; }
            set { vc.velocity = value; }
        }

        private float pitch
        {
            get { return vc.rotation; }
            set { vc.rotation = value; }
        }

        private static Vector2 grav = new Vector2(0, 20f);
        private const float lift_const = .00017f;
        private const float drag_const = .00012f;//.001f;
        //private const float motor_const = 20;

        int tc = Environment.TickCount;

        private VelocityComponent vc;

        public override void initialize()
        {
            base.initialize();
            //entity.position = new Vector2(-200, 700);
            vc = entity.addComponent<VelocityComponent>();
            Console.WriteLine("component " + trim + " " + charge);

            velocity = new Vector2(2, 1);
            pitch = -(float)Math.PI / 10;
        }

        public void update()
        {
            var pitch = Util.ang_to_vec(this.pitch);
            var angle_of_attack = Util.nfmod((this.pitch - Util.vec_to_ang(velocity)) + (float)Math.PI, (float)Math.PI * 2) - (float)Math.PI;
            var vel_squared = velocity * velocity;
            //Console.WriteLine(angle_of_attack * Mathf.rad2Deg);
            if (velocity.X < 0) vel_squared.X *= -1;
            if (velocity.Y < 0) vel_squared.Y *= -1;
            var lift_amplitude = (vel_squared * pitch).Length() * lift_const * (angle_of_attack > 35 * Mathf.deg2Rad ? 0 : angle_of_attack < -35 * Mathf.deg2Rad ? 0 : Math.Abs(35 / 2 - Math.Abs(angle_of_attack)) / 35 + 0.8f);
            //if (lift_amplitude == 0) Console.WriteLine("STALLED");
            var lift_dir = new Vector2(pitch.Y, -pitch.X);
            var lift_vector = lift_dir * lift_amplitude;
            var drag_vector = -velocity * velocity.Length() * drag_const * ((-Mathf.cos(2 * angle_of_attack) + 1) / 3 + 1 / 3); // \frac{-\cos \left(2x\right)+1}{2}
            var motor_vec = Vector2.Zero;
            if (Input.isKeyDown(Keys.Space)) {
                motor_vec = pitch * charge;
                charge *= Mathf.pow(.55f, Time.deltaTime);
                vc.angular_velocity += trim * 0.000008f * vel_squared.Length() * Time.deltaTime;
            }
            var accel = lift_vector + drag_vector + grav + motor_vec;


            var l = Input.isKeyDown(Keys.Left);
            var r = Input.isKeyDown(Keys.Right);
            var input_torque = ((l ? -0.08f : 0) + (r ? 0.08f : 0));

            vc.angular_velocity += input_torque * Time.deltaTime;
            //Console.WriteLine(velocity + " " + Util.vec_to_ang(velocity));
            vc.angular_velocity -= angle_of_attack * 0.2f * Time.deltaTime;
            

            //var tcc = Environment.TickCount;
            //Console.WriteLine(tcc - tc + " " + Time.deltaTime);
            //tc = tcc;
            //var accel = new Vector2(1, 0);
            velocity += accel * Time.deltaTime;

            //Console.WriteLine("V: " + vel_squared + " L: " + lift_vector + " D: " + drag_vector);

            //pitch = Vector2.Normalize((pitch * 143 + Vector2Ext.normalize(velocity)));
            //pitch = Vector2.Normalize(new Vector2(1, -1));
            //Console.WriteLine(pitch + " " + pitch.Length());
            //entity.rotation = (float)Math.PI / 4f;

            /*velocity += accel * Time.deltaTime;
            //Console.WriteLine(velocity);
            var before = entity.position.X;
            entity.position += velocity;
            Console.WriteLine(velocity);
            Console.WriteLine(entity.position.X - before);*/
        }
    }
}