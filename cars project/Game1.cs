using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;    // for list
using System;

using cars_project.classes;
using cars_project.classes.Components;
using static System.Formats.Asn1.AsnWriter;

namespace cars_project;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public static GameMode gameMode = GameMode.Menu;

    private int screenWidth = 800;
    private int screenHeight = 600;

    private Player player;
    private Map map;
    private Shop shop;

    private List<Obstacle> obstacles;
    private List<Explosion> explosions;

    private Label label;

    private MainMenu mainMenu = new MainMenu();

    private GameOver gameOver = new GameOver();

    private HUD hud = new HUD();


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        // Config
        _graphics.PreferredBackBufferWidth = screenWidth;
        _graphics.PreferredBackBufferHeight = screenHeight;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        map = new Map();
        player=new Player();

        obstacles = new List<Obstacle>();
        explosions = new List<Explosion>();

        label = new Label("Hello, World!!!", Vector2.Zero, Color.White);

        player.TakeDamage += hud.OnPlayerTakeDamage;
        player.ScoreUpdated += hud.OnScoreChanged;
        shop = new Shop();
        mainMenu.OnPlayingStarted += SwitchGameMode;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        player.LoadContent(Content);
        map.LoadContent(Content);

        label.LoadContent(Content);

        mainMenu.LoadContent(Content);

        gameOver.LoadContent(Content);
        shop.LoadContent(Content);

        hud.LoadContent(Content);

    }

    protected override void Update(GameTime gameTime)
    {
        // TODO: Add your update logic here

        switch (gameMode)
        {
            case GameMode.Playing:

                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    gameMode = GameMode.Menu;
                }

                map.Speed = 10;
                player.Update(Content, gameTime);
                map.Update();
                UpdateObstacles();
                UpdateExplosions(gameTime);
                CheckCollision();
                hud.Update();

                if (player.Health <= 0)
                {
                    gameMode = GameMode.GameOver;
                    gameOver.SetScore(player.Score);
                }

                break;

            case GameMode.Menu:
                mainMenu.Update();
                map.Speed = 0.5f;
                map.Update();
                break;
            case GameMode.Shop:
                shop.Update();
                map.Speed = 0.5f;
                map.Update();
                break;

            case GameMode.GameOver:
                gameOver.Update();
                map.Speed = 0.5f;
                map.Update();
                break;

            case GameMode.Exit:
                Exit();
                break;
        }


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        switch (gameMode)
        {
            case GameMode.Playing:
                map.Draw(_spriteBatch);
                player.Draw(_spriteBatch);

                foreach (Obstacle obstacle in obstacles)
                {
                    obstacle.Draw(_spriteBatch);
                }

                foreach (var explosion in explosions)
                {
                    explosion.Draw(_spriteBatch);
                }

                //label.Draw(_spriteBatch);

                hud.Draw(_spriteBatch);



                break;

            case GameMode.Menu:
                map.Draw(_spriteBatch);
                mainMenu.Draw(_spriteBatch);
                break;
            case GameMode.Shop:
                map.Draw(_spriteBatch);
                shop.Draw(_spriteBatch);
                break;

            case GameMode.GameOver:
                map.Draw(_spriteBatch);
                gameOver.Draw(_spriteBatch);
                break;
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void CheckCollision()
    {
        foreach (var obstacle in obstacles)
        {
            if (player.Collision.Intersects(obstacle.Collision))
            {
                obstacle.IsAlive = false;

                player.Damage();

                Explosion explosion = new Explosion(obstacle.Position);
                explosion.LoadContent(Content);
                explosions.Add(explosion);

                explosion.PlaySoundEffect();
            }
        }
    }

    private void UpdateExplosions(GameTime gameTime)
    {
        for (int i = 0; i < explosions.Count; i++)
        {
            explosions[i].Update(gameTime);

            if (!explosions[i].IsAlive)
            {
                explosions.RemoveAt(i);
                i--;
            }
        }
    }


    private void UpdateObstacles()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            Obstacle obstacle = obstacles[i];

            obstacle.Update();

            // teleport
            if (obstacle.Position.Y > screenHeight)
            {
                Random random = new Random();
                int y = random.Next(-screenHeight, 0 - obstacle.Height);
                int x = random.Next(100, screenWidth - obstacle.Width-100);

                obstacle.Position = new Vector2(x, y);
            }


            if (!obstacle.IsAlive)
            {
                obstacles.Remove(obstacle);
                i--;
            }
        }

        // загрузка доп астеройдов в игру
        if (obstacles.Count < 10)
        {
            LoadObstacle();
        }
    }

    private void LoadObstacle()
    {
        Obstacle obstacle = new Obstacle(Vector2.Zero);
        obstacle.LoadContent(Content);

        int rectagleWidth = screenWidth;
        int rectangleHeight = screenHeight;

        Random random = new Random();

        int x = random.Next(100,rectagleWidth - obstacle.Width-100);
        int y = random.Next(0, rectangleHeight - obstacle.Height);

        obstacle.Position = new Vector2(x, -y);

            obstacles.Add(obstacle);
    }

    public void SwitchGameMode()
    {
        gameMode = GameMode.Playing;

        Reset();
    }

    public void Reset()
    {
        player.Reset();

        explosions.Clear();
        obstacles.Clear();

        hud.Reset();
    }
}
