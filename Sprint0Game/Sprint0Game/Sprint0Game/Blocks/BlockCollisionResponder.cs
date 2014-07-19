
namespace Sprint0Game
{
    public class BlockCollisionResponder
    {
        private Block Block;

        public BlockCollisionResponder(Block block)
        {
            this.Block = block;
        }

        public void RespondToCollision(Side side, IObject obj)
        {
            if (obj is IMario)
            {
                RespondToCollisionWithMario(side, (Mario)obj);
            }            
        }

        private void RespondToCollisionWithMario(Side side, Mario mario)
        {
            if (IsMetalMarioAndWillBreakBlock(side, mario))
            {
                this.Block.Break();
                SoundBoard.BrickSmash.Play();
            }
            else if (SideGeneralizer.IsBottom(side))
            {
                if (this.Block.CurrentState is QuestionBlockState)
                {
                    SoundBoard.Bump.Play();
                    this.Block.Bump();
                }
                else if (this.Block.CurrentState is BrickBlockState && mario.IsBig())
                {
                    SoundBoard.BrickSmash.Play();
                    this.Block.Break();
                }
                else if (this.Block.CurrentState is BrickBlockState && !mario.IsBig())
                {
                    SoundBoard.Bump.Play();
                    this.Block.Bump();
                }
                else if (this.Block.CurrentState is HiddenBlockState && !mario.IsFalling())
                {
                    SoundBoard.Bump.Play();
                    this.Block.Bump();
                }
            }
        }

        private bool IsMetalMarioAndWillBreakBlock(Side side, IMario mario)
        {
            return mario.PowerLevel() == MarioPowerLevel.Metal && this.Block.CurrentState is BrickBlockState
                && !SideGeneralizer.IsTop(side);
        }
    }
}
