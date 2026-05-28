using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Player
{
    public Player(Texture2D asset,InputManager input)
    {
        texture = asset;
        this.input=input;
        position = new Vector2(320,180);
        speed = 0f;
    }
    public Texture2D texture;
    public Vector2 position;
    private float rotation;
    private InputManager input;
    private float speed;
    private float velocity;
    Quarter lastQuarter;
    enum Quarter
    {
        None,
        Quarter1,
        Quarter2,
        Quarter3,
        Quarter4
    }

    public void Draw(SpriteBatch sb)
    {
        Vector2 origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
        sb.Draw(
            texture: texture,
            position: position,
            sourceRectangle: null,
            color: Color.White,
            rotation: rotation,
            origin: origin,
            scale: 0.01f,
            effects: SpriteEffects.None,
            layerDepth: 0f
            );
    }

    public void Update()
    {
        input.InputCheck();
        trackRotation();
        Move();
    }
    
    private void Move()
    {
        if(input.LeftStick.LengthSquared()>0.5f)
        {
            rotation = MathF.Atan2(-input.LeftStick.Y,input.LeftStick.X);
            
            float dirX = MathF.Cos(rotation);
            float dirY = MathF.Sin(rotation); 
            Vector2 forwardDirection = new Vector2(dirX, dirY);
            position += forwardDirection * speed;
        }
        if(speed>0) speed-=1f;
    }
    
    public void trackRotation()
    {
        //player içine kopyalanacak komple
        //enum ve quarter variable olmadan çalışmaz
        //CurrentQuarter ve rotatoinCheck olmadan çalışmaz
        var current = CurrentQuarter(input.RightStick);
        if(lastQuarter!=current)
        {
            Console.WriteLine(current);
            if(rotationCheck(current))
            {
                //dogru yone donuyor burda iste character speed+1 falan yazılacak
                Console.WriteLine("babag dogru yon devam");
                speed+=4f;
            }
            else
            {
                //yanlıs yone donuyor character speed-1 falan yazılacak
                Console.WriteLine("noo");
            }
        }
        lastQuarter=current;
    }
    Quarter CurrentQuarter(Vector2 vector)
    {
        // koodinat sisteminde hangi kısımda olduğunu buluyor
        if (vector.X >= 0 && vector.Y > 0) return Quarter.Quarter1;
        if (vector.X > 0 && vector.Y <= 0) return Quarter.Quarter4;
        if (vector.X <= 0 && vector.Y < 0) return Quarter.Quarter3;
        if (vector.X < 0 && vector.Y >= 0) return Quarter.Quarter2;
        return Quarter.None;
    }
    bool rotationCheck(Quarter current)
    {
        //koordinat sisteminde dogru yone mi hareket ediyor diye
        //table of koodinat sistemi yazdım
        return
        (lastQuarter == Quarter.Quarter1 && current == Quarter.Quarter4) ||
        (lastQuarter == Quarter.Quarter4 && current == Quarter.Quarter3) ||
        (lastQuarter == Quarter.Quarter3 && current == Quarter.Quarter2) ||
        (lastQuarter == Quarter.Quarter2 && current == Quarter.Quarter1);
    }
}