using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class LoseScene : IScene
{
    private ContentManager Content;
    private InputManager input;
    private SpriteBatch spriteBatch;
    private SceneManager sceneManager;
    private Texture2D Lose;
    private Game game1;

    public LoseScene(ContentManager Content,SpriteBatch spriteBatch,InputManager input,SceneManager sceneManager,Game game1)
    {
        this.Content = Content;
        this.spriteBatch = spriteBatch;
        this.input = input;
        this.sceneManager = sceneManager;
        this.game1=game1;
        this.input.OnButtonA += LoadNextScene;
    }
    public void Load()
    {
        Lose = Content.Load<Texture2D>("images/lose");
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();

        spriteBatch.Draw(Lose,new Rectangle(0,0,Lose.Bounds.Width,Lose.Bounds.Height),Color.White);

        spriteBatch.End();
    }

    public void Update(GameTime gameTime)
    {
        input.InputCheck();
        if(Keyboard.GetState().IsKeyDown(Keys.Space))
        {
            LoadNextScene(this,null);
        }
        Console.WriteLine("perne");
    }
    
    private void LoadNextScene(object sender,EventArgs e)
    {
        sceneManager.RemoveScene();
        input.OnButtonA -= LoadNextScene;
        sceneManager.AddScene(new StartScene(Content,spriteBatch,input,sceneManager,game1));
    }
}