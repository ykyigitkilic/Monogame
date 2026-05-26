using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace DrillAway;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SceneManager sceneManager;
    private SpriteBatch spriteBatch;
    private Texture2D armoDrillo;
    Test testObject;
    Player player;
    InputManager input;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.IsBorderless = false;
        Window.AllowUserResizing = true;
        Window.AllowAltF4 = true;
        sceneManager = new();
        input = new();
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
        player = new(armoDrillo);
        testObject = new(input,this);
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        sceneManager.AddScene(new StartScene(Content,spriteBatch,input,sceneManager));
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        input.InputCheck();
        testObject.trackRotation();
        base.Update(gameTime);
        sceneManager.GetCurrentScene().Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        //GraphicsDevice.Clear(Color.CornflowerBlue);
        sceneManager.GetCurrentScene().Draw(spriteBatch);
        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
