using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Collections;

namespace Sprint0Game
{
    public class KeyboardController : IController
    {
        private ArrayList MotionKeys;
        private Dictionary<Keys, ICommand> CommandDict;
        private Keybinder Keybinds;
        private Keys UpKey = Keys.Z;
        private ILevel Level;
        private bool AttackHasBeenPressed;
	
        public KeyboardController(Game game, ILevel level)
        {
            this.Level = level;
            this.Keybinds = game.Keybinds;

            ArrayList GameKeys = new ArrayList();
            this.MotionKeys = new ArrayList();
            UpKey = Keybinds.JumpKeybind.key;

            GameKeys.Add(Keybinds.QuitKeybind.key);
            GameKeys.Add(Keybinds.JumpKeybind.key);
            GameKeys.Add(Keybinds.CrouchKeybind.key);
            GameKeys.Add(Keybinds.LeftKeybind.key);
            GameKeys.Add(Keybinds.RightKeybind.key);
            GameKeys.Add(Keybinds.AttackKeybind.key);
            GameKeys.Add(Keybinds.PauseKeybind.key);

            this.MotionKeys.Add(Keybinds.LeftKeybind.key);
            this.MotionKeys.Add(Keybinds.RightKeybind.key);
            this.MotionKeys.Add(UpKey);
            this.MotionKeys.Add(Keybinds.CrouchKeybind.key);

            //create dictionary of keys and commands
            this.CommandDict = new Dictionary<Keys, ICommand>();
            this.CommandDict.Add((Keys)GameKeys[0], new ShowMenuCommand(game));
            this.CommandDict.Add((Keys)GameKeys[1], new UpCommand(level));
            this.CommandDict.Add((Keys)GameKeys[2], new DownCommand(level));
            this.CommandDict.Add((Keys)GameKeys[3], new LeftCommand(level));
            this.CommandDict.Add((Keys)GameKeys[4], new RightCommand(level));
            this.CommandDict.Add((Keys)GameKeys[5], new FireballCommand(level));
            this.CommandDict.Add((Keys)GameKeys[6], new PauseCommand(level));
        }

        public void Update()
        {

            KeyboardState state = Keyboard.GetState();
            Keys[] pressedKeys = state.GetPressedKeys();
            ArrayList arr = new ArrayList(pressedKeys);

            if (!UpPressed(pressedKeys)) 
            {
                ICommand command = new UpReleasedCommand(this.Level);
                command.Execute();
            }

            if (!AnyMotionKeysPressed(pressedKeys))
            {
                ICommand command = new NoKeyCommand(this.Level);
                command.Execute();
            } 

            //iterate through pressed keys. if key is in dict, execute mapped command
            for (int i = 0; i < pressedKeys.Length; i++)
            {
                Microsoft.Xna.Framework.Input.Keys key = pressedKeys[i];
                ICommand command;
                //check that lookup was successful before executing
                bool validKey = this.CommandDict.TryGetValue(key, out command);
                if (command is FireballCommand)
                {
                    if (!AttackHasBeenPressed)
                        command.Execute();
                    AttackHasBeenPressed = true;
                }
                else if (validKey)
                {
                    command.Execute();
                }
            }

            if (!arr.Contains(Keybinds.AttackKeybind.key))
                AttackHasBeenPressed = false;
        }

        private bool UpPressed(Keys[] keys)
        {
            bool pressed = false;
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i] == this.UpKey)
                {
                    pressed = true;
                }
            }
            return pressed;
        }

        private bool AnyMotionKeysPressed(Keys[] keys)
        {
            bool pressed = false;
            for (int i = 0; i < keys.Length; i++)
            {
                if (this.MotionKeys.Contains(keys[i])) 
                {
                    pressed = true;
                }
            }
            return pressed;
        }

    }
}
