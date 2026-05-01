using System;
using System.IO;
using System.Net.Mime;


class MapGenerator
{
    Random random = new Random();

    int tileWidth =100;
    int tileHeight=50;

    public void BackGroundGen()
    {
        string filepath="bg.csv";
        StreamWriter writer = new StreamWriter(filepath);
        for(int i =0;i<tileHeight;i++)
        {
            for(int a =0;a<tileWidth;a++)
            {
                var num=random.Next(10);
                if(a==tileWidth-1)
                {
                    writer.Write(num);
                }
                else
                {
                    writer.Write(num + ",");
                }
            }
            writer.WriteLine();
        }
        writer.Close();
    }

    public void MiddelGroundGen()
    {
        FastNoiseLite noise = new FastNoiseLite();
        noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
        noise.SetFrequency(0.1f); //no idea what it does exatcly???

        float threshold = 0.1f;//intensity for generation
        string filepath="mg.csv";
        StreamWriter writer = new StreamWriter(filepath);
        for (int y = 0; y < tileHeight; y++)
        {
            for (int x = 0; x < tileWidth; x++)
            {
                float val = noise.GetNoise(x, y);//outputs either 1 or -1
                int binaryValue = (val > threshold) ? 1 : 0;//clamps it down to 1-0 range with the threshold

                if (x==tileWidth-1)
                {
                    writer.Write(binaryValue);
                }
                else
                {
                    writer.Write(binaryValue+",");
                }
            }
            writer.WriteLine();
        }
        writer.Close();
    }
}