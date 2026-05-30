using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class WinScene : IScene
{
    private ContentManager Content;
    private InputManager input;
    private SpriteBatch spriteBatch;
    private SceneManager sceneManager;
    private Texture2D Win;
    private Game game1;

    public WinScene(ContentManager Content,SpriteBatch spriteBatch,InputManager input,SceneManager sceneManager,Game game1)
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
        Win = Content.Load<Texture2D>("images/win");
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();

        spriteBatch.Draw(Win,new Rectangle(0,0,Win.Bounds.Width,Win.Bounds.Height),Color.White);

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