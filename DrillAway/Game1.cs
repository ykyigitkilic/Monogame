using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DrillAway;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SceneManager sceneManager;
    private SpriteBatch spriteBatch;
    InputManager input;
    private RenderTarget2D renderTarget;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
        Window.IsBorderless = true;
        Window.AllowUserResizing = true;
        Window.AllowAltF4 = true;
        
        _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        _graphics.IsFullScreen = false;
        _graphics.HardwareModeSwitch = false;
        _graphics.ApplyChanges();
        
        sceneManager = new();
        input = new();
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        
        renderTarget = new RenderTarget2D(GraphicsDevice, 640, 360);
        
        sceneManager.AddScene(new StartScene(Content,spriteBatch,input,sceneManager,this));
        
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        sceneManager.GetCurrentScene().Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(renderTarget);
        sceneManager.GetCurrentScene().Draw(spriteBatch);

        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.Black);

        spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        spriteBatch.Draw(renderTarget, GraphicsDevice.Viewport.Bounds, Color.White);
        spriteBatch.End();

        base.Draw(gameTime);
    }
}