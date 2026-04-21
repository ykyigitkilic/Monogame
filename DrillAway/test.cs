using System;
using DrillAway;

class Test
{
    InputManager input;
    Game1 game1;
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
}