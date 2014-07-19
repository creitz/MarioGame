using Microsoft.Xna.Framework;
using Microsoft.Xna;
using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Sprint0Game
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        public ILevel CurrentLevel { get; private set; }
        public Keybinder Keybinds { get; private set; }
        public HUD HUD { get; private set; }
        public bool MenuDisplaying { get; set; }
        private GraphicsDeviceManager Graphics;
        private LivesScreen LivesScreen;
        public Menu Menu { get; private set; }
        private SpriteBatch SpriteBatch;
        private int LivesScreenTimer = GameConfig.LivesScreenTimer;
        private String LevelString;
        public ArrayList Levels { get; private set; }
        private int LevelBeingPlayed = 0;
        public bool MusicShouldPlay { get; set; }

        public Game()
        {
            this.Levels = new ArrayList { "Level1-1", "Level1-2" };
            Content.RootDirectory = "Content";
            this.Graphics = new GraphicsDeviceManager(this);
            GameStats.Lives = 1;
            MenuDisplaying = true;
            this.LevelString = (String)Levels[0];
            this.MusicShouldPlay = true;
            Keybinds = new Keybinder();
        }

        public void AdvanceLevel()
        {
            LevelBeingPlayed++;
            if (LevelBeingPlayed + 1 > Levels.Count)
            {
                LoadHordeLevel();
                LevelBeingPlayed = 0;
            }
            else
                LoadLevel();
        }

        public void PlayHordeMode()
        {
            this.MenuDisplaying = false;
            LoadHordeLevel();
        }

        private void LoadLevel()
        {
            this.LivesScreenTimer = GameConfig.LivesScreenTimer;
            if (LevelBeingPlayed > 0)
                this.CurrentLevel = new Level(this, this.Graphics, (String)Levels[LevelBeingPlayed], this.CurrentLevel.Mario.PowerLevel());
            else 
                this.CurrentLevel = new Level(this, this.Graphics, (String)Levels[LevelBeingPlayed], MarioPowerLevel.Small);
            this.HUD = new HUD(this, ((string)Levels[LevelBeingPlayed]).Substring(5));
            
        }

        public void LoadHordeLevel()
        {
            this.LivesScreenTimer = GameConfig.LivesScreenTimer;
            this.CurrentLevel = new HordeLevel(this, this.Graphics);
            this.HUD = new HUD(this, "Horde");
        }

        public void PlayGame()
        {
            this.MenuDisplaying = false;
            LoadLevel();
        }

        public void ResetLevel()
        {
             if (GameStats.Lives < 0)
            {
                this.MenuDisplaying = true;
                GameStats.Points = 0;
                GameStats.Lives = 1;
                this.LevelBeingPlayed = 0;
                this.ResetCurrentLevel();
            } 
            else
            {
                ResetCurrentLevel();   
            }
             MarioConfig.SetNormalMarioSpeed();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        private void ResetCurrentLevel()
        {
            MediaPlayer.Pause();
            GameStats.Coins = 0;
            if (this.CurrentLevel is HordeLevel)
                this.LoadHordeLevel();
            else
                this.LoadLevel();
        }

        protected override void LoadContent()
        {
            SpriteHolder.LoadSprites(this);
            SoundBoard.LoadSounds(this);
            MediaPlayer.IsRepeating = true;
            this.SpriteBatch = new SpriteBatch(this.GraphicsDevice);
            this.LivesScreen = new LivesScreen(this.GraphicsDevice.Viewport.Width, this.GraphicsDevice.Viewport.Height);
            this.Menu = new Menu(this, this.GraphicsDevice.Viewport.Width, this.GraphicsDevice.Viewport.Height);
            this.HUD = new HUD(this, this.LevelString.Substring(5));
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        public static void SetMusic(bool on)
        {
            if (on)
                MediaPlayer.Resume();
            else
                MediaPlayer.Pause();
        }

        protected override void Update(GameTime gameTime)
        {
            if (MenuDisplaying)
            {
                this.Menu.Update();
                MediaPlayer.Pause();
            }
            else if (LivesScreenTimer > 0)
            {
                if (LivesScreenTimer == GameConfig.LivesScreenTimer && this.MusicShouldPlay)
                    MediaPlayer.Play(SoundBoard.BackgroundMusic);

                LivesScreenTimer--;
            }
            else
            {
                this.CurrentLevel.Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.SpriteBatch.Begin();

            if (MenuDisplaying)
                this.Menu.Draw(this.SpriteBatch);
            else if (LivesScreenTimer > 0)
                this.LivesScreen.Draw(this.SpriteBatch);
            else
            {
                this.CurrentLevel.Draw(this.SpriteBatch);
                this.HUD.Draw(this.SpriteBatch);
            }

            this.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
