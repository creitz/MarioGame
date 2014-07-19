
namespace Sprint0Game
{
    public class CoinCollisionResponder
    {
        private Coin Coin;

        public CoinCollisionResponder(Coin coin)
        {
            this.Coin = coin;
        }

        public void RespondToCollision(IObject obj)
        {
            if (obj is IMario)
            {
                RespondToCollisionWithMario();
            }            
        }

        private void RespondToCollisionWithMario()
        {
            SoundBoard.Coin.Play();
            this.Coin.Collect();
        }
    }
}
