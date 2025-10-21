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

        //Street lines
        Vector2 point1_1 = new Vector2(80, 600);
        Vector2 point1_2 = new Vector2(200, 300);

        Vector2 point2_1 = new Vector2(600, 300);
        Vector2 point2_2 = new Vector2(780, 600);

        Vector2[] roadLines = new Vector2[5]; 
        bool drawLine = true;

        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetSize(800, 600);
            Window.SetTitle("Assignment 3 Game");
            Window.TargetFPS = 8;

            //Initialize the roadlines
            for (int i = 0; i < roadLines.Length; i++) {
                roadLines[i] = new Vector2(400, 300 + i * 50);
                /*
                300
                 350
                400,
               450
                500
                 */
            }

        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.Black);


            Draw.Line(point1_1, point1_2);
            Draw.Line(point2_1, point2_2);

            for (int i = 0; i < roadLines.Length; i++)
            {
                if(drawLine  == true){
                    Draw.LineColor = Color.Gray;
                    Draw.FillColor = Color.Gray;
                    drawLine = false;
                }
                else if(drawLine == false)
                {
                    Draw.LineColor = Color.Black;
                    Draw.FillColor = Color.Black;
                    drawLine = true;
                }
                Vector2 hi = new Vector2(roadLines[i].X, roadLines[i].Y + 50);
                    Draw.Line(roadLines[i], hi);

                playerInputs();
                Draw.LineColor = Color.Gray;
                Draw.FillColor = Color.Gray;
                playerMovement = new Vector2(playerX, playerY);
                Draw.Circle(playerMovement, playerRadius);

            }

            //Scales radius
            if (playerY >= 200) { playerRadius = 10; }
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
                playerX += 5;
            }
            else if (Input.IsKeyboardKeyDown(KeyboardInput.Left) == true)
            {
                playerX -= 5;
            }
            if (Input.IsKeyboardKeyDown(KeyboardInput.Down) == true)
            {
                playerY += 5;
            }
            else if (Input.IsKeyboardKeyDown(KeyboardInput.Up) == true)
            {
                playerY -= 5;
            }
        }
    }

}
