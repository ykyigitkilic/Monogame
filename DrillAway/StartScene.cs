using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media; // Add this line

public class StartScene : IScene
{
    private ContentManager Content;
    private InputManager input;
    private SpriteBatch spriteBatch;
    private SceneManager sceneManager;
    private Texture2D DrillAway;
    private Game game1;
    private Song HKTheme; // Changed from SoundEffect to Song

    public StartScene(ContentManager Content, SpriteBatch spriteBatch, InputManager input, SceneManager sceneManager, Game game1)
    {
        this.Content = Content;
        this.spriteBatch = spriteBatch;
        this.input = input;
        this.sceneManager = sceneManager;
        this.game1 = game1;
        this.input.OnButtonA += LoadNextScene;
    }

    public void Load()
    {
        DrillAway = Content.Load<Texture2D>("images/DrillAway");
        
        // Load as a Song instead of SoundEffect
        HKTheme = Content.Load<Song>("HKTheme"); 
        
        // Songs must be played through the MediaPlayer
        MediaPlayer.Play(HKTheme); 
        
        // Optional: Loop the music
        MediaPlayer.IsRepeating = true; 
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(DrillAway, new Rectangle(0, 0, DrillAway.Bounds.Width, DrillAway.Bounds.Height), Color.White);
        spriteBatch.End();
    }

    public void Update(GameTime gameTime)
    {
        input.InputCheck();
        if(Keyboard.GetState().IsKeyDown(Keys.Space))
        {
            LoadNextScene(this, null);
        }
        Console.WriteLine("perne");
    }
    
    private void LoadNextScene(object sender, EventArgs e)
    {
        sceneManager.RemoveScene();
        input.OnButtonA -= LoadNextScene;
        sceneManager.AddScene(new GamePlayeScene(Content, spriteBatch, input, sceneManager, game1));
    }
}