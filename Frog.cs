using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MohawkGame2D
{
    internal class Frog
    {
        public Vector2 position;
        public Vector2 size = new Vector2(10, 10);
        public int counter = 500;

        public Frog(int x, int y)
        {
            position = new Vector2(x, y);
        }

        public Frog() { }


        public void Update()
        {
        }
    }
}
