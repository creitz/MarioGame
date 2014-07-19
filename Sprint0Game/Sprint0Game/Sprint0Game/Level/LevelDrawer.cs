using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;

namespace Sprint0Game
{
    public class LevelDrawer
    {
        private ILevel Level;
        private Texture2D Pause;

        public LevelDrawer(ILevel level)
        {
            this.Level = level;
            this.Pause = SpriteHolder.Pause;
        }
        
        public bool IsStopped()
        {
            return this.Level.IsFrozen || this.Level.IsPaused;
        }

        private static void DrawIfNecessary(IObject obj, SpriteBatch spriteBatch, ICamera camera)
        {
            if (obj.HasBeenReached)
            {
                obj.Draw(spriteBatch, camera);
            }
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            this.Level.Background.Draw(this.Level.Window.Bounds, spriteBatch);
            DrawItems(spriteBatch, camera);
            DrawProjectiles(spriteBatch, camera);
            DrawFlags(spriteBatch, camera);
            if (this.Level.HasCastle)
                this.Level.Castle.Draw(spriteBatch, camera);
            
            if (this.Level.Mario.IsDying())  //if mario is dying, the blocks need to be drawn behind
            {
                DrawBlocks(spriteBatch, camera);
                if (!this.Level.FlagReached)
                    this.Level.Mario.Draw(spriteBatch, camera);
                else
                    this.Level.WinLevelAnimator.Draw(spriteBatch);
            }
            else  //otherwise the blocks should be drawn in front (specifically for pipe traveling etc.)
            {
                if (!this.Level.FlagReached)
                    this.Level.Mario.Draw(spriteBatch, camera);
                else
                    this.Level.WinLevelAnimator.Draw(spriteBatch);
                DrawBlocks(spriteBatch, camera);
            }

            DrawEnemies(spriteBatch, camera);
            DrawPipes(spriteBatch, camera);
            if (this.Level.IsPaused)
                this.DrawPaused(spriteBatch);
        }

        private void DrawItems(SpriteBatch spriteBatch, ICamera camera)
        {
            foreach (IItem item in this.Level.Items)
            {
                DrawIfNecessary(item, spriteBatch, camera);
            }
        }

        private void DrawEnemies(SpriteBatch spriteBatch, ICamera camera)
        {
            foreach (IEnemy enemy in this.Level.Enemies)
            {
                DrawIfNecessary(enemy, spriteBatch, camera);
            }
        }

        private void DrawProjectiles(SpriteBatch spriteBatch, ICamera camera)
        {
            foreach (IProjectile projectile in this.Level.Projectiles)
            {
                DrawIfNecessary(projectile, spriteBatch, camera);
            }
        }

        private void DrawBlocks(SpriteBatch spriteBatch, ICamera camera)
        {
            foreach (Block block in this.Level.Blocks)
            {
                DrawIfNecessary(block, spriteBatch, camera);
            }
        }

        private void DrawPipes(SpriteBatch spriteBatch, ICamera camera)
        {
            foreach (IPipe pipe in this.Level.Pipes)
            {
                DrawIfNecessary(pipe, spriteBatch, camera);
            }
        }

        private void DrawFlags(SpriteBatch spriteBatch, ICamera camera)
        {
            foreach (Flag flag in this.Level.Flags)
            {
                DrawIfNecessary(flag, spriteBatch, camera);
            }
        }

        private void DrawPaused(SpriteBatch spriteBatch)
        {
            int width = this.Pause.Width;
            int height = this.Pause.Height;
            Rectangle sourceRectangle = new Rectangle(0, 0, width, height);
            Rectangle drawnRectangle = new Rectangle((int)HUDConfig.PausedLoc.X, (int)HUDConfig.PausedLoc.Y,
                width, height);

            spriteBatch.Draw(this.Pause, drawnRectangle, sourceRectangle, Color.White);
        }
    }
}