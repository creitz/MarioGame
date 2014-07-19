using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint0Game
{
    public class Creatable
    {
        public Texture2D Sprite {get; set;}
        public String EncodingString { get; set; }

        public Creatable(Texture2D DisplaySprite, String EncodingString)
        {
            this.Sprite = DisplaySprite;
            this.EncodingString = EncodingString;
        }

        public Button MakeButton(Vector2 Position)
        {
            return (new Button(Position, this.Sprite, false));
        }      
    }
}
