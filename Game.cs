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
        Player player = new Player();

        //Street lines
        //First sidewalk line
        Vector2 point1_1 = new Vector2(80, 600);
        Vector2 point1_2 = new Vector2(200, 300);

        Vector2 groundObjLS1 = new Vector2(100, 200);
        Vector2 groundObjLS2 = new Vector2(40, 300);
        Vector2 groundObjLS3 = new Vector2(-20, 400);
        int groundObjLSTimer = 500;

        //Second sidwalk line
        Vector2 point2_1 = new Vector2(600, 300);
        Vector2 point2_2 = new Vector2(720, 600);

        //Yellow division lines idk
        Vector2[] roadLines = new Vector2[5];
        bool drawLine = true; //Will determine if line is to be seen

        int screen = 1; //Will determine what screen to show
        //
        float secondsElapsed = Time.SecondsElapsed;

        int textureChoice = 1; //Will choose what texture to load
        string textureFilePath = "";
        Texture2D playerTexture; 

        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetSize(800, 600);
            Window.SetTitle("Assignment 3 Game");
            Window.TargetFPS = 10;


            textureFilePath = "MohawkGame2D\\Images\\StopSign.png";
            playerTexture = Graphics.LoadTexture(textureFilePath);

            //Initialize the roadlines
            for (int i = 0; i < roadLines.Length; i++)
            {
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

            if (screen == 1)
            {
                if ((Input.IsKeyboardKeyDown(KeyboardInput.Enter) == true) || (Input.IsAnyControllerButtonPressed(ControllerButton.MiddleRight) == true))
                {
                    screen = 2;
                    Time.SecondsElapsed = 0;
                }
                Vector2 textPosition = new Vector2(44, 44);
                Text.Color = Color.White;
                Text.Draw("Driving Game", textPosition);
                textPosition = new Vector2(200, 300);
                Text.Draw("Press 'enter' or 'start' to begin!", textPosition);
                textPosition = new Vector2(200, 350);
                Text.Draw("Use a keyboard or controller!", textPosition);

                if (Input.IsKeyboardKeyDown(KeyboardInput.A)) { player.name = player.name + "a"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.B)) { player.name = player.name + "b"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.C)) { player.name = player.name + "c"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.D)) { player.name = player.name + "d"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.E)) { player.name = player.name + "e"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.F)) { player.name = player.name + "f"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.G)) { player.name = player.name + "g"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.H)) { player.name = player.name + "h"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.I)) { player.name = player.name + "i"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.J)) { player.name = player.name + "j"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.K)) { player.name = player.name + "k"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.L)) { player.name = player.name + "l"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.M)) { player.name = player.name + "m"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.N)) { player.name = player.name + "n"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.O)) { player.name = player.name + "o"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.P)) { player.name = player.name + "p"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.Q)) { player.name = player.name + "q"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.R)) { player.name = player.name + "r"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.S)) { player.name = player.name + "s"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.T)) { player.name = player.name + "t"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.U)) { player.name = player.name + "u"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.V)) { player.name = player.name + "v"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.W)) { player.name = player.name + "w"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.X)) { player.name = player.name + "x"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.Y)) { player.name = player.name + "y"; }
                if (Input.IsKeyboardKeyDown(KeyboardInput.Z)) { player.name = player.name + "z"; }

                if (Input.IsKeyboardKeyDown(KeyboardInput.Backspace) && (player.name.Length > 0)) //Laggy inputs require a way for the player to correct their work
                {
                    player.name = player.name.Remove(player.name.Length - 1);
                }
            }
            if (screen == 2)
            {

                playerInputs();

                //Scales radius
                if (player.plyPosition.Y >= 200) { player.radius = 22; }
                if (player.plyPosition.Y >= 250) { player.radius = 25; }
                if (player.plyPosition.Y >= 300) { player.radius = 30; }
                if (player.plyPosition.Y >= 400) { player.radius = 40; }
                if (player.plyPosition.Y >= 500) { player.radius = 45; }
                if (player.plyPosition.Y >= 600) { player.radius = 50; }

                drawBackground();
                drawObjectLS();
                //Draw score
                Vector2 textPosition = new Vector2(44, 44);
                Text.Color = Color.White;
                int timer = 60 - Convert.ToInt32(Time.SecondsElapsed);

                Text.Draw(timer.ToString(), textPosition);

                drawPlayer();
                if (59 > 60 - Convert.ToInt32(Time.SecondsElapsed)) { drawStoppedCar(200, 100 + Convert.ToInt32(Time.SecondsElapsed * 100), 15); }
            }
        }

        /// <summary>
        /// Checks if player is making an input
        /// </summary>
        public void playerInputs()
        {
            float deadzone = 0.05f;
            if ((Input.IsKeyboardKeyDown(KeyboardInput.Right) == true) || (Input.GetAnyControllerAxis(ControllerAxis.LeftX, deadzone) > 0.10))
            {
                player.plyPosition.X += 8;
            }
            else if ((Input.IsKeyboardKeyDown(KeyboardInput.Left) == true) || (Input.GetAnyControllerAxis(ControllerAxis.LeftX, deadzone) < -0.10))
            {
                player.plyPosition.X -= 8;
            }
            if ((Input.IsKeyboardKeyDown(KeyboardInput.Down) == true) || (Input.GetAnyControllerAxis(ControllerAxis.LeftY, deadzone) > 0.10))
            {
                player.plyPosition.Y += 8;
            }
            else if ((Input.IsKeyboardKeyDown(KeyboardInput.Up) == true) || (Input.GetAnyControllerAxis(ControllerAxis.LeftY, deadzone) < -0.10))
            {
                player.plyPosition.Y -= 8;
            }
            if (player.plyPosition.Y <= 300) { player.plyPosition.Y = 301; }
            if (player.plyPosition.X < 90) { player.plyPosition.X = 90; }
            if (player.plyPosition.X > 780) { player.plyPosition.X = 780; }
            if (player.plyPosition.Y > 600) { player.plyPosition.Y = 600; }
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

            Draw.LineColor = Color.Green;
            Draw.FillColor = Color.Green;
            Vector2 groundPoint1 = new Vector2(0, 300);
            Vector2 groundPoint2 = new Vector2(200, 300);
            Vector2 groundPoint3 = new Vector2(0, 800);
            Draw.Triangle(groundPoint1, groundPoint2, groundPoint3);

            groundPoint1 = new Vector2(600, 300);
            groundPoint2 = new Vector2(800, 300);
            groundPoint3 = new Vector2(800, 800);
            Draw.Triangle(groundPoint1, groundPoint2, groundPoint3);

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
        public void drawPlayer()
        {
            //Wheels
            Draw.LineColor = Color.Gray;
            Draw.FillColor = Color.Gray;
            Vector2 playerWheel = new Vector2(player.plyPosition.X - 12, player.plyPosition.Y + 15);
            Draw.Circle(playerWheel, player.radius / 2);
            Vector2 playerWheel2 = new Vector2(player.plyPosition.X + 8, player.plyPosition.Y + 15);
            Draw.Circle(playerWheel2, player.radius / 2);

            Color playerColour = new Color(player.r, player.g, player.b);
            Draw.LineColor = playerColour;
            Draw.FillColor = playerColour;
            Vector2 playerMovement = new Vector2(player.plyPosition.X, player.plyPosition.Y);
            Vector2 playerMovement2 = new Vector2(100, 40);
            Draw.Rectangle(playerMovement, playerMovement2);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">X position of vehicle</param>
        /// <param name="y">Y position of vehicle</param>
        /// <param name="r">Radius of vehicle</param>
        public void drawStoppedCar(int x, int y, int r)
        {

            Draw.LineColor = Color.Red;
            Draw.FillColor = Color.Red;
            Vector2 enemyCar = new Vector2(x, y);
            Draw.Circle(enemyCar, r);
        }

        public void drawObjectLS()
        {

            if ((groundObjLSTimer <= 150) && (groundObjLSTimer > 100))
            {
                Graphics.Scale = 1;
                Graphics.Draw(playerTexture, groundObjLS1);
                
            }
            else if ((groundObjLSTimer <= 100) && (groundObjLSTimer > 50))
            {
                Graphics.Scale = 2;
                Graphics.Draw(playerTexture, groundObjLS2);
            }
            else if ((groundObjLSTimer <= 100) && (groundObjLSTimer > 0))
            {
                Graphics.Scale = 3 ;
                Graphics.Draw(playerTexture, groundObjLS3);
            }
            else if (groundObjLSTimer < 0)
            {
                groundObjLSTimer = 500;
                if (textureChoice == 1)
                {
                    textureFilePath = "MohawkGame2D\\Images\\StopSign.png";
                    playerTexture = Graphics.LoadTexture(textureFilePath);
                    textureChoice = 2;
                }
                else if (textureChoice == 2)
                {
                    textureFilePath = "MohawkGame2D\\Images\\FireHydrant.png";
                    playerTexture = Graphics.LoadTexture(textureFilePath);
                    textureChoice = 1;
                }

            }
            groundObjLSTimer -= 10;

        }
    }

}
