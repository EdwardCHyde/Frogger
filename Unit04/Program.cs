using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unit04.Game.Casting;
using Unit04.Game.Directing;
using Unit04.Game.Services;


namespace Unit04
{
    /// <summary>
    /// The program's entry point.
    /// </summary>
    class Program
    {
        private static int FRAME_RATE = 12;
        private static int MAX_X = 900;
        private static int MAX_Y = 600;
        private static int CELL_SIZE = 15;
        private static int FONT_SIZE = 15;
        private static int COLS = 60;
        private static int ROWS = 40;
        private static string CAPTION = "Robot Finds Kitten";
        private static string DATA_PATH = "Data/messages.txt";
        private static Color GREEN = new Color(0, 255, 0);

        private static Color WHITE = new Color(225, 255, 225);

         private static Color BROWN = new Color(181, 101, 29);
        public static int Rock = 40;
        private static int Gems = 40;


        /// <summary>
        /// Starts the program using the given arguments.
        /// </summary>
        /// <param name="args">The given arguments.</param>
        static void Main(string[] args)
        {
            // create the cast
            Cast cast = new Cast();
            Car cars = new Car(new Point(5,0));

            cast.AddActor("cars", cars);

            // create the banner
            Actor banner = new Actor();
            banner.SetText("");
            banner.SetFontSize(FONT_SIZE);
            banner.SetColor(WHITE);
            banner.SetPosition(new Point(CELL_SIZE, 0));
            cast.AddActor("banner", banner);

            // create the robot
            int newdx = 5;
            int newdy = 0;
            Point newdirection = new Point(newdx, newdy);
            Actor robot = new Actor();
            robot.SetText("#");
            robot.SetFontSize(FONT_SIZE);
            robot.SetColor(GREEN);
            robot.SetVelocity(newdirection);
            robot.SetPosition(new Point(MAX_X / 2, MAX_Y - 15));
            cast.AddActor("robot", robot);

        
    


            // load the messages
            List<string> messages = File.ReadAllLines(DATA_PATH).ToList<string>();

            // create the artifacts
            Random random = new Random();
            for (int i = 0; i < Rock; i++)
            {
                string text = "oo";
                int points = -100;

                int dx = 3;
                int dy = 0;

                Point direction = new Point(dx, dy);
                
                int x = random.Next(1, COLS);
                int y = random.Next(1, ROWS);
                Point position = new Point(x, y);
                position = position.Scale(CELL_SIZE);


                Rock log = new Rock();
                log.SetText(text);
                log.SetFontSize(FONT_SIZE);
                log.SetColor(BROWN);
                log.SetPosition(position);
                log.SetMessage(points);
                log.SetVelocity(direction);
                cast.AddActor("log", log);

             
            }

            
            // for (int i = 0; i < 50; i++)
            // {
            //     string text = "*-*";
            //     int points = 100;

            //     int dx = 5;
            //     int dy = 0;

            //     Point direction = new Point(dx, dy);

              
            //     // int x = random.Next(1, COLS);
            //     // int y = random.Next(1, ROWS);
            //     // Point position = new Point(x, y);
            //     // position = position.Scale(CELL_SIZE);

            //     int r = random.Next(0, 256);
            //     int g = random.Next(0, 256);
            //     int b = random.Next(0, 256);
            //     Color color = new Color(r, g, b);

            //     Rock artifact = new Rock();

            //     int min = random.Next(0, 39);
            //     int max = random.Next(0, 39);

                
            //     int x = random.Next(0,900);
            //     // int y = 300 + i*15;
            //     int y = random.Next(20,30) * 15;
            //     Point pos = new Point(x,y);
            //     artifact.SetPosition(pos);

            //     artifact.SetText(text);
            //     artifact.SetFontSize(FONT_SIZE);
            //     artifact.SetColor(color);
            //     // artifact.SetPosition(position);
            //     artifact.SetMessage(points);
            //     artifact.SetVelocity(direction);
            //     cast.AddActor("artifacts", artifact);
            // }

      


            // start the game
            KeyboardService keyboardService = new KeyboardService(CELL_SIZE);
            VideoService videoService 
                = new VideoService(CAPTION, MAX_X, MAX_Y, CELL_SIZE, FRAME_RATE, false);
            Director director = new Director(keyboardService, videoService);
            director.StartGame(cast);

            // test comment
        }
    }
}