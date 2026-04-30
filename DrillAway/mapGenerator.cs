using System;


class MapGenerator
{
    Random random = new Random();

    int tileWidth =100;
    int tileHeight=50;

    public void BackGroundGen()
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

    public void MiddelGroundGen()
    {
        FastNoiseLite noise = new FastNoiseLite();
        noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
        noise.SetFrequency(0.05f); //no idea what it does exatcly???

        float threshold = 0.2f;//intensity for generation

        for (int y = 0; y < tileHeight; y++)
        {
            for (int x = 0; x < tileWidth; x++)
            {
                float val = noise.GetNoise(x, y);//outputs either 1 or -1
                int binaryValue = (val > threshold) ? 1 : 0;

                if (x==tileWidth-1)
                {
                    Console.Write(binaryValue);
                }
                else
                {
                    Console.Write(binaryValue+",");
                }
            }
            Console.WriteLine();
        }
    }
}