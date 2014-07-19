
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace Sprint0Game
{
    public class AnimatedBlock : IAnimatedSprite
    {
        public bool Changed { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        private Texture2D Texture;
        private Block Block;
        private int BumpPositionCounter;
        private int BumpVelocity = BlockConfig.BumpUpSpeed;
        private ArrayList TextureCoordinate;
        private int CurrentFrame = 0;
        private int FrameStepCounter = 0;
        private Color Color = Color.White;

        public AnimatedBlock(Texture2D texture, ArrayList coordinates, Block block)
        {
            this.Texture = texture;
            this.Block = block;
            this.TextureCoordinate = coordinates;
            this.Changed = false;
            this.Width = SpriteHolder.BlockWidth;
            this.Height = SpriteHolder.BlockHeight;
        }

        public void Update()
        {
            int totalFrames;
            if (this.Block.CurrentState is QuestionBlockState && !Changed)
                totalFrames = SpriteHolder.QuestionBlockFrames;
            else
                totalFrames = 1;

            this.FrameStepCounter++;
            if (this.FrameStepCounter == BlockConfig.FrameStepPeriod)
            {
                this.CurrentFrame++;
                if (this.CurrentFrame == totalFrames)
                {
                    this.CurrentFrame = 0;
                }
                this.FrameStepCounter = 0;
            }

            //Keep hidden blocks transparent
            if (this.Block.CurrentState is HiddenBlockState)
                this.Color = Color.Transparent;
            else
                this.Color = Color.White;

            //Control the movement of a bumped block
            if (this.Block.Bumped)
            {
                if (this.BumpPositionCounter == BlockConfig.BumpHeight)
                {
                    this.BumpVelocity = BlockConfig.BumpDownSpeed;
                }
                this.BumpPositionCounter += BumpVelocity;
                this.Block.CurrentPosition = new Vector2(this.Block.CurrentPosition.X, 
                    this.Block.CurrentPosition.Y - this.BumpVelocity);
                if (this.BumpPositionCounter == 0)
                {
                    this.Block.Bumped = false;
                    this.BumpVelocity = BlockConfig.BumpUpSpeed;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle sourceRectangle = new Rectangle(this.Width * this.CurrentFrame, 
                this.Height * (int)TextureCoordinate[1], this.Width, this.Height);
            Rectangle drawnRectangle = new Rectangle((int)(this.Block.CurrentPosition.X - camera.CurrentPosition.X), 
                (int)this.Block.CurrentPosition.Y - this.Height, 
                this.Width, this.Height);
             
            spriteBatch.Draw(this.Texture, drawnRectangle, sourceRectangle, this.Color);
        }

        public void Destroy()
        {
            this.Block.CurrentState = new DestroyedBlockState(Block);
        }
    }
}
