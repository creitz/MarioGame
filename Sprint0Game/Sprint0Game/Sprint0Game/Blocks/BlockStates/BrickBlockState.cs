using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Collections;

namespace Sprint0Game
{
    public class BrickBlockState : IBlockState
    {
        public int Height { get { return this.Sprite.Height; } }
        public int Width { get { return this.Sprite.Width; } }
        private AnimatedBlock Sprite;
        private Block Block;

        public BrickBlockState(Block block)
        {
            this.Block = block;
            this.Sprite = new AnimatedBlock(SpriteHolder.Blocks, SpriteHolder.BrickBlock, block);
        }

        public void Update()
        {
            this.Sprite.Update();
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            this.Sprite.Draw(spriteBatch, camera);
        }

        public void Bump()
        {
            this.Block.Bumped = true;
        }

        public void Break()
        {
            this.Sprite.Destroy();
        }
    }
}
