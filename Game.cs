// Include the namespaces (code libraries) you need below.
using System;
using System.Numerics;

// The namespace your code is in.
namespace MohawkGame2D
{
    /// <summary>
    ///     Your game code goes inside this class!
    /// </summary>
    public class Game
    {
        // Place your variables here:
        //Player
        int playerX = 400;
        int playerY = 400;
        int playerRadius = 5;
        Vector2 playerMovement = new Vector2(400, 400);
        int r = Random.Integer(1, 255);
        int g = Random.Integer(1, 255);
        int b = Random.Integer(1, 255);

        //Street lines
        //First sidewalk line
        Vector2 point1_1 = new Vector2(80, 600);
        Vector2 point1_2 = new Vector2(200, 300);

        //Second sidwalk line
        Vector2 point2_1 = new Vector2(600, 300);
        Vector2 point2_2 = new Vector2(780, 600);

        //Yellow division lines idk
        Vector2[] roadLines = new Vector2[5]; 
        bool drawLine = true; //Will determine if line is to be seen

        //
        float secondsElapsed = Time.SecondsElapsed;

        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetSize(800, 600);
            Window.SetTitle("Assignment 3 Game");
            Window.TargetFPS = 10;

           
            
            //Initialize the roadlines
            for (int i = 0; i < roadLines.Length; i++) {
                roadLines[i] = new Vector2(400, 300 + i * 50);
                //300, 350, 400, 450, 500
            }

        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.Black);
            drawBackground();
            //Draw score
            Vector2 lame = new Vector2(44, 44);
            Text.Color = Color.White;
            int guy = 60 - Convert.ToInt32(Time.SecondsElapsed);

            Text.Draw((guy).ToString(), lame);




            drawPlayer();
            playerInputs();

            //Scales radius
            if (playerY >= 200) { playerRadius = 5; }
            if (playerY >= 250) { playerRadius = 10; }
            if (playerY >= 300) { playerRadius = 15; }
             if (playerY >= 400) { playerRadius = 20; }
             if (playerY >= 500) { playerRadius = 25; }
             if (playerY >= 600) { playerRadius = 30; }
        }

        /// <summary>
        /// Checks if player is making an input
        /// </summary>
        public void playerInputs()
        {
            if (Input.IsKeyboardKeyDown(KeyboardInput.Right) == true)
            {
                playerX += 8;
            }
            else if (Input.IsKeyboardKeyDown(KeyboardInput.Left) == true)
            {
                playerX -= 8;
            }
            if (Input.IsKeyboardKeyDown(KeyboardInput.Down) == true)
            {
                playerY += 8;
            }
            else if (Input.IsKeyboardKeyDown(KeyboardInput.Up) == true)
            {
                playerY -= 8;
            }
            if (playerY <= 300) { playerY = 301; }

            if (playerX < 80) { playerX = 80; }
            if (playerX > 780) { playerX = 780; }

            if (playerY > 600) { playerY = 600; }
        }

        /// <summary>
        /// Draws the basic background colours
        /// </summary>
        public void drawBackground()
        {
            //Draws the sky
            Color sky = new Color(121, 174, 255);
            Draw.LineColor = sky;
            Draw.FillColor = sky;
            Vector2 skyOriginPoint = new Vector2(0, 0);
            Vector2 skySize = new Vector2(800, 300);
            Draw.Rectangle(skyOriginPoint, skySize);


            //Draws the sidewalk lines
            Draw.LineColor = Color.Gray;
            Draw.FillColor = Color.Gray;
            Draw.Line(point1_1, point1_2);
            Draw.Line(point2_1, point2_2);

            for (int i = 0; i < roadLines.Length; i++)
            {
                if (drawLine == true)
                {
                    Draw.LineColor = Color.Gray;
                    Draw.FillColor = Color.Gray;
                    drawLine = false;
                }
                else if (drawLine == false)
                {
                    Draw.LineColor = Color.Black;
                    Draw.FillColor = Color.Black;
                    drawLine = true;
                }
                Vector2 hi = new Vector2(roadLines[i].X, roadLines[i].Y + 50);
                Draw.Line(roadLines[i], hi);




            }
        }

        /// <summary>
        /// Draws the player
        /// </summary>
        public void drawPlayer(){
            //Wheels
            Draw.LineColor = Color.Gray;
            Draw.FillColor = Color.Gray;
            Vector2 playerWheel = new Vector2(playerX - 12, playerY + 15);
            Draw.Circle(playerWheel, playerRadius / 2);
            Vector2 playerWheel2 = new Vector2(playerX + 8, playerY + 15);
            Draw.Circle(playerWheel2, playerRadius / 2);

            Color playerColour = new Color(r, g, b);
            Draw.LineColor = playerColour;
            Draw.FillColor = playerColour;
            playerMovement = new Vector2(playerX, playerY);
            Draw.Circle(playerMovement, playerRadius);

        }
    }

}
