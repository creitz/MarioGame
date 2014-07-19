using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;

namespace Sprint0Game
{
    public class WinLevelAnimator
    {
        private Flag Flag;
        private ILevel Level;
        private bool MarioSpriteSwitchedToRunning;
        private bool MarioSpriteSwitchedToStanding;
        private bool FlagRemoved;
        private bool ShouldDrawMario;
        private int MarioStandingTimer;
        private int IdleGameTimer;

        public WinLevelAnimator(ILevel level, Flag flag)
        {
            this.Flag = flag;
            this.Level = level;
            this.ShouldDrawMario = true;
            this.Level.Mario.RespondToRequest(MarioActionRequest.Fall);
        }

        private bool MarioHasReachedBottomOfPole()
        {
            return this.Level.Mario.CurrentPosition.Y > this.Flag.CurrentPosition.Y;
        }
        
        private bool FlagHasReachedBottomOfPole()
        {
            return this.Flag.FlagCurrentPosition.Y+this.Flag.FlagHeight > this.Flag.CurrentPosition.Y+this.Flag.Height;
        }

        private void DeleteFlagAndMakeMarioRunIfNeeded(){
            if(!this.MarioSpriteSwitchedToRunning){
                this.MarioSpriteSwitchedToRunning=true;
                this.Level.Mario.RespondToRequest(MarioActionRequest.GoIdle);
                this.Level.Mario.RespondToRequest(MarioActionRequest.GoRight); //call goright twice in case he 
                this.Level.Mario.RespondToRequest(MarioActionRequest.GoRight); //was facing left when he hit flag
            }

            if(!this.FlagRemoved){
                this.FlagRemoved=true;
                this.Flag.Remove();
            }
        }

        private void MakeMarioStandIfNeeded(){
            if(!this.MarioSpriteSwitchedToStanding){
                this.MarioSpriteSwitchedToStanding=true;
                this.Level.Mario.RespondToRequest(MarioActionRequest.GoIdle);
            }
        }

        public void Update()
        {
            this.Level.IsFrozen = true;
            if (!MarioHasReachedBottomOfPole())
            {
                this.Level.Mario.CurrentPosition = new Vector2(this.Level.Mario.CurrentPosition.X, this.Level.Mario.CurrentPosition.Y + Level1Config.FlagAndMarioDownPoleSpeed);
            }
            if (!FlagHasReachedBottomOfPole())
            {
                this.Flag.FlagCurrentPosition = new Vector2(this.Flag.FlagCurrentPosition.X, this.Flag.FlagCurrentPosition.Y + Level1Config.FlagAndMarioDownPoleSpeed);
            }
            else if (this.Level.Mario.CurrentPosition.X < this.Level.Castle.DoorPosition.X)
            {
                this.DeleteFlagAndMakeMarioRunIfNeeded();
                this.Level.Mario.Update();
            }
            else if (this.MarioStandingTimer < Level1Config.MarioStandingAfterFlagTimer)
            {
                this.MakeMarioStandIfNeeded();
                this.Level.Mario.Update();
                this.MarioStandingTimer++;
            }
            else if (this.IdleGameTimer < Level1Config.TimeAfterMarioHasDisappeared)
            {
                this.ShouldDrawMario = false;
                this.IdleGameTimer++;
            }
            else
            {
                this.Level.BeatLevel();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.Flag.Draw(spriteBatch, this.Level.Camera);
            if(this.ShouldDrawMario)
                this.Level.Mario.CurrentState.Draw(spriteBatch, this.Level.Camera);
        }
    }
}