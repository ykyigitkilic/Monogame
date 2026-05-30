using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Timer
{
    private Texture2D texture;
    private SpriteFont font;
    private float Length;
    public bool active;
    public string text;
    private float timeLeft;
    public bool repeat;
    public event EventHandler OnTimerEnd;

    SpriteBatch spriteBatch;

    public Timer(Texture2D texture,SpriteFont spriteFont,float Length,SpriteBatch spriteBatch)
    {
        this.texture=texture;
        this.font=spriteFont;
        this.Length=Length;
        this.spriteBatch=spriteBatch;
        Reset();
    }

    private void formatText()
    {
        text=TimeSpan.FromSeconds(timeLeft).ToString(@"mm\:ss"); 
    }

    public void StartStop()
    {
        active=!active;
    }
    public void Reset()
    {
        timeLeft=Length;
        formatText();
    }

    public void Update(GameTime gameTime)
    {
        if(!active) return;
        timeLeft-= (float)gameTime.ElapsedGameTime.TotalSeconds;

        if(timeLeft<=0)
        {
            if(repeat)
            {
                Reset();
            }
            else
            {
                StartStop();
                timeLeft=0;
                OnTimerEnd?.Invoke(this,EventArgs.Empty);
            }
        }
        formatText();
    }

    public void Draw()
    {
        spriteBatch.Draw(
            texture: texture,
            position: new Vector2(30,30),
            sourceRectangle: null,
            color: Color.White,
            rotation: 0f,
            origin: new Vector2(0,0),
            scale: 1.5f,
            effects: SpriteEffects.None,
            layerDepth: 1
            );
        spriteBatch.DrawString(font,text,new Vector2(30,30),Color.Black);
    }
}