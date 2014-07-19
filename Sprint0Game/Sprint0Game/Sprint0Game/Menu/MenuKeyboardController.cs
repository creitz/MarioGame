using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Collections;

namespace Sprint0Game
{
    public class MenuKeyboardController : IController
    {
        private Dictionary<Keys, ICommand> CommandDict;
        private Dictionary<Keys, ICommand> UnpressedCommandDict;
        private ArrayList UsedKeys = new ArrayList();
        public Keys LastKey { get; set; }
	
        public MenuKeyboardController(Menu menu)
        {
            //create dictionary of keys and commands
            this.UsedKeys.Add(Keys.Left);
            this.UsedKeys.Add(Keys.Right);
            this.UsedKeys.Add(Keys.Enter);

            this.CommandDict = new Dictionary<Microsoft.Xna.Framework.Input.Keys, ICommand>();
            this.UnpressedCommandDict = new Dictionary<Microsoft.Xna.Framework.Input.Keys, ICommand>();
            this.CommandDict.Add((Keys)UsedKeys[0], new LeftMenuCommand(menu));
            this.UnpressedCommandDict.Add((Keys)UsedKeys[0], new LeftMenuUnpressedCommand(menu));
            this.CommandDict.Add((Keys)UsedKeys[1], new RightMenuCommand(menu));
            this.UnpressedCommandDict.Add((Keys)UsedKeys[1], new RightMenuUnpressedCommand(menu));
            this.CommandDict.Add((Keys)UsedKeys[2], new SelectMenuCommand(menu));
            this.UnpressedCommandDict.Add((Keys)UsedKeys[2], new SelectMenuUnpressedCommand(menu));
        }
           
        public void Update()
        {
            KeyboardState state = Keyboard.GetState();
            Keys[] pressedKeys = state.GetPressedKeys();
            //iterate through pressed keys. if key is in dict, execute mapped command
            for (int i = 0; i < pressedKeys.Length; i++)
            {
                LastKey = pressedKeys[0];
                Microsoft.Xna.Framework.Input.Keys key = pressedKeys[i];
                ICommand command;
                //check that lookup was successful before executing
                bool validKey = this.CommandDict.TryGetValue(key, out command);
                if (validKey)
                {
                    command.Execute();
                }
            }

            for (int i=0; i < UsedKeys.Count; i++) {
                if (!KeyPressed((Keys)UsedKeys[i], pressedKeys))
                {
                    ICommand command;
                    if (this.UnpressedCommandDict.TryGetValue((Keys)UsedKeys[i], out command))
                    {
                        command.Execute();
                    }
                }
            }

        }

        private static bool KeyPressed(Keys key, Keys[] pressedKeys)
        {
            ArrayList list = new ArrayList(pressedKeys);
            return list.Contains(key);
        }
    }
}
