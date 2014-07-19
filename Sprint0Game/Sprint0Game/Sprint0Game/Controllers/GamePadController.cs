using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Sprint0Game
{
    public class GamePadController : IController
    {
        private Game Game;
        private List<Buttons> ButtonList;
        private Dictionary<Buttons, ICommand> CommandDict;

        public GamePadController(Game game)
        {
            this.Game = game;

            //create list of used buttons
            this.ButtonList = new List<Buttons>();
            this.ButtonList.Add(Buttons.A);
            this.ButtonList.Add(Buttons.LeftThumbstickDown);
            this.ButtonList.Add(Buttons.LeftThumbstickLeft);
            this.ButtonList.Add(Buttons.LeftThumbstickRight);
            this.ButtonList.Add(Buttons.B);

            //create dictionary of buttons with commands
            this.CommandDict = new Dictionary<Buttons, ICommand>();
            this.CommandDict.Add(this.ButtonList.ElementAt(0), new UpCommand(this.Game.CurrentLevel));
            this.CommandDict.Add(this.ButtonList.ElementAt(1), new DownCommand(this.Game.CurrentLevel));
            this.CommandDict.Add(this.ButtonList.ElementAt(2), new LeftCommand(this.Game.CurrentLevel));
            this.CommandDict.Add(this.ButtonList.ElementAt(3), new RightCommand(this.Game.CurrentLevel));
            this.CommandDict.Add(this.ButtonList.ElementAt(4), new FireballCommand(this.Game.CurrentLevel));
        }

        public void Update()
        {
            //loop through buttons
            for (int i = 0; i < this.ButtonList.Count(); i++)
            {
                //check if button is pressed
                Buttons button = ButtonList.ElementAt(i);
                if (GamePad.GetState(PlayerIndex.One).IsButtonDown(button))
                {
                    //execute corresponding command
                    ICommand command;
                    bool successLookup = this.CommandDict.TryGetValue(button, out command);
                    if (successLookup)
                    {
                        command.Execute();
                    }
                }
            }

        }

    }
}
