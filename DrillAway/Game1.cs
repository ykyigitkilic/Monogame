using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace DrillAway;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D armoDrillo;
    private Texture2D textureAtlas;
    Test testObject;
    Player player;
    InputManager input;
    MapGenerator mapGenerator;
    public List<Rectangle> StoredTextures;
    public List<Rectangle> ColonyTextures;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.IsBorderless = false;
        Window.AllowUserResizing = true;
        Window.AllowAltF4 = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
        player = new(armoDrillo);
        input = new();
        testObject = new(input,this);
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

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        armoDrillo = Content.Load<Texture2D>("images/ArmoDrillo");
        textureAtlas = Content.Load<Texture2D>("images/dirt_tileset");
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        input.InputCheck();
        testObject.trackRotation();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        //for sprite renders always start the batch
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        // Sectıon 1
        foreach(var item in mapGenerator.tilemapBG)
        {
            Rectangle dest = new(
                (int)item.Key.X*16,
                (int)item.Key.Y*16,
                16,16
            );

            Rectangle src = StoredTextures[item.Value];
            _spriteBatch.Draw(textureAtlas,dest,src,Color.White);

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
            _spriteBatch.Draw(textureAtlas,dest,src,Color.White);

        }

        //after sprites are rendered end the batch
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
