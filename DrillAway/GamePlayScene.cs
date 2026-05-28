using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class GamePlayeScene : IScene
{
    private ContentManager Content;
    private InputManager input;
    private SpriteBatch spriteBatch;
    private SceneManager sceneManager;
    MapGenerator mapGenerator;
    Player player;
    public List<Rectangle> StoredTextures;
    public List<Rectangle> ColonyTextures;
    private Texture2D textureAtlas;
    private Texture2D playerTexture;
    private Matrix CameraShift;
    private Vector2 cameraPosition; 
    private float cameraLagSpeed = 3f;

    public GamePlayeScene(ContentManager Content,SpriteBatch spriteBatch,InputManager input,SceneManager sceneManager)
    {
        this.Content = Content;
        this.spriteBatch = spriteBatch;
        this.input = input;
        this.sceneManager = sceneManager;

        mapGenerator = new();
        mapGenerator.BackGroundGen();
        mapGenerator.MiddelGroundGen();
        mapGenerator.tilemapBG=mapGenerator.loadTileMap("bg.csv");
        mapGenerator.tilemapMG=mapGenerator.loadTileMap("mg.csv");
        StoredTextures = new()
        {
            new Rectangle(9*4,6*4,4,4),
            new Rectangle(0*4,6*4,4,4),
            new Rectangle(9*4,13*4,4,4),
            new Rectangle(0*4,13*4,4,4),
            new Rectangle(9*4,20*4,4,4),
            new Rectangle(0*4,20*4,4,4)
        };
        ColonyTextures = new()
        {
            new Rectangle(0*4,0*4,4,4),
            new Rectangle(9*4,20*4,4,4)
        };

    }

    private void MatrixCalculation(GameTime gameTime)
    {
        float targetX = (640 / 2f) - player.position.X;
        float targetY = (360 / 2f) - player.position.Y;
        Vector2 targetPosition = new Vector2(targetX, targetY);

        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        cameraPosition.X = MathHelper.Lerp(cameraPosition.X, targetPosition.X, cameraLagSpeed * deltaTime);
        cameraPosition.Y = MathHelper.Lerp(cameraPosition.Y, targetPosition.Y, cameraLagSpeed * deltaTime);

        CameraShift = Matrix.CreateTranslation(cameraPosition.X, cameraPosition.Y, 0);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: CameraShift);
        float viewLeft = player.position.X - (640 / 2) - 80;
        float viewRight = player.position.X + (640 / 2) + 80;
        float viewTop = player.position.Y - (360 / 2) - 80;
        float viewBottom = player.position.Y + (360 / 2) + 80;

    // Section 1: Background
    foreach(var item in mapGenerator.tilemapBG)
    {
        float tileX = item.Key.X * 8;
        float tileY = item.Key.Y * 8;

        if (tileX < viewLeft || tileX > viewRight || tileY < viewTop || tileY > viewBottom)
            continue;

        Rectangle dest = new((int)tileX, (int)tileY, 8, 8);
        Rectangle src = StoredTextures[item.Value];
        spriteBatch.Draw(textureAtlas, dest, src, Color.White);
    }

    // Section 2: Middleground
    foreach(var item in mapGenerator.tilemapMG)
    {
        float tileX = item.Key.X * 8;
        float tileY = item.Key.Y * 8;

        if (tileX < viewLeft || tileX > viewRight || tileY < viewTop || tileY > viewBottom)
            continue;

        Rectangle dest = new((int)tileX, (int)tileY, 8, 8);
        Rectangle src = ColonyTextures[item.Value];
        spriteBatch.Draw(textureAtlas, dest, src, Color.White);
    }

    player.Draw(spriteBatch);
    spriteBatch.End();
}

    public void Load()
    {
        textureAtlas = Content.Load<Texture2D>("images/dirt_tileset");
        playerTexture = Content.Load<Texture2D>("images/ArmoDrillo");
        player = new(playerTexture,input);
        cameraPosition = new Vector2(
            (640 / 2f) - player.position.X, 
            (360 / 2f) - player.position.Y
        );
    }

    public void Update(GameTime gameTime)
    {
        input.InputCheck();
        player.Update();
        MatrixCalculation(gameTime);
        Console.WriteLine("dotto");
    }
}