using System.Collections.Generic;
using Unit04.Game.Casting;
using Unit04.Game.Services;
using System;
using Raylib_cs;



namespace Unit04.Game.Directing
{
    /// <summary>
    /// <para>A person who directs the game.</para>
    /// <para>
    /// The responsibility of a Director is to control the sequence of play.
    /// </para>
    /// </summary>
    public class Director
    {
        private KeyboardService keyboardService = null;
        private VideoService videoService = null;

        public int points = 0;

        public Casting.Color WHITE = new Casting.Color(225, 255, 225);
        
        

        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService)
        {
            this.keyboardService = keyboardService;
            this.videoService = videoService;
        }

        /// <summary>
        /// Starts the game by running the main game loop for the given cast.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame(Cast cast)
        {
            videoService.OpenWindow();
            while (videoService.IsWindowOpen())
            {
                GetInputs(cast);
                DoUpdates(cast);
                DoOutputs(cast);
            }
            videoService.CloseWindow();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the robot.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            Actor robot = cast.GetFirstActor("robot");
            Point velocity = keyboardService.GetDirection();
       
            

            List<Actor> log = cast.GetActors("log");
            List<Actor> artifacts = cast.GetActors("artifacts");



            int newdx = 3;
            int newdy = 0;
          
            
            Point newdirection = new Point(newdx, newdy);
            Point zeroDirection = new Point(newdy, newdy);

            
             
            robot.SetVelocity(velocity); 

            foreach (Actor actor in log)
            {

                if (robot.GetPosition().Equals(actor.GetPosition()))
                {
                    robot.SetVelocity2(newdirection);

                }

        
            }
            
            // Game over 
            foreach (Actor actor in artifacts)
            {
                Point robotPosition = robot.GetPosition();
                int x = robotPosition.GetX();
                int y = robotPosition.GetY();
                Rectangle RobotRecs = new Rectangle(x, y, 15, 15);

                Point actorPosition = actor.GetPosition();
                int x2 = actorPosition.GetX();
                int y2 = actorPosition.GetY();
                Rectangle actorRecs = new Rectangle(x2, y2, 30, 15);

                if (Raylib.CheckCollisionRecs(RobotRecs, actorRecs))
                {
               

                    
                    int MaxX = videoService.GetWidth();
                    int MaxY = videoService.GetHeight();
                    int X = MaxX / 2;
                    int Y = MaxY / 2;
                    Point position = new Point(X, Y);

                    Actor message = new Actor();
                    message.SetText("Game Over!");
                    message.SetPosition(position);
                    cast.AddActor("messages", message);
                    robot.SetText("X");

                    //set each car to white 
                    robot.SetColor(WHITE);
                    foreach (Actor actors in artifacts)
                    {
                        actors.SetColor(WHITE);
                    }

                    // set each log to white
                    foreach (Actor actors in log)
                    {
                        actors.SetColor(WHITE);
                    }


                }
            }



            
               

         

            

            





            
    
     

            
            
    

        }

        /// <summary>
        /// Updates the robot's position and resolves any collisions with artifacts.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
            Actor banner = cast.GetFirstActor("banner");
            Actor robot = cast.GetFirstActor("robot");
            Car cars = (Car)cast.GetFirstActor("cars");
            List<Actor> log = cast.GetActors("log");
            List<Actor> carslist = cars.GetCars();


            banner.SetText($"Score: {points}");
            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            robot.MoveNext(maxX, maxY);

            foreach (Actor actor in carslist)
            {
                actor.MoveNext(maxX, maxY);
                if (robot.GetPosition().Equals(actor.GetPosition()))
                {
                    
                    Rock artifact = (Rock) actor;
                    int points = artifact.GetMessage();
                    this.points += points;
                    banner.SetText($"Score: {points}");


        


                }

                if (actor.GetPosition().GetY() == (maxY -10))
                {
                    Car car = (Car) actor;
                    
                    
                    int x = car.GetPosition().GetX();
                    int y = car.GetPosition().GetY();
                    Random random = new Random();
                    int ranX = random.Next(0, maxX) * 15;
                    
        

                    Point postition = new Point(ranX,y);
                    car.SetPosition(postition);
                     
                }

                
            } 

            foreach(Actor car in carslist)
            {
                int Newdx = 10;
                int Newdy = 0;
                Point NewDirection = new Point(Newdx, Newdy);

         
               

            
                  
                if (robot.GetPosition().GetY() <10)
                {
                
                    car.SetVelocity(NewDirection); 
                }
            }
           

            


            foreach (Actor actor in log)
            {
                actor.MoveNext(maxX, maxY);
              

                if (actor.GetPosition().GetY() == (maxY -10))
                {
                    Rock logs = (Rock) actor;
                    
                    
                    int x = logs.GetPosition().GetX();
                    int y = logs.GetPosition().GetY();
                    Random random = new Random();
                    int ranX = random.Next(0, maxX) * 15;
                    
        

                    Point postition = new Point(ranX,y);
                    logs.SetPosition(postition);
                }

                
            } 



            //Set the the velocity to back to 0 once the frog leaves the log s
            int newdx = 5;
            int newdy = 0;
        
            Point newdirection = new Point(newdx, newdy);
            Point zeroDirection = new Point(newdy, newdy);
        

            foreach (Actor actor in log)
            {

            

                if (robot.GetPosition()!= actor.GetPosition())
                {
                    robot.SetVelocity2(zeroDirection); 
                }
            }
        }

        /// <summary>
        /// Draws the actors on the screen.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void DoOutputs(Cast cast)
        {
            List<Actor> actors = cast.GetAllActors();
            videoService.ClearBuffer();
            videoService.DrawActors(actors);
            videoService.FlushBuffer();

        }

    }
}



