using System;


class MapGenerator
{
    Random random = new Random();

    int tileWidth =10;
    int tileHeight=10;

    public void BGGen()
    {
        for(int i =0;i<tileHeight;i++)
        {
            for(int a =0;a<tileWidth;a++)
            {
                if(a==tileWidth-1)
                {
                    Console.Write(random.Next(10));
                }
                else
                {
                    Console.Write(random.Next(10)+",");
                }
            }
            Console.WriteLine();
        }
    }
}