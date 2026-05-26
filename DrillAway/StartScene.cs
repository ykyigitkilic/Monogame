using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class StartScene : IScene
{
    private ContentManager Content;
    private InputManager input;
    private SpriteBatch spriteBatch;
    private SceneManager sceneManager;
    private Texture2D armoDrillo;
    
    private int posx;
    private int posy;


    public StartScene(ContentManager Content,SpriteBatch spriteBatch,InputManager input,SceneManager sceneManager)
    {
        this.Content = Content;
        this.spriteBatch = spriteBatch;
        this.input = input;
        this.sceneManager = sceneManager;
        input.OnButtonA += LoadNextScene;
    }
    public void Load()
    {
        armoDrillo = Content.Load<Texture2D>("images/ArmoDrillo");
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();

        spriteBatch.Draw(armoDrillo,new Rectangle(
            posx,
            posy,
            armoDrillo.Bounds.Width/10,
            armoDrillo.Bounds.Width/10),
            Color.White);

        spriteBatch.End();
    }

    public void Update(GameTime gameTime)
    {
        input.InputCheck();
        Console.WriteLine("porno");
    }
    
    private void LoadNextScene(object sender,EventArgs e)
    {
        sceneManager.AddScene(new GamePlayeScene(Content,spriteBatch,input,sceneManager));
    }
}