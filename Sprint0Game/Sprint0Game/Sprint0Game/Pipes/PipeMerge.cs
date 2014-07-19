using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint0Game
{
    public class PipeMerge : IPipe
    {
        public int Type { get; set; }
        public Vector2 CurrentPosition { get; set; }
        public Vector2 CurrentVelocity { get; set; }
        public int Height { get { return this.Sprite.Height; } }
        public int Width { get { return this.Sprite.Width; } }
        public bool ShouldBeRemoved { get; set; }
        public bool HasBeenReached { get; set; }
        public IAnimatedSprite Sprite { get; set; }
        private PipeCollisionResponder collisionResponder;
        public bool IsTransitional { get; set; }
        public bool Side { get; set; }

        public PipeMerge(Vector2 StartPosition, bool isTransitional, bool Left)
        {
            this.CurrentPosition = StartPosition;
            if (Left)
                this.Type = SpriteHolder.PipeMergeLeftFrame;
            else
                this.Type = SpriteHolder.PipeMergeRightFrame; 
            this.IsTransitional = isTransitional;
            this.Side = true;
            this.Sprite = new PipeSprite(this);
            this.ShouldBeRemoved = false;
            this.collisionResponder = new PipeCollisionResponder(this);
            
        }

        public void Update()
        {
            this.Sprite.Update();
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            this.Sprite.Draw(spriteBatch, camera);
        }

        public void RespondToCollision(Side side, IObject obj, Rectangle intersectRect)
        {
            this.collisionResponder.RespondToCollision(side, obj, intersectRect);
        }

        public void RespondToNoCollision() { }
    }
}