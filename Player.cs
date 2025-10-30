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
        int r;
        int g;
        int b;
        public int acceleration;
        public Vector2 plyPosition;
        public Vector2 plySize;
        public int multiplyer;
        public int evilFrogsKilled = 0;

        public void Setup() { ResetPlayer(); }

        public void Update() { DrawPlayer(); }

        /// <summary>
        /// Creates a player with basic variables
        /// </summary>
        public Player(){
            this.name = "";
            score = 0;
            r = Random.Integer(1, 255);
            g = Random.Integer(1, 255);
            b = Random.Integer(1, 255);
            acceleration = 50;
            plyPosition = new Vector2(400, 500);
            plySize = new Vector2(10, 10);
            multiplyer = 1;

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
            acceleration = 50;
            multiplyer = 1;

        }


        public void DrawPlayer(){

            //Draws the tires
            //Tire 1
            Draw.LineColor = Color.Gray;
            Draw.FillColor = Color.Gray;
            Vector2 playerMovement = new Vector2(plyPosition.X - 6, plyPosition.Y + 2);
            Vector2 playerMovement2 = new Vector2(6, 20);
            Draw.Rectangle(playerMovement, playerMovement2);

            //Tire 2
            playerMovement = new Vector2(plyPosition.X + 40, plyPosition.Y + 2);
            playerMovement2 = new Vector2(6, 20);
            Draw.Rectangle(playerMovement, playerMovement2);

            //Tire 3
            playerMovement = new Vector2(plyPosition.X - 6, plyPosition.Y + 52);
            playerMovement2 = new Vector2(6, 20);
            Draw.Rectangle(playerMovement, playerMovement2);

            //Tire 4
            playerMovement = new Vector2(plyPosition.X + 40, plyPosition.Y + 52);
            playerMovement2 = new Vector2(6, 20);
            Draw.Rectangle(playerMovement, playerMovement2);

            //Draw the main vehicle
            Color playerColour = new Color(r, g, b);
            Draw.LineColor = playerColour;
            Draw.FillColor = playerColour;
            playerMovement = new Vector2(plyPosition.X, plyPosition.Y);
            playerMovement2 = new Vector2(40, 75);
            Draw.Rectangle(playerMovement, playerMovement2);
        }

        public void ResetPlayer(){

            score = 0;
            multiplyer = 0;
            evilFrogsKilled = 0;
            acceleration = 50;
        }
    }

    
}
