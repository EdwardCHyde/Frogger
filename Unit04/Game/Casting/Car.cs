using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unit04.Game.Casting;
using Unit04.Game.Directing;
using Unit04.Game.Services;
namespace Unit04.Game.Casting

{
    public class Car : Actor
{
    List<Actor> cars = new List<Actor>();
    public Car(Point direction)
    {

        PrepareCars(direction);
    }

    public void PrepareCars(Point direction)
    {
        // cars.Clear();
        Random random = new Random();
        for (int i = 0; i < 50; i++)
            {
                string text = "*-*";
                // int points = 100;

              
                

              
                // int x = random.Next(1, COLS);
                // int y = random.Next(1, ROWS);
                // Point position = new Point(x, y);
                // position = position.Scale(CELL_SIZE);

                int r = random.Next(0, 256);
                int g = random.Next(0, 256);
                int b = random.Next(0, 256);
                Color color = new Color(r, g, b);

                Actor car = new Actor();
                

                int min = random.Next(0, 39);
                int max = random.Next(0, 39);
                
                
                int x = random.Next(0,900);
                // int y = 300 + i*15;
                int y = random.Next(20,30) * 15;
                Point pos = new Point(x,y);
                car.SetPosition(pos);

                car.SetText(text);
                car.SetFontSize(15);
                car.SetColor(color);
                // car.SetPosition(position);
                // car.SetMessage(points);
                car.SetVelocity(new Point(5,0));
            
            }



    }

    public List<Actor> GetCars()
    {
        return cars;
    }

}

}
