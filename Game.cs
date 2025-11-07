// Include the namespaces (code libraries) you need below.
using System;
using System.Drawing;
using System.Net.Sockets;
using System.Numerics;
using System.Threading;

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
        Rocket rocket = new Rocket();
        
        //Street lines
        //First sidewalk line
        //Each space is used to create an illusion of an object moving down the screen so the player appears to be driving forward
        Vector2 groundObjLS1 = new Vector2(50, 0);
        Vector2 groundObjLS2 = new Vector2(50, 100);
        Vector2 groundObjLS3 = new Vector2(50, 200);
        Vector2 groundObjLS4 = new Vector2(50, 300);
        Vector2 groundObjLS5 = new Vector2(50, 400);
        Vector2 groundObjLS6 = new Vector2(50, 500);
        int groundObjLSTimer = 500;

        //Second sidwalk line
        Vector2 groundObjRS1 = new Vector2(700, 0);
        Vector2 groundObjRS2 = new Vector2(700, 100);
        Vector2 groundObjRS3 = new Vector2(700, 200);
        Vector2 groundObjRS4 = new Vector2(700, 300);
        Vector2 groundObjRS5 = new Vector2(700, 400);
        Vector2 groundObjRS6 = new Vector2(700, 500);

        //Enemy frog information
        int frogCounter = 100;
        Frog frog;
        bool isFrog = false;

        int screen = 1; //Will determine what screen to show

        int textureChoice = 1; //Will choose what texture to load
        string[] textureFilePath = new string[3];
        Texture2D playerTexture;

        //Will spawn a permanent point multiplier for bonus points
        Vector2 pointsPosition;
        bool isPoints;


        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetSize(800, 600);
            Window.SetTitle("Assignment 3 - Frogn't");
            Window.TargetFPS = 60;


            textureFilePath[0] = "MohawkGame2D\\Images\\StopSign.png";
            textureFilePath[1] = "MohawkGame2D\\Images\\FireHydrant.png";
            textureFilePath[2] = "MohawkGame2D\\Images\\Bush.png";
            playerTexture = Graphics.LoadTexture(textureFilePath[0]);

        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.Black);

            if (screen == 1) //Main Menu
            {
                
                Text.Size = 30;
                if ((Input.IsKeyboardKeyReleased(KeyboardInput.Enter) == true) || (Input.IsAnyControllerButtonPressed(ControllerButton.MiddleRight) == true)){screen = 2;}
                if ((Input.IsKeyboardKeyReleased(KeyboardInput.RightShift) == true) || (Input.IsAnyControllerButtonPressed(ControllerButton.MiddleLeft) == true))
                {
                    screen = 6;
                }
                Text.Size = 50;
                Text.Color = Color.Green;
                player.Setup();
                Text.Draw("Frogn't", new Vector2(44, 44));
                Text.Size = 30;
                Text.Color = Color.White;
                Text.Draw("Use a keyboard or controller!", new Vector2(25, 300));
                Text.Draw("Press 'enter' or 'start'", new Vector2(25, 350));

                
                Text.Draw("press 'right shift' or 'select' to read plot", new Vector2(25, 400));


                Text.Draw("Created by: Liam McDougall", new Vector2(25, 575));



            }
            else if (screen == 2) //Controls menu
            {
                
                if ((Input.IsKeyboardKeyReleased(KeyboardInput.Enter) == true) || (Input.IsAnyControllerButtonPressed(ControllerButton.MiddleRight) == true))
                {
                    screen = 3;
                    Time.SecondsElapsed = 0;
                    
                }

                Text.Draw("Controls:", new Vector2(10, 100));
                Text.Draw("Accelerate: W key or Right Trigger", new Vector2(10, 150));
                Text.Draw("De-accelerate: S key or Left Trigger", new Vector2(10, 200));
                Text.Draw("Shoot rocket: D key or Right Face Down Button", new Vector2(10, 250));
                Text.Draw("Move: D-Pad or Left Analog Stick", new Vector2(10, 300));
                Text.Draw("Collect Red Score Multipliers", new Vector2(10, 350));
                Text.Draw("Press 'enter' or 'start' to begin!", new Vector2(10, 550));

            }

            else if (screen == 3) //Main Gameplay
            {
                
                RocketLaunch();
                DrawBackground();
                player.Update();
                
                DrawRocket();
                DrawObjectLS();
                
                DrawMultiplier();
                DrawFrog();
                player.DrawPlayer();
                CheckWin();
            }

            else if (screen == 4)//Good Ending
            {
                Text.Size = 30;
                //Reset all player stats
                
                Text.Draw("You win! You killed enough frogs!", new Vector2(40, 50));
                Text.Draw("Score: " + player.score, new Vector2(40, 150));
                Text.Draw("Press 'enter' to restart", new Vector2(40, 300));
                if ((Input.IsKeyboardKeyReleased(KeyboardInput.Enter) == true) || (Input.IsAnyControllerButtonPressed(ControllerButton.MiddleRight) == true))
                {
                    screen = 1;
                }
                
            }
            else if (screen == 5) //Bad Ending
            {
                Text.Size = 30;
                //Reset all player stats
                
                Text.Draw("You failed! The frogs won, nuclear winter \nhas begun...", new Vector2(40, 50));
                Text.Draw("Score: " + player.score, new Vector2(40, 150));
                Text.Draw("Press 'enter' to restart", new Vector2(40, 300));
                if ((Input.IsKeyboardKeyReleased(KeyboardInput.Enter) == true) || (Input.IsAnyControllerButtonPressed(ControllerButton.MiddleRight) == true))
                {
                    screen = 1;
                }
                
            }
            else if (screen == 6) //Plot Synopsis Menu
            {

                if ((Input.IsKeyboardKeyReleased(KeyboardInput.Enter) == true) || (Input.IsAnyControllerButtonPressed(ControllerButton.MiddleRight) == true))
                {
                    screen = 1;
                }

                Text.Draw("It is the year 50XX... frogs have discovered \n"
                        + "nuclear weapons. They have found the required \n" +
                        "launch codes. Now, their goal is to get to the \n" +
                        "launch pad and enter the codes. Your goal is to \n" +
                        "stop nuclear winter. Kill every frog you see, \n" +
                        "either run them over or launch your rockets and \n" +
                        "blow them back to France...", new Vector2(10, 40));

               

            }
        }

        /// <summary>
        /// Checks if player is trying to launch a rocket
        /// </summary>
        public void RocketLaunch()
        {            
            if ((Input.IsKeyboardKeyDown(KeyboardInput.D) == true) || (Input.IsAnyControllerButtonDown(ControllerButton.RightFaceDown) == true))
            {
                if (rocket.isRocket == false)
                {
                    rocket = new Rocket(Convert.ToInt32(player.plyPosition.X) + 10, Convert.ToInt32(player.plyPosition.Y));
                    rocket.isRocket = true;
                }

            }


        }

        /// <summary>
        /// Draws the basic background colours
        /// </summary>
        public void DrawBackground()
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


            //Draw score and time
            Text.Color = Color.White;
            int timer = 90 - Convert.ToInt32(Time.SecondsElapsed);
            Text.Size = 20;
            Text.Draw("Time Left: " + timer.ToString(), new Vector2(5, 45));
            if (player.evilFrogsKilled <= 30) { Text.Draw("Frogs To Kill: " + (30 - player.evilFrogsKilled).ToString(), new Vector2(5, 65)); }
            Text.Draw("Score: " + player.score.ToString(), new Vector2(5, 85));

        }




        /// <summary>
        /// Draws objects on the grass on the side of the road at different times to simulate moving
        /// </summary>
        public void DrawObjectLS()
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
                    playerTexture = Graphics.LoadTexture(textureFilePath[0]);
                    textureChoice = 2;
                }
                else if (textureChoice == 2)
                {
                    
                    playerTexture = Graphics.LoadTexture(textureFilePath[1]);
                    textureChoice = 3;
                }
                else if (textureChoice == 3)
                {
                    
                    playerTexture = Graphics.LoadTexture(textureFilePath[2]);
                    textureChoice = 1;
                }

            }
            groundObjLSTimer -= 10;

            //Draws the grey street lines
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
            else if (abc % 2 == 0)
            {
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
        
        /// <summary>
        /// Draws the frog enemies.
        /// </summary>
        public void DrawFrog()
        {
            if ((isFrog == false) && (frogCounter <= 0))
            {
                int randomX = Random.Integer(175, 525);
                frog = new Frog(randomX, 0);
                isFrog = true;
            }
            else if ((isFrog == true) && (frogCounter <= 0))
            {
                frog.position.Y += 8;
                Draw.LineColor = Color.Green;
                Draw.FillColor = Color.Green;
                Draw.Circle(frog.position, 10);
                if (frog.position.Y > 600)
                {
                    isFrog = false;
                    frogCounter = 50;
                }

                if ((rocket.isRocket == true) && (frog.position.Y - 10 < rocket.position.Y) && (rocket.position.Y < frog.position.Y + 10))
                {
                    if ((frog.position.X - 10 < rocket.position.X) && (rocket.position.X < frog.position.X + 10))
                    {
                        isFrog = false;
                        player.score += (20 * player.multiplyer);
                        player.evilFrogsKilled++;
                        frogCounter = 50;
                        
                    }
                }
                if ((frog.position.X > player.plyPosition.X - 4) && (player.plyPosition.X + 48 > frog.position.X))
                {
                    if ((frog.position.Y - 10 < player.plyPosition.Y) && (player.plyPosition.Y < frog.position.Y + 10))
                    {
                        isFrog = false;
                        player.score += (5 * player.multiplyer);
                        player.evilFrogsKilled++;
                        frogCounter = 50;
                       
                    }
                }
            }
            else
            {
                frogCounter--;
            }


        }

        /// <summary>
        /// Draws the multiplier orb the player can collect
        /// </summary>
        public void DrawMultiplier()
        {
            
            if ((Convert.ToInt16(Time.SecondsElapsed) % 30 == 0) && (isPoints == false) && (Convert.ToInt16(Time.SecondsElapsed) > 5))
            {

                pointsPosition = new Vector2(Random.Integer(165, 525), 0);
                isPoints = true;
            }
            if (isPoints == true)
            {

                pointsPosition.Y += 10;
                Draw.LineColor = Color.Red;
                Draw.FillColor = Color.Red;
                Draw.Circle(pointsPosition, 10);
                if ((pointsPosition.X > player.plyPosition.X) && (player.plyPosition.X + 40 > pointsPosition.X))
                {
                    if ((pointsPosition.Y - 10 < player.plyPosition.Y) && (player.plyPosition.Y < pointsPosition.Y + 10))
                    {
                        player.multiplyer++;
                        isPoints = false;
                        Time.SecondsElapsed++;
                    }
                }
                if(pointsPosition.Y > 600){ isPoints = false; Time.SecondsElapsed++; }

            }


        }


        /// <summary>
        /// Checks if the player has killed enough frogs
        /// </summary>
        public void CheckWin(){
            if(Convert.ToInt16(Time.SecondsElapsed) > 90){
                if (player.evilFrogsKilled >= 30) { screen = 4; }
                else if (player.evilFrogsKilled < 30) { screen = 5; }
                
            } 
        }

        /// <summary>
        /// Draws the player's rocket attack
        /// </summary>
        public void DrawRocket()
        {

            //If rocket has been launched
            if (rocket.isRocket == true)
            {
                
                rocket.position.Y -= 15;
                Draw.LineColor = Color.Blue;
                Draw.FillColor = Color.Blue;
                Draw.Rectangle(rocket.position, rocket.size);

                if (rocket.position.Y < 0) { rocket.isRocket = false;  }
            }
        }
    }

}
