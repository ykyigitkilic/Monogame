using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Player
{
    public Player(Texture2D asset)
    {
        texture = asset;
    }
    private Texture2D texture;
    public int posX;
    public int posY;

    public void Draw(SpriteBatch sp)
    {
        sp.Draw(texture,new Rectangle(
            posX,
            posY,
            texture.Bounds.Width/10,
            texture.Bounds.Width/10),
            Color.White);
    }
    
    
}