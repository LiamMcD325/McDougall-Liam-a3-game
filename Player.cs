using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MohawkGame2D
{
    internal class Player
    {

        public string name;
        public int score;
        public int r;
        public int g;
        public int b;
        public int acceleration;
        public Vector2 plyPosition;
        public Vector2 plySize;
        
        
        /// <summary>
        /// Creates a player with basic variables
        /// </summary>
        public Player(){
            this.name = "";
            score = 0;
            r = Random.Integer(1, 255);
            g = Random.Integer(1, 255);
            b = Random.Integer(1, 255);
            acceleration = 1;
            plyPosition = new Vector2(400, 500);
            plySize = new Vector2(10, 10);

        }

        /// <summary>
        /// Creates a player
        /// </summary>
        /// <param name="Name">Name input by player</param>
        public Player(string Name)
        {
            this.name = Name;
            score = 0;
            r = Random.Integer(1, 255);
            g = Random.Integer(1, 255);
            b = Random.Integer(1, 255);
            acceleration = 1;

        }

        public Player(string name, int score, int r, int g, int b, Vector2 plySize, Vector2 plyPosition, int radius)
        {
            this.name = name;
            this.score = score;
            this.r = r;
            this.g = g;
            this.b = b;
            this.plySize = plySize;
            this.plyPosition = plyPosition;
            acceleration = 1;

        }
    }

    
}
