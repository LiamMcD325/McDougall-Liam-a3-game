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
        public Vector2 size;
        public bool isRocket;

        public Rocket(int x, int y) {
            position = new Vector2(x, y);
            isRocket = false;
            size = new Vector2(5, 20);
        }

        public Rocket() {
            position = new Vector2(800, 600);
            isRocket = false;
            size = new Vector2(5, 20);

        }
        public void Setup() { }

        public void Update(){ }


    }
}
