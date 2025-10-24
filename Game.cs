// Include the namespaces (code libraries) you need below.
using System;
using System.Drawing;
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

        Vector2 groundObjLS1 = new Vector2(75, 0);
        Vector2 groundObjLS2 = new Vector2(75, 100);
        Vector2 groundObjLS3 = new Vector2(75, 200);
        Vector2 groundObjLS4 = new Vector2(75, 300);
        Vector2 groundObjLS5 = new Vector2(75, 400);
        Vector2 groundObjLS6 = new Vector2(75, 500);
        Vector2 groundObjLS7 = new Vector2(75, 600);
        int groundObjLSTimer = 500;

        //Second sidwalk line

        Vector2 groundObjRS1 = new Vector2(725, 0);
        Vector2 groundObjRS2 = new Vector2(725, 100);
        Vector2 groundObjRS3 = new Vector2(725, 200);
        Vector2 groundObjRS4 = new Vector2(725, 300);
        Vector2 groundObjRS5 = new Vector2(725, 400);
        Vector2 groundObjRS6 = new Vector2(725, 500);
        Vector2 groundObjRS7 = new Vector2(75, 600);



        int screen = 1; //Will determine what screen to show
        
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
            Window.TargetFPS = 60;


            textureFilePath = "MohawkGame2D\\Images\\StopSign.png";
            playerTexture = Graphics.LoadTexture(textureFilePath);

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

            }
            if (screen == 2)
            {

                playerInputs();

                drawBackground();

                drawObjectLS();

                drawPlayer();
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
                player.plyPosition.X = player.plyPosition.X + player.acceleration / 10;

            }
            else if ((Input.IsKeyboardKeyDown(KeyboardInput.Left) == true) || (Input.GetAnyControllerAxis(ControllerAxis.LeftX, deadzone) < -0.10))
            {
                player.plyPosition.X = player.plyPosition.X - player.acceleration / 10;
            }
            if ((Input.IsKeyboardKeyDown(KeyboardInput.Down) == true) || (Input.GetAnyControllerAxis(ControllerAxis.LeftY, deadzone) > 0.10))
            {
                player.plyPosition.Y = player.plyPosition.Y + player.acceleration / 10; ;
            }
            else if ((Input.IsKeyboardKeyDown(KeyboardInput.Up) == true) || (Input.GetAnyControllerAxis(ControllerAxis.LeftY, deadzone) < -0.10))
            {
                player.plyPosition.Y = player.plyPosition.Y  - player.acceleration / 10; 
            }
            if ((Input.IsKeyboardKeyDown(KeyboardInput.W) == true) || (Input.IsAnyControllerButtonDown(ControllerButton.RightTrigger2) == true))
            {
                player.acceleration += 2;

            }
            else if ((Input.IsKeyboardKeyDown(KeyboardInput.S) == true) || (Input.IsAnyControllerButtonDown(ControllerButton.LeftTrigger2) == true))
            {
                player.acceleration -= 2;

            }

            if (player.plyPosition.Y < 0) { player.plyPosition.Y = 0; }
            if (player.plyPosition.Y > 540) { player.plyPosition.Y = 540; }

            if (player.plyPosition.X < 154) { player.plyPosition.X = 154; }
            if (player.plyPosition.X > 604) { player.plyPosition.X = 604; }
        


        }

        /// <summary>
        /// Draws the basic background colours
        /// </summary>
        public void drawBackground()
        {
            //Draws the grass
            Vector2 originPoint = new Vector2(0, 0);
            Vector2 originSize = new Vector2(800, 600);
            Color grass = new Color(50, 175, 0);
            Draw.LineColor = grass;
            Draw.FillColor = grass;
            Draw.Rectangle(originPoint, originSize);

            //Draws the street
            Draw.LineColor = Color.Black;
            Draw.FillColor = Color.Black;
            originPoint = new Vector2(150, 0);
            originSize = new Vector2(500, 600);
            Draw.Rectangle(originPoint, originSize);

             
                 //Draw score
                Vector2 textPosition = new Vector2(44, 44);
                Text.Color = Color.White;
                int timer = 60 - Convert.ToInt32(Time.SecondsElapsed);

                Text.Draw(timer.ToString(), textPosition);
        }

        /// <summary>
        /// Draws the player
        /// </summary>
        public void drawPlayer()
        {
            //Draws the tires
            //Tire 1
            Draw.LineColor = Color.Gray;
            Draw.FillColor = Color.Gray;
            Vector2 playerMovement = new Vector2(player.plyPosition.X - 6, player.plyPosition.Y + 2);
            Vector2 playerMovement2 = new Vector2(6, 20);
            Draw.Rectangle(playerMovement, playerMovement2);

            //Tire 2
            playerMovement = new Vector2(player.plyPosition.X + 40, player.plyPosition.Y + 2);
            playerMovement2 = new Vector2(6, 20);
            Draw.Rectangle(playerMovement, playerMovement2);

            //Tire 3
            playerMovement = new Vector2(player.plyPosition.X - 6 , player.plyPosition.Y + 52);
            playerMovement2 = new Vector2(6, 20);
            Draw.Rectangle(playerMovement, playerMovement2);

            //Tire 4
            playerMovement = new Vector2(player.plyPosition.X + 40, player.plyPosition.Y + 52);
            playerMovement2 = new Vector2(6, 20);
            Draw.Rectangle(playerMovement, playerMovement2);

            //Draw the main vehicle
            Color playerColour = new Color(player.r, player.g, player.b);
            Draw.LineColor = playerColour;
            Draw.FillColor = playerColour;
            playerMovement = new Vector2(player.plyPosition.X, player.plyPosition.Y);
            playerMovement2 = new Vector2(40, 75);
            Draw.Rectangle(playerMovement, playerMovement2);
        }

 
        /// <summary>
        /// 
        /// </summary>
        public void drawObjectLS()
        {

            if ((groundObjLSTimer <= 600) && (groundObjLSTimer > 500))
            {
          
                Graphics.Draw(playerTexture, groundObjLS1);
                Graphics.Draw(playerTexture, groundObjRS1);

            }
            else if ((groundObjLSTimer <= 500) && (groundObjLSTimer > 400))
            {
            
                Graphics.Draw(playerTexture, groundObjLS2);
                Graphics.Draw(playerTexture, groundObjRS2);
            }
            else if ((groundObjLSTimer <= 400) && (groundObjLSTimer > 300))
            {
            
                Graphics.Draw(playerTexture, groundObjLS3);
                Graphics.Draw(playerTexture, groundObjRS3);
            }
            else if ((groundObjLSTimer <= 300) && (groundObjLSTimer > 200))
            {

                Graphics.Draw(playerTexture, groundObjLS4);
                Graphics.Draw(playerTexture, groundObjRS4);
            }
            else if ((groundObjLSTimer <= 200) && (groundObjLSTimer > 100))
            {

                Graphics.Draw(playerTexture, groundObjLS5);
                Graphics.Draw(playerTexture, groundObjRS5);
            }
            else if ((groundObjLSTimer <= 100) && (groundObjLSTimer > 0))
            {

                Graphics.Draw(playerTexture, groundObjLS6);
                Graphics.Draw(playerTexture, groundObjRS6);
            }
            else if (groundObjLSTimer < 0)
            {
                groundObjLSTimer = 1000;
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
            int abc = groundObjLSTimer / 100;

            Draw.LineColor = Color.Gray;
            Draw.FillColor = Color.Gray;
            if (abc % 2 == 1)
            {

                Draw.Line(new Vector2(400, 50), new Vector2(400, 100));
                Draw.Line(new Vector2(400, 150), new Vector2(400, 200));
                Draw.Line(new Vector2(400, 250), new Vector2(400, 300));
                Draw.Line(new Vector2(400, 350), new Vector2(400, 400));
                Draw.Line(new Vector2(400, 450), new Vector2(400, 500));
                Draw.Line(new Vector2(400, 550), new Vector2(400, 600));
                Draw.Line(new Vector2(400, 650), new Vector2(400, 700));
                Draw.Line(new Vector2(400, 750), new Vector2(400, 800));
                Draw.Line(new Vector2(400, 850), new Vector2(400, 900));
            }
            else if (abc % 2 == 0){

                Draw.Line(new Vector2(400, 0), new Vector2(400, 50));
                Draw.Line(new Vector2(400, 100), new Vector2(400, 150));
                Draw.Line(new Vector2(400, 200), new Vector2(400, 250));
                Draw.Line(new Vector2(400, 300), new Vector2(400, 350));
                Draw.Line(new Vector2(400, 400), new Vector2(400, 450));
                Draw.Line(new Vector2(400, 500), new Vector2(400, 550));
                Draw.Line(new Vector2(400, 600), new Vector2(400, 650));
                Draw.Line(new Vector2(400, 700), new Vector2(400, 750));
                Draw.Line(new Vector2(400, 800), new Vector2(400, 850));

            }

        }


    }

}
