using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.Collections.Generic;

namespace Sprint0Game
{
    public class FloorBlockState : IBlockState
    {
        public int Height { get { return this.Sprite.Height; } }
        public int Width { get { return this.Sprite.Width; } }
        private AnimatedBlock Sprite;
        private Block Block;

        public FloorBlockState(Block block)
        {
            this.Block = block;
            this.Sprite = new AnimatedBlock(SpriteHolder.Blocks, SpriteHolder.FloorBlock, block);
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
            //Block can't break, but in future implementations it might
        }

    }
}
