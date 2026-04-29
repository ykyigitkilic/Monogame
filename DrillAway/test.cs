using System;
using DrillAway;
using Microsoft.Xna.Framework;

class Test
{
    InputManager input;
    Game1 game1;
    enum Quarter
    {
        Quarter3,
        QuarterBuffer,
        Quarter1
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
        //Console.WriteLine(input.RightStick);
        Console.WriteLine(CurrentQuarter(input.RightStick));
    }
    Quarter CurrentQuarter(Vector2 vector)
    {
        int sideX = 0;
        int sideY = 0;
        if(vector.X>0) sideX++;
        if(vector.Y>0) sideY++;
        if(sideX+sideY==2)return Quarter.Quarter1;
        if(sideX+sideY==1)return Quarter.QuarterBuffer;
        return Quarter.Quarter3;
    }

}