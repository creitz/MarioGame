using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class Block : IObject
    {
        public IBlockState CurrentState { get; set; }
        public Vector2 CurrentPosition { get; set; }
        public Vector2 CurrentVelocity { get; set; }
        public int Height { get { return this.CurrentState.Height; } }
        public int Width { get { return this.CurrentState.Width; } }
        public bool ShouldBeRemoved { get; set; }
        public bool HasBeenReached { get; set; }
        public bool Bumped { get; set; }
        public bool ShouldCheckCollisions { get; private set; }
        public ItemDescriptor BlockItem { get; private set; }
        private BlockCollisionResponder CollisionResponder;
        private ILevel Level;

        public Block(Vector2 position, BlockStateDescriptor state, ItemDescriptor blockItem, ILevel level, bool checkCollision)
        {
            this.ShouldCheckCollisions = checkCollision;
            this.CurrentPosition = position;
            this.Level = level;
            this.CollisionResponder = new BlockCollisionResponder(this);
            this.BlockItem = blockItem;

            switch (state)
            {
                case BlockStateDescriptor.Brick:
                    this.CurrentState = new BrickBlockState(this);
                    break;
                case BlockStateDescriptor.Floor:
                    this.CurrentState = new FloorBlockState(this);
                    break;
                case BlockStateDescriptor.Hidden:
                    this.CurrentState = new HiddenBlockState(this);
                    break;
                case BlockStateDescriptor.Question:
                    this.CurrentState = new QuestionBlockState(this);
                    break;
                case BlockStateDescriptor.Stair:
                    this.CurrentState = new StairBlockState(this);
                    break;
                case BlockStateDescriptor.Used:
                    this.CurrentState = new UsedBlockState(this);
                    break;
            }
        }

        public void Update()
        {
            this.CurrentState.Update();
        }

        public void Bump()
        {
            this.CurrentState.Bump();
        }

        public void Break()
        {
            this.CurrentState.Break();
        }

        public void SpawnItemFromBlock()
        {
            this.Level.SpawnItemFromBlock(this);
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            this.CurrentState.Draw(spriteBatch, camera);
        }

        public void RespondToCollision(Side side, IObject obj, Rectangle intersectRect)
        {
            this.CollisionResponder.RespondToCollision(side, obj);
        }

        public void RespondToNoCollision() { }

        public bool IsHidden()
        {
            return this.CurrentState is HiddenBlockState;
        }

        public bool IsDestroyed()
        {
            return this.CurrentState is DestroyedBlockState;
        }
    }
}
