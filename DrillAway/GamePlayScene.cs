using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
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
    Timer timer;
    public List<Rectangle> StoredTextures;
    public List<Rectangle> ColonyTextures;
    private Texture2D textureAtlas;
    private Texture2D colonyAtlas;
    private Texture2D playerTexture;
    private Matrix CameraShift;
    private Vector2 cameraPosition; 
    private float cameraLagSpeed = 10f;
    public AntManager antManager;
    private Game game1;
    AntCounter antCounter;
    Song NMKG;
    public GamePlayeScene(ContentManager Content,SpriteBatch spriteBatch,InputManager input,SceneManager sceneManager,Game game1)
    {
        this.Content = Content;
        this.spriteBatch = spriteBatch;
        this.input = input;
        this.sceneManager = sceneManager;
        this.game1=game1;

        antManager = new();
        mapGenerator = new();
        mapGenerator.BackGroundGen();
        mapGenerator.MiddelGroundGen();
        mapGenerator.tilemapBG=mapGenerator.loadTileMap("bg.csv");
        mapGenerator.tilemapMG=mapGenerator.loadTileMap("mg.csv");
        antManager.antCreate(
        mapGenerator.tilemapMG,
        mapGenerator.tileWidth,
        mapGenerator.tileHeight,
        10000);

        StoredTextures = new()
        {
            new Rectangle(0*16,0*16,16,16),
            new Rectangle(1*16,0*16,16,16)
        };

        ColonyTextures = new()
        {
            new Rectangle(0*16,0*16,16,16),  // Index 0 (Empty)
            new Rectangle(1*16,0*16,16,16), // Index 1 (Land)
            new Rectangle(2*16,0*16,16,16)  // Index 2 (ANT)
        };

        
    }

    private void MatrixCalculation(GameTime gameTime)
{
    float screenWidth = 640f;
    float screenHeight = 360f;
    float tileSize = 16f;

    float mapWidthInPixels = mapGenerator.tileWidth * tileSize;
    float mapHeightInPixels = mapGenerator.tileHeight * tileSize;

    float targetX = (screenWidth / 2f) - player.position.X;
    float targetY = (screenHeight / 2f) - player.position.Y;

    targetX = MathHelper.Clamp(targetX, -(mapWidthInPixels - screenWidth), 0);
    targetY = MathHelper.Clamp(targetY, -(mapHeightInPixels - screenHeight), 0);

    float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

    cameraPosition.X = MathHelper.Lerp(cameraPosition.X, targetX, cameraLagSpeed * deltaTime);
    cameraPosition.Y = MathHelper.Lerp(cameraPosition.Y, targetY, cameraLagSpeed * deltaTime);

    CameraShift = Matrix.CreateTranslation(cameraPosition.X, cameraPosition.Y, 0);
}

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: CameraShift);
        
        float viewLeft = player.position.X - (640 / 2) - 120;
        float viewRight = player.position.X + (640 / 2) + 120;
        float viewTop = player.position.Y - (360 / 2) - 120;
        float viewBottom = player.position.Y + (360 / 2) + 120;

        int minTileX = (int)Math.Floor(viewLeft / 16);
        int maxTileX = (int)Math.Ceiling(viewRight / 16);
        int minTileY = (int)Math.Floor(viewTop / 16);
        int maxTileY = (int)Math.Ceiling(viewBottom / 16);

        //Background
        for (int y = minTileY; y <= maxTileY; y++)
        {
            for (int x = minTileX; x <= maxTileX; x++)
            {
                if (x >= 0 && x < mapGenerator.tileWidth && y >= 0 && y < mapGenerator.tileHeight)
                {
                    int tileValue = mapGenerator.tilemapBG[x, y];
                    
                    Rectangle dest = new Rectangle(x * 16, y * 16, 16, 16);
                    Rectangle src = StoredTextures[tileValue];
                    spriteBatch.Draw(textureAtlas, dest, src, Color.White);
                }
            }
        }

        //Middleground
        
        for (int y = minTileY; y <= maxTileY; y++)
        {
            for (int x = minTileX; x <= maxTileX; x++)
            {
                if (x >= 0 && x < mapGenerator.tileWidth && y >= 0 && y < mapGenerator.tileHeight)
                {
                    int tileValue = mapGenerator.tilemapMG[x, y];
                    
                    Rectangle dest = new Rectangle(x * 16, y * 16, 16, 16);
                    Rectangle src = ColonyTextures[tileValue];
                    if(tileValue>1) spriteBatch.Draw(colonyAtlas, dest, src, Color.White,0,new Vector2(),effects:SpriteEffects.None,0.99f);
                    spriteBatch.Draw(colonyAtlas, dest, src, Color.White);
                }
            }
        }
        

        player.Draw(spriteBatch);
        
        spriteBatch.End();


        //HUD
        spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        
        timer.Draw();
        antCounter.Draw();
        
        spriteBatch.End();
    }

    public void Update(GameTime gameTime)
{
    input.InputCheck();
    player.Update();

    float tileSize = 16f;
    float playerWidth = 16f;
    float playerHeight = 16f;

    player.position.X = MathHelper.Clamp(player.position.X, 0, (mapGenerator.tileWidth * tileSize) - playerWidth);
    player.position.Y = MathHelper.Clamp(player.position.Y, 0, (mapGenerator.tileHeight * tileSize) - playerHeight);

    timer.Update(gameTime);

    int playerGridX = (int)Math.Floor(player.position.X / 16);
    int playerGridY = (int)Math.Floor(player.position.Y / 16);

    antManager.KillAntAt(
        playerGridX, 
        playerGridY, 
        mapGenerator.tilemapMG, 
        mapGenerator.tileWidth, 
        mapGenerator.tileHeight
    );

    antManager.antMove(
        gameTime,
        mapGenerator.tilemapMG,
        mapGenerator.tileWidth,
        mapGenerator.tileHeight
    );

    MatrixCalculation(gameTime);
}
    public void Load()
    {
        textureAtlas = Content.Load<Texture2D>("images/mainAtlas");
        colonyAtlas = Content.Load<Texture2D>("images/colonyAtlas");
        playerTexture = Content.Load<Texture2D>("images/ArmoDrillo");
        Texture2D timerTexture = Content.Load<Texture2D>("images/TimerBackGround");
        SpriteFont font = Content.Load<SpriteFont>("font");
        player = new(playerTexture,input,game1);
        timer = new(timerTexture,font,60,spriteBatch);
        cameraPosition = new Vector2(
            (640 / 2f) - player.position.X, 
            (360 / 2f) - player.position.Y
        );
        timer.StartStop();
        timer.repeat=false;
        timer.OnTimerEnd+=LoadNextScene;
        antCounter = new(spriteBatch,timerTexture,font,antManager);
        NMKG = Content.Load<Song>("NMKG"); 
        
        MediaPlayer.Play(NMKG); 

        MediaPlayer.IsRepeating = true; 
    }

    private void LoadNextScene(object sender,EventArgs e)
    {
        if(antManager.AntsKilled>=10)
        {
            sceneManager.RemoveScene();
            timer.OnTimerEnd-=LoadNextScene;
            sceneManager.AddScene(new WinScene(Content,spriteBatch,input,sceneManager,game1));
        }
        else
        {
            sceneManager.RemoveScene();
            timer.OnTimerEnd-=LoadNextScene;
            sceneManager.AddScene(new LoseScene(Content,spriteBatch,input,sceneManager,game1));
        }
    }
}