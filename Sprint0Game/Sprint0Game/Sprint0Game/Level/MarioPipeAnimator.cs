using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;

namespace Sprint0Game
{
    public class MarioPipeAnimator
    {
        private ILevel Level;
        private ICamera Camera;
        private int PipeTimer = 50;
        private bool EnteringPipe;
        private Vector2 UndergroundPos;

        public MarioPipeAnimator(ILevel level, ICamera camera, bool entering, Vector2 marioUndergroundPos)
        {
            this.EnteringPipe = entering;
            this.Camera = camera;
            this.Level = level;
            this.Level.Mario.TouchingTransPipe = false;
            this.Level.Mario.OnTransPipe = false;
            this.UndergroundPos = marioUndergroundPos;
            SoundBoard.PowerDown.Play();
        }

        public void Update()
        {
            this.PipeTimer--;
            if (this.PipeTimer > 0)
            {
                this.Level.Mario.DisableAllInput();
                if (this.EnteringPipe)
                    this.Level.Mario.CurrentPosition = new Vector2(this.Level.Mario.CurrentPosition.X, this.Level.Mario.CurrentPosition.Y + 1);
                else
                    this.Level.Mario.CurrentPosition = new Vector2(this.Level.Mario.CurrentPosition.X, this.Level.Mario.CurrentPosition.Y - 1);
            }
            else
            {
                this.Level.Mario.EnableAllInput();
                this.Level.Mario.IsTravelingPipe = false;
                this.Level.Mario.IsUnderground = false;
                this.Level.TempMarioPos = this.Level.Mario.CurrentPosition;
                this.Level.TempCameraPos = this.Camera.CurrentPosition;
                if (this.EnteringPipe)
                {
                    this.Level.Mario.IsUnderground = true;
                    this.Level.Mario.CurrentPosition = this.UndergroundPos;
                    this.Camera.CurrentPosition = new Vector2(this.Level.Mario.CurrentPosition.X - GameConfig.MarioCameraOffset, 
                        this.Camera.CurrentPosition.Y);
                    this.Level.Background.Texture = SpriteHolder.BlackBackground;
                }
            }
        }
    }
}