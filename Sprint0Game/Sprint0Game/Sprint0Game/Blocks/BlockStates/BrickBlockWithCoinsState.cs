using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Collections;

namespace Sprint0Game
{
    public class BrickBlockWithCoinsState : IBlockState
    {
        public int Height { get { return this.Sprite.Height; } }
        public int Width { get { return this.Sprite.Width; } }
        private AnimatedBlock Sprite;
        private Block Block;
        private int CoinsLeftCounter;

        public BrickBlockWithCoinsState(Block block)
        {
            this.CoinsLeftCounter = 10;
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
            this.Block.SpawnItemFromBlock();
            this.CoinsLeftCounter--;
            if(CoinsLeftCounter<1)
                this.Block.CurrentState=new UsedBlockState(this.Block);
        }

        public void Break()
        {

        }
    }
}
