using System;
using DrillAway;
using Microsoft.Xna.Framework;

class Test
{
    InputManager input;
    Game1 game1;
    Quarter lastQuarter;
    enum Quarter
    {
        None,
        Quarter1,
        Quarter2,
        Quarter3,
        Quarter4
    }
    public Test()
    {
        Console.WriteLine("test has been initiated");
    }
    public Test(InputManager inputManager,Game1 _game1)
    {
        input = inputManager;
        game1 = _game1;
        Console.WriteLine("test has been initiated");
        input.OnButtonB += ExitTest;
    }
    public void ExitTest(object sender,EventArgs e)
    /*
    game1 içinde exit komutunun olması yerine artık obje kullanarak
    exit atabilioz cool af
    */
    {
        Console.WriteLine("exitted??");
        game1.Exit();
    }
    public void trackRotation()
    {
        var current = CurrentQuarter(input.RightStick);
        if(lastQuarter!=current)
        {
            Console.WriteLine(current);
            if(rotationCheck(current))
            {
                Console.WriteLine("babag dogru yon devam");
            }
            else
            {
                Console.WriteLine("noo");
            }
        }
        lastQuarter=current;
    }
    Quarter CurrentQuarter(Vector2 vector)
    {
        if (vector.X >= 0 && vector.Y > 0) return Quarter.Quarter1;
        if (vector.X > 0 && vector.Y <= 0) return Quarter.Quarter4;
        if (vector.X <= 0 && vector.Y < 0) return Quarter.Quarter3;
        if (vector.X < 0 && vector.Y >= 0) return Quarter.Quarter2;
        return Quarter.None;
    }
    bool rotationCheck(Quarter current)
    {
        return
        (lastQuarter == Quarter.Quarter1 && current == Quarter.Quarter4) ||
        (lastQuarter == Quarter.Quarter4 && current == Quarter.Quarter3) ||
        (lastQuarter == Quarter.Quarter3 && current == Quarter.Quarter2) ||
        (lastQuarter == Quarter.Quarter2 && current == Quarter.Quarter1);
    }
}