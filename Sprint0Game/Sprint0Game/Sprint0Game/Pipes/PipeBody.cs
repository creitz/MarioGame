using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint0Game
{
    public class PipeBody : IPipe
    {
        public int Type { get; set; }
        public Vector2 CurrentPosition { get; set; }
        public Vector2 CurrentVelocity { get; set; }
        public int Height { get { return this.Sprite.Height; } }
        public int Width { get { return this.Sprite.Width; } }
        public bool ShouldBeRemoved { get; set; }
        public bool HasBeenReached { get; set; }
        public IAnimatedSprite Sprite { get; set; }
        public bool IsTransitional { get; set; }
        public bool Side { get; set; }
        private PipeCollisionResponder CollisionResponder;

        public PipeBody(Vector2 StartPosition, bool isTransitional, bool Side)
        {
            this.CurrentPosition = StartPosition;
            this.Type = SpriteHolder.PipeBodyFrame;
            this.Side = Side;
            this.IsTransitional = isTransitional;
            this.Sprite = new PipeSprite(this);
            this.ShouldBeRemoved = false;
            this.CollisionResponder = new PipeCollisionResponder(this);
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
            this.CollisionResponder.RespondToCollision(side, obj, intersectRect);
        }

        public void RespondToNoCollision() { }
    }
}