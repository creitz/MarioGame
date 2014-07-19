using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class Castle
    {
        public Vector2 CurrentPosition { get; set; }
        public Vector2 DoorPosition { get; private set; }
        public bool HasBeenReached { get; set; }
        private CastleSprite Sprite;

        public Castle(Vector2 postion)
        {
            this.HasBeenReached = true;
            this.CurrentPosition = postion;
            this.Sprite = new CastleSprite(this);
            this.DoorPosition = new Vector2(this.CurrentPosition.X + this.Sprite.Width*(0.4f), this.CurrentPosition.Y);
        }

        public void Update()
        {
            this.Sprite.Update();
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            this.Sprite.Draw(spriteBatch, camera);
        }
    }
}
