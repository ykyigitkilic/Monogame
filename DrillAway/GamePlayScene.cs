using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class GamePlayeScene : IScene
{
    private ContentManager Content;
    private InputManager input;
    private SpriteBatch spriteBatch;
    private SceneManager sceneManager;
    MapGenerator mapGenerator;
    public List<Rectangle> StoredTextures;
    public List<Rectangle> ColonyTextures;
    private Texture2D textureAtlas;



    public GamePlayeScene(ContentManager Content,SpriteBatch spriteBatch,InputManager input,SceneManager sceneManager)
    {
        this.Content = Content;
        this.spriteBatch = spriteBatch;
        this.input = input;
        this.sceneManager = sceneManager;

        mapGenerator = new();
        mapGenerator.BackGroundGen();
        mapGenerator.MiddelGroundGen();
        mapGenerator.tilemapBG=mapGenerator.loadTileMap("bg.csv");
        mapGenerator.tilemapMG=mapGenerator.loadTileMap("mg.csv");
        StoredTextures = new()
        {
            new Rectangle(9*4,6*4,4,4),
            new Rectangle(0*4,6*4,4,4),
            new Rectangle(9*4,13*4,4,4),
            new Rectangle(0*4,13*4,4,4),
            new Rectangle(9*4,20*4,4,4),
            new Rectangle(0*4,20*4,4,4)
        };
        ColonyTextures = new()
        {
            new Rectangle(0*4,0*4,4,4),
            new Rectangle(9*4,20*4,4,4)
        };
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        // Sectıon 1
        foreach(var item in mapGenerator.tilemapBG)
        {
            Rectangle dest = new(
                (int)item.Key.X*16,
                (int)item.Key.Y*16,
                16,16
            );

            Rectangle src = StoredTextures[item.Value];
            spriteBatch.Draw(textureAtlas,dest,src,Color.White);

        }

        // Sectıon 2
        foreach(var item in mapGenerator.tilemapMG)
        {
            Rectangle dest = new(
                (int)item.Key.X*16,
                (int)item.Key.Y*16,
                16,16
            );

            Rectangle src = ColonyTextures[item.Value];
            spriteBatch.Draw(textureAtlas,dest,src,Color.White);

        }
        
        //after sprites are rendered end the batch
        spriteBatch.End();
    }

    public void Load()
    {
        textureAtlas = Content.Load<Texture2D>("images/dirt_tileset");
    }

    public void Update(GameTime gameTime)
    {
        Console.WriteLine("dotto");
    }
}