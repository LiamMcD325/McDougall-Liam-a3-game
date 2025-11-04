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

        
        public int score;
        int r;
        int g;
        int b;
        public int acceleration;
        public Vector2 plyPosition;
        public Vector2 plySize;
        public int multiplyer;
        public int evilFrogsKilled = 0;

        public void Setup() { ResetPlayer();  }

        public void Update() {
            PlayerInputs();
            DrawPlayer();

                         
        }

        /// <summary>
        /// Creates a player with basic variables
        /// </summary>
        public Player()
        {
            
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
           
            score = 0;
            r = Random.Integer(1, 255);
            g = Random.Integer(1, 255);
            b = Random.Integer(1, 255);
            acceleration = 50;
            multiplyer = 1;

        }


        public void DrawPlayer()
        {
            Console.WriteLine("DRAW PLAYER HAS LOADED");
            Console.WriteLine("Drawing player at position: " + plyPosition);
            //Draws the tires
            //Tire 1
            Draw.LineColor = Color.Gray;
            Draw.FillColor = Color.Gray;
            Vector2 playerMovement = new(plyPosition.X - 6, plyPosition.Y + 2);
            Vector2 playerMovement2 = new(6, 20);
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

        public void ResetPlayer()
        {

            score = 0;
            multiplyer = 1;
            evilFrogsKilled = 0;
            acceleration = 50;
        }

        /// <summary>
        /// Checks if player is making an input
        /// </summary>
        public void PlayerInputs()
        {
            float deadzone = 0.05f;
            if ((Input.IsKeyboardKeyDown(KeyboardInput.Right) == true) || (Input.GetAnyControllerAxis(ControllerAxis.LeftX, deadzone) > 0.10))
            {
                plyPosition.X = plyPosition.X + acceleration / 10;

            }
            else if ((Input.IsKeyboardKeyDown(KeyboardInput.Left) == true) || (Input.GetAnyControllerAxis(ControllerAxis.LeftX, deadzone) < -0.10))
            {
                plyPosition.X = plyPosition.X - acceleration / 10;
            }
            if ((Input.IsKeyboardKeyDown(KeyboardInput.Down) == true) || (Input.GetAnyControllerAxis(ControllerAxis.LeftY, deadzone) > 0.10))
            {
                plyPosition.Y = plyPosition.Y + acceleration / 10; ;
            }
            else if ((Input.IsKeyboardKeyDown(KeyboardInput.Up) == true) || (Input.GetAnyControllerAxis(ControllerAxis.LeftY, deadzone) < -0.10))
            {
                plyPosition.Y = plyPosition.Y - acceleration / 10;
            }
            if ((Input.IsKeyboardKeyDown(KeyboardInput.W) == true) || (Input.IsAnyControllerButtonDown(ControllerButton.RightTrigger2) == true))
            {
                acceleration += 2;
                if (acceleration > 750) { acceleration = 750; }

            }
            else if ((Input.IsKeyboardKeyDown(KeyboardInput.S) == true) || (Input.IsAnyControllerButtonDown(ControllerButton.LeftTrigger2) == true))
            {
                acceleration -= 2;
                if (acceleration < 0) { acceleration = 0; }

            }

            //Collision so the player cannot go on the grass
            if (plyPosition.Y < 0) { plyPosition.Y = 0; }
            if (plyPosition.Y > 540) { plyPosition.Y = 540; }

            if (plyPosition.X < 154) { plyPosition.X = 154; }
            if (plyPosition.X > 604) { plyPosition.X = 604; }

            

        }

    }
}
