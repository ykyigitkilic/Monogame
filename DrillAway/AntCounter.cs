using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class AntCounter
{
    SpriteBatch spriteBatch;
    Texture2D texture2D;
    SpriteFont spriteFont;
    AntManager antManager;
    public AntCounter(SpriteBatch spriteBatch,Texture2D texture2D,SpriteFont spriteFont,AntManager antManager)
    {
        this.spriteBatch = spriteBatch;
        this.texture2D = texture2D;
        this.spriteFont = spriteFont;
        this.antManager = antManager;
    }


    public void Draw()
    {
        spriteBatch.Draw(
        texture: texture2D,
        position: new Vector2(578,30),
        sourceRectangle: null,
        color: Color.White,
        rotation: 0f,
        origin: new Vector2(0,0),
        scale: 1.5f,
            effects: SpriteEffects.None,
        layerDepth: 1
        );
        spriteBatch.DrawString(spriteFont,Convert.ToString(antManager.AntsKilled),new Vector2(578,30),Color.Black);
    }
}