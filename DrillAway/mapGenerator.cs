using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;

class MapGenerator
{
    Random random = new Random();
    public int tileWidth =4000;
    public int tileHeight=4000;

    public void BackGroundGen()
    {
        string filepath="bg.csv";
        StreamWriter writer = new StreamWriter(filepath);
        for(int i =0;i<tileHeight;i++)
        {
            for(int a =0;a<tileWidth;a++)
            {
                var num=random.Next(2);
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
        noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
        noise.SetFrequency(0.06f);

        float threshold = 0.3f;//intensity for generation
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
    // dictionary yerine 2D array (int[,]) kullanıyoz
    public int[,] tilemapBG;
    public int[,] tilemapMG;

    public int[,] loadTileMap(string filepath)
    {
        int[,] result = new int[tileWidth, tileHeight];

        StreamReader reader = new(filepath);
        string line;
        int y = 0;
        
        while ((line = reader.ReadLine()) != null && y < tileHeight)
        {
            string[] items = line.Split(',');
            for (int x = 0; x < items.Length && x < tileWidth; x++)
            {
                if (int.TryParse(items[x], out int value))
                {
                    result[x, y] = value; 
                }
            }
            y++;
        }
        return result;
    }
}