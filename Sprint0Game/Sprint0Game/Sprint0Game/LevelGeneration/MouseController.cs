using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint0Game
{
    public class MouseController : IController
    {
        private Game Game;

        public MouseController(Game game)
        {
            this.Game = game;
            Game.IsMouseVisible = true;
        }
    
        public void Update()
        {
            MouseState mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                //Left-Click Command
            }
            else if (mouse.RightButton == ButtonState.Pressed)
            {
                //Right-Click Command
            }

        }
    }
}
