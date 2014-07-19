using System;
using System.Collections;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public interface IBlockState
    {
        int Height { get; }
        int Width { get; }

        void Bump();
        void Break();
        void Update();
        void Draw(SpriteBatch spriteBatch, ICamera camera);
    }
}
