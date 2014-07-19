
using System;
using Microsoft.Xna.Framework;

namespace Sprint0Game
{
    public class PipeCollisionResponder
    {
        private IPipe Pipe;

        public PipeCollisionResponder(IPipe pipe)
        {
            this.Pipe = pipe;
        }

        public void RespondToCollision(Side side, IObject obj, Rectangle intersectRect)
        {
            if (obj is IMario)
            {
                RespondToCollisionWithMario(side, (IMario)obj, intersectRect);
            }
        }

        private void RespondToCollisionWithMario(Side side, IMario mario, Rectangle intersectRect)
        {
            mario.OnTransPipe = false;
            mario.TouchingTransPipe = false;
            if (this.Pipe is PipeTop) {
                PipeTop pipeTop = (PipeTop)this.Pipe;
                if (pipeTop.IsTransitional)
                {
                    if (IsAbleToEnterPipe(side, pipeTop, intersectRect))
                    {
                        mario.OnTransPipe = true;
                    }
                    else if ((SideGeneralizer.IsLeft(side) || SideGeneralizer.IsRight(side)) && pipeTop.Side)
                    {
                        mario.TouchingTransPipe = true;
                    }
                }
            }
        }

        private static bool IsAbleToEnterPipe(Side side, PipeTop pipeTop, Rectangle intersectRect)
        {
            return SideGeneralizer.IsTop(side) && !pipeTop.Side && 
                (Math.Abs(intersectRect.Center.X - (pipeTop.CurrentPosition.X + pipeTop.Width/2)) < Level1Config.MarioOnPipeCenterTolerance);
        }
    }
}
