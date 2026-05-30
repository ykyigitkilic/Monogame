using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Ant
{
    public int X { get; set; }
    public int Y { get; set; }
    public float MoveTimer { get; set; }
    public int Direction { get; set; } // 0=Up, 1=Right, 2=Down, 3=Left

    public Ant(int x, int y, int startDirection)
    {
        X = x;
        Y = y;
        MoveTimer = 0f;
        Direction = startDirection;
    }
}

public class AntManager
{
    public List<Ant> Ants { get; private set; } = new List<Ant>();
    private Random random = new Random();
    public int AntsKilled=0;

    public void antCreate(int[,] middleGroundMap, int mapWidth, int mapHeight, int maxAntsToSpawn)
    {
        Ants.Clear();
        int antsSpawned = 0;

        while (antsSpawned < maxAntsToSpawn)
        {
            int rx = random.Next(0, mapWidth);
            int ry = random.Next(0, mapHeight);

            // If it's land (1), spawn an ant and update the map to be an ant (2)
            if (middleGroundMap[rx, ry] == 1)
            {
                int startDir = random.Next(0, 4);
                Ants.Add(new Ant(rx, ry, startDir));
                
                // INJECT INTO ARRAY
                middleGroundMap[rx, ry] = 2; 
                
                antsSpawned++;
            }
        }
    }

    public void antMove(GameTime gameTime, int[,] middleGroundMap, int mapWidth, int mapHeight)
    {
        float timeBetweenMoves = 0.15f; 

        foreach (var ant in Ants)
        {
            ant.MoveTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (ant.MoveTimer >= timeBetweenMoves)
            {
                ant.MoveTimer = 0f;

                int nextX = ant.X;
                int nextY = ant.Y;

                // Move in a straight line
                if (ant.Direction == 0) nextY -= 1;
                else if (ant.Direction == 1) nextX += 1;
                else if (ant.Direction == 2) nextY += 1;
                else if (ant.Direction == 3) nextX -= 1;

                // If path is clear (must be a 1!)
                if (IsWalkable(nextX, nextY, mapWidth, mapHeight, middleGroundMap))
                {
                    // 1. Turn old tile back to land
                    middleGroundMap[ant.X, ant.Y] = 1; 
                    
                    // 2. Update ant data
                    ant.X = nextX;
                    ant.Y = nextY;
                    
                    // 3. Turn new tile into an ant
                    middleGroundMap[ant.X, ant.Y] = 2; 
                }
                else
                {
                    // We hit a wall (0) or the edge of the map. Find a new direction!
                    List<int> validDirections = new List<int>();

                    if (IsWalkable(ant.X, ant.Y - 1, mapWidth, mapHeight, middleGroundMap)) validDirections.Add(0);
                    if (IsWalkable(ant.X + 1, ant.Y, mapWidth, mapHeight, middleGroundMap)) validDirections.Add(1);
                    if (IsWalkable(ant.X, ant.Y + 1, mapWidth, mapHeight, middleGroundMap)) validDirections.Add(2);
                    if (IsWalkable(ant.X - 1, ant.Y, mapWidth, mapHeight, middleGroundMap)) validDirections.Add(3);

                    if (validDirections.Count > 0)
                    {
                        ant.Direction = validDirections[random.Next(validDirections.Count)];
                    }
                }
            }
        }
    }

    /// <summary>
    /// Checks a specific grid coordinate. If an ant occupies it, it is removed from the tracking list and world array.
    /// </summary>
    public void KillAntAt(int gridX, int gridY, int[,] middleGroundMap, int mapWidth, int mapHeight)
    {
        // 1. Safety boundary check
        if (gridX >= 0 && gridX < mapWidth && gridY >= 0 && gridY < mapHeight)
        {
            // 2. Check if the tile value is an ant (2)
            if (middleGroundMap[gridX, gridY] == 2)
            {
                // 3. Locate and remove the matching ant object from the memory tracking list
                for (int i = 0; i < Ants.Count; i++)
                {
                    if (Ants[i].X == gridX && Ants[i].Y == gridY)
                    {
                        Ants.RemoveAt(i);
                        AntsKilled++;
                        break; // Stop iterating once the target is removed
                    }
                }

                // 4. Revert the grid space map value back to clear land (1)
                middleGroundMap[gridX, gridY] = 1; 
            }
        }
    }

    // Helper: Only returns true if the tile exists AND is land (1). 
    // It will return false if the tile is a 0, or if another Ant (2) is already standing there!
    private bool IsWalkable(int x, int y, int width, int height, int[,] map)
    {
        return x >= 0 && x < width && y >= 0 && y < height && map[x, y] == 1;
    }

    
}