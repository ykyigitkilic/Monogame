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
    private Texture2D DrillAway;

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
        DrillAway = Content.Load<Texture2D>("images/DrillAway");
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();

        spriteBatch.Draw(DrillAway,new Rectangle(0,0,DrillAway.Bounds.Width,DrillAway.Bounds.Height),Color.White);

        spriteBatch.End();
    }

    public void Update(GameTime gameTime)
    {
        input.InputCheck();
        Console.WriteLine("perne");
    }
    
    private void LoadNextScene(object sender,EventArgs e)
    {
        sceneManager.RemoveScene();
        input.OnButtonA -= LoadNextScene;
        sceneManager.AddScene(new GamePlayeScene(Content,spriteBatch,input,sceneManager));
    }
}