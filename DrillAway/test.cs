using System;
using System.Diagnostics;
using DrillAway;

class Test
{
    InputManager input;
    public Test()
    {
        Console.WriteLine("test has been initiated");
    }
    public Test(InputManager inputManager)
    {
        input = inputManager;
        Console.WriteLine("test has been initiated");
        input.OnButtonB += ExitTest;
    }
    public void ExitTest(object sender,EventArgs e)
    {
        Console.WriteLine("exitted??");
    }
}