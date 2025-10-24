using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MohawkGame2D
{
    internal class Rocket
    {
        public Vector2 position;
        public Vector2 size = new Vector2(5, 20);
        public bool isRocket;

        public Rocket(int x, int y) {
            position = new Vector2(x, y);
            isRocket = false;
        }

        public Rocket() {
            position = new Vector2(800, 600);
            isRocket = false;

        }
        public void Setup() { }

        public void Update(){ }

        /// <summary>
        /// Draws the player
        /// </summary>
        public void drawRocket()
        {

            //If rocket has been launched
            if (isRocket == true)
            {
                Console.WriteLine("ROCKET LAUNCHED!!");
                position.Y -= 15;
                Draw.LineColor = Color.Red;
                Draw.FillColor = Color.Blue;

                Draw.Rectangle(position, size);
                if (position.Y < 0) { isRocket = false; Console.WriteLine("rocket done"); }
            }
        }
    }
}
