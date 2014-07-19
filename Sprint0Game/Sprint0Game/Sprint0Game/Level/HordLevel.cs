using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.Collections;

namespace Sprint0Game
{
    public class HordeLevel : ILevel
    {
        public IMario Mario { get; set; }
        public Background Background { get; private set; }
        public ICamera Camera { get; private set; }
        public ArrayList Enemies { get { return this.Updater.Enemies; } }
        public ArrayList Items { get { return this.Updater.Items; } }
        public ArrayList Projectiles { get { return this.Updater.Projectiles; } }
        public ArrayList Blocks { get { return this.Updater.Blocks; } }
        public ArrayList Pipes { get { return this.Updater.Pipes; } }
        public ArrayList Flags { get { return this.Updater.Flags; } }
        public ArrayList Spawners { get { return this.Updater.Spawners; } }
        public Castle Castle { get; set; }
        public Viewport Window { get; private set; }
        public bool IsFrozen { get; set; }
        public Vector2 TempMarioPos { get; set; }
        public Vector2 TempCameraPos { get; set; }
        public bool IsPaused { get { return this.Updater.IsPaused; } set { this.Updater.IsPaused = value; Game.SetMusic(value); } }
        public WinLevelAnimator WinLevelAnimator { get; set; }
        public bool FlagReached { get; set; }
        public double Time { get; set; }
        public bool HasCastle { get; set; }
        public Dictionary<Vector2, Vector2> UndergroundDict { get; private set; }
        private MarioPipeAnimator MarioPipeAnimator;
        private LevelUpdater Updater;
        private LevelDrawer Drawer;
        private GraphicsDeviceManager Graphics;
        private Game CurrentGame;

        public HordeLevel(Game game, GraphicsDeviceManager graphics)
        {
            this.UndergroundDict=new Dictionary<Vector2,Vector2>();
            this.CurrentGame = game;
            this.Graphics = graphics;
            this.Updater = new LevelUpdater(this, game);
            this.Drawer = new LevelDrawer(this);
            this.Time = LevelHordConfig.LevelTime;
            this.LoadContent();
        }

        public void BeatLevel()
        {

        }

        public void Reset()
        {
            this.CurrentGame.ResetLevel();
        }

        private void LoadContent()
        {
            this.Window = CurrentGame.GraphicsDevice.Viewport;
            this.Updater.LoadContent();
            this.Camera = new NonMovingCamera();
            LoadLevel();
        }

        private void LoadLevel()
        {
            string file = string.Format("Content/{0}.txt", LevelHordConfig.LevelPath);
            LevelCreator creator = new LevelCreator(this);
            this.Background = new Background();
            creator.GenerateLevel(file);
            this.Mario.HasBeenReached = true;
        }       

        public void UnloadContent()
        {
            this.CurrentGame.Content.Unload();
        }

        protected virtual void Dispose(bool disposeAll)
        {
            //set to IDisposable type so Dispose() from IDisposable interface can be called 
            IDisposable disp = this.Graphics;
            disp.Dispose();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool IsStopped()
        {
            return this.IsFrozen || this.IsPaused;
        }

        public void Update()
        {
            if (this.Mario.IsTravelingPipe)
            {
                this.MarioPipeAnimator.Update();
            }
            else
            {
                this.Updater.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.Drawer.Draw(spriteBatch, this.Camera);
        }

        public void ShootFireballFromMario()
        {
            if (this.Mario.IsFire())
            {
                float yPos = this.Mario.CurrentPosition.Y - this.Mario.Height / 3;
                float xPos = this.Mario.CurrentPosition.X;

                Fireball fireball = new Fireball(new Vector2(xPos, yPos), this.Mario.IsRightFacing());
                this.Projectiles.Add(fireball);
            }
        }

        public void SpawnItemFromBlock(Block block)
        {
            this.Updater.SpawnItemFromBlock(block);
        }

        public void HandlePipeEntrance()
        {
            this.Mario.IsTravelingPipe = true;
            this.MarioPipeAnimator = new MarioPipeAnimator(this, this.Camera, true, GetUndergroundPos());
        }

        public void HandlePipeExit()
        {
            this.Mario.IsTravelingPipe = true;
            this.Mario.CurrentPosition = this.TempMarioPos;
            this.Camera.CurrentPosition = this.TempCameraPos;
            this.Background.Texture = SpriteHolder.Background;
            this.MarioPipeAnimator = new MarioPipeAnimator(this, this.Camera, false, GetUndergroundPos());
        }

        private Vector2 GetUndergroundPos()
        {
            Vector2 closestPipe = new Vector2(int.MaxValue, int.MaxValue);
            foreach (Vector2 pos in this.UndergroundDict.Keys)
            {
                if (Math.Abs(pos.X - this.Mario.CurrentPosition.X) < Math.Abs(closestPipe.X - this.Mario.CurrentPosition.X))
                    closestPipe = pos;
            }
            Vector2 Destination;
            this.UndergroundDict.TryGetValue(closestPipe, out Destination);
            return Destination;
        }

        public float YDistanceFromMario(IObject obj)
        {
            return obj.CurrentPosition.Y - this.Mario.CurrentPosition.Y;
        }

        public float XDistanceFromMario(IObject obj)
        {
            return obj.CurrentPosition.X - this.Mario.CurrentPosition.X;
        }
    }
}