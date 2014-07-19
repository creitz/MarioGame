using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class DestroyedBlockState : IBlockState
    {
        private Block Block;
        private int Timer { get; set; }

        private Texture2D Sprite1;
        private Texture2D Sprite2;
        private Texture2D Sprite3;
        private Texture2D Sprite4;

        private Vector2 Velocity1;
        private Vector2 Velocity2;
        private Vector2 Velocity3;
        private Vector2 Velocity4;

        private Vector2 CurrentPosition1;
        private Vector2 CurrentPosition2;
        private Vector2 CurrentPosition3;
        private Vector2 CurrentPosition4;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public DestroyedBlockState(Block block)
        {
            this.Width = 0;
            this.Height = 0;
            this.Block = block;
            this.Timer = BlockConfig.DestroyedBlockTimer;

            //Initialize 4 block pieces with their own sprites
            this.Sprite1 = SpriteHolder.DestroyedBlock;
            this.Sprite2 = SpriteHolder.DestroyedBlock;
            this.Sprite3 = SpriteHolder.DestroyedBlock;
            this.Sprite4 = SpriteHolder.DestroyedBlock;

            //Set width and heigh of sprites
            this.Width = SpriteHolder.BrokenBlockWidth;
            this.Height = SpriteHolder.BrokenBlockHeight;

            //Initialize the velocities of the four sprites in order of topleft, topright, bottomleft, bottomright
            this.Velocity1.X = -BlockConfig.BrokenBlockVelocity.X; 
            this.Velocity1.Y = -BlockConfig.BrokenBlockVelocity.Y;
            this.Velocity2.X = BlockConfig.BrokenBlockVelocity.X;
            this.Velocity2.Y = -BlockConfig.BrokenBlockVelocity.Y;
            this.Velocity3.X = -BlockConfig.BrokenBlockVelocity.X;
            this.Velocity3.Y = BlockConfig.BrokenBlockVelocity.Y;
            this.Velocity4.X = BlockConfig.BrokenBlockVelocity.X;
            this.Velocity4.Y = BlockConfig.BrokenBlockVelocity.Y;

            //Initialize positions in a similar fashion to velocities
            this.CurrentPosition1.X = block.CurrentPosition.X;
            this.CurrentPosition1.Y = block.CurrentPosition.Y - Height;
            this.CurrentPosition2.X = block.CurrentPosition.X + Width;
            this.CurrentPosition2.Y = block.CurrentPosition.Y - Height;
            this.CurrentPosition3.X = block.CurrentPosition.X;
            this.CurrentPosition3.Y = block.CurrentPosition.Y;
            this.CurrentPosition4.X = block.CurrentPosition.X + Width;
            this.CurrentPosition4.Y = block.CurrentPosition.Y;
        }

        public void Update()
        {
            if (this.Timer > 0)
            {
                Timer--;
                
                //Update Positions of the pieces based on their velocity
                this.CurrentPosition1 += this.Velocity1;
                this.CurrentPosition2 += this.Velocity2;
                this.CurrentPosition3 += this.Velocity3;
                this.CurrentPosition4 += this.Velocity4;

                //Update velocities based on gravity
                this.Velocity1.Y += PhysicsConfig.Gravity;
                this.Velocity2.Y += PhysicsConfig.Gravity;
                this.Velocity3.Y += PhysicsConfig.Gravity;
                this.Velocity4.Y += PhysicsConfig.Gravity;

            }

            else
            {
                //Block has timed out and needs to be removed
                this.Width = 0;
                this.Height = 0;
                this.Block.ShouldBeRemoved = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            //Make a source and four drawn rectangles
            Rectangle sourceRectangle = new Rectangle(0, 0, Width, Height);
            Rectangle drawnRectangle1 = new Rectangle((int)(this.CurrentPosition1.X - camera.CurrentPosition.X),
                (int)this.CurrentPosition1.Y - this.Height, Width, Height);
            Rectangle drawnRectangle2 = new Rectangle((int)(this.CurrentPosition2.X - camera.CurrentPosition.X),
                (int)this.CurrentPosition2.Y - this.Height, Width, Height);
            Rectangle drawnRectangle3 = new Rectangle((int)(this.CurrentPosition3.X - camera.CurrentPosition.X),
                (int)this.CurrentPosition3.Y - this.Height, Width, Height);
            Rectangle drawnRectangle4 = new Rectangle((int)(this.CurrentPosition4.X - camera.CurrentPosition.X),
                (int)this.CurrentPosition4.Y - this.Height, Width, Height);

            //Draw the 4 sprites
            spriteBatch.Draw(this.Sprite1, drawnRectangle1, sourceRectangle, Color.White);
            spriteBatch.Draw(this.Sprite2, drawnRectangle2, sourceRectangle, Color.White);
            spriteBatch.Draw(this.Sprite3, drawnRectangle3, sourceRectangle, Color.White);
            spriteBatch.Draw(this.Sprite4, drawnRectangle4, sourceRectangle, Color.White);
 
        }

        public void Break()
        {
            //This type of block is already broken
        }

        public void Bump()
        {
            //This type of block cannot be bumped
        }
    }
}
