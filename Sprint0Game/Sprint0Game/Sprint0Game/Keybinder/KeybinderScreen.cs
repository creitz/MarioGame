using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Sprint0Game
{
    public class KeybinderScreen
    {
        private Keybinder Keybinds;
        private Dictionary<String, ICommand> KeybindChoiceDict;
        private MenuKeyboardController KeyboardController;
        private ArrayList KeybindButtons;
        private ArrayList KeybindButtonYCoords;
        private SpriteFont Font;
        private int WindowWidth, WindowHeight;
        private Texture2D MarioImg;
        private Texture2D Background;

        public KeybinderScreen(Game game, int windowWidth, int windowHeight)
        {
            this.Background = SpriteHolder.Background;
            this.Keybinds = game.Keybinds;
            this.WindowWidth = windowWidth;
            this.WindowHeight = windowHeight;
            this.Font = SpriteHolder.MenuFont;
            this.MarioImg = SpriteHolder.SmallMario;
            InitKeybindButtons();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, new Rectangle(0, 0, this.WindowWidth,
                        this.WindowHeight), Color.White);
            DrawKeybindButtons(spriteBatch);
            DrawKeybinds(spriteBatch);
            DrawKeybindCursor(spriteBatch);
        }

        private void InitKeybindButtons()
        {
            this.KeybindButtons = new ArrayList();
            this.KeybindChoiceDict = new Dictionary<String, ICommand>();

            this.KeybindButtons.Add("Move Left");
            this.KeybindChoiceDict.Add("Move Left", new RebindCommand(Keybinds.LeftKeybind, KeyboardController));
            this.KeybindButtons.Add("Move Right");
            this.KeybindChoiceDict.Add("Move Right", new RebindCommand(Keybinds.RightKeybind, KeyboardController));
            this.KeybindButtons.Add("Crouch");
            this.KeybindChoiceDict.Add("Crouch", new RebindCommand(Keybinds.CrouchKeybind, KeyboardController));
            this.KeybindButtons.Add("Jump");
            this.KeybindChoiceDict.Add("Jump", new RebindCommand(Keybinds.JumpKeybind, KeyboardController));
            this.KeybindButtons.Add("Shoot");
            this.KeybindChoiceDict.Add("Shoot", new RebindCommand(Keybinds.AttackKeybind, KeyboardController));
            this.KeybindButtons.Add("Pause");
            this.KeybindChoiceDict.Add("Pause", new RebindCommand(Keybinds.PauseKeybind, KeyboardController));
            this.KeybindButtons.Add("Quit Game");
            this.KeybindChoiceDict.Add("Quit Game", new RebindCommand(Keybinds.QuitKeybind, KeyboardController));
            this.KeybindButtons.Add("Done");
            this.KeybindChoiceDict.Add("Done", new LaunchMainMenuCommand(this));

            this.KeybindButtonYCoords = new ArrayList();
            int buttonYCoord = MenuConfig.KeybindMenuButtonY;
            for (int i = 0; i < this.KeybindButtons.Count; i++)
            {
                if (i > 0)
                {
                    buttonYCoord += ButtonHeight((String)this.KeybindButtons[i - 1]);
                    buttonYCoord += MenuConfig.KeybindMenuButtonsSpacing;
                }
                this.KeybindButtonYCoords.Add(buttonYCoord);
            }
        }

        private void DrawKeybinds(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Keybinds.keyToString(Keybinds.LeftKeybind.key), new Vector2(MenuConfig.MenuButtonsX + MenuConfig.KeyOffset, (int)KeybindButtonYCoords[0]), Color.Red);
            spriteBatch.DrawString(Font, Keybinds.keyToString(Keybinds.RightKeybind.key), new Vector2(MenuConfig.MenuButtonsX + MenuConfig.KeyOffset, (int)KeybindButtonYCoords[1]), Color.Red);
            spriteBatch.DrawString(Font, Keybinds.keyToString(Keybinds.CrouchKeybind.key), new Vector2(MenuConfig.MenuButtonsX + MenuConfig.KeyOffset, (int)KeybindButtonYCoords[2]), Color.Red);
            spriteBatch.DrawString(Font, Keybinds.keyToString(Keybinds.JumpKeybind.key), new Vector2(MenuConfig.MenuButtonsX + MenuConfig.KeyOffset, (int)KeybindButtonYCoords[3]), Color.Red);
            spriteBatch.DrawString(Font, Keybinds.keyToString(Keybinds.AttackKeybind.key), new Vector2(MenuConfig.MenuButtonsX + MenuConfig.KeyOffset, (int)KeybindButtonYCoords[4]), Color.Red);
            spriteBatch.DrawString(Font, Keybinds.keyToString(Keybinds.PauseKeybind.key), new Vector2(MenuConfig.MenuButtonsX + MenuConfig.KeyOffset, (int)KeybindButtonYCoords[5]), Color.Red);
            spriteBatch.DrawString(Font, Keybinds.keyToString(Keybinds.QuitKeybind.key), new Vector2(MenuConfig.MenuButtonsX + MenuConfig.KeyOffset, (int)KeybindButtonYCoords[6]), Color.Red);
        }

        private void DrawKeybindCursor(SpriteBatch spriteBatch)
        {
            int width = SpriteHolder.SmallMarioWidth;
            int height = this.MarioImg.Height;
            int frame = SpriteHolder.IdleMarioFrame;
            Rectangle sourceRect = new Rectangle(frame * width, 0, width, height);
            Rectangle destRect = new Rectangle(MenuConfig.CursorX, (int)this.KeybindButtonYCoords[this.CursorPosition], width, height);
            spriteBatch.Draw(this.MarioImg, destRect, sourceRect, Color.White);
        }

        private void DrawKeybindButtons(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, "To change a keybind select the key and press Enter.", new Vector2(MenuConfig.KeybindInstructionsX, MenuConfig.KeybindInstructionsY), Color.White);
            spriteBatch.DrawString(Font, "Press Enter again to save your changes.", new Vector2(MenuConfig.KeybindInstructionsX, MenuConfig.KeybindInstructionsY2), Color.White);
            for (int i = 0; i < this.KeybindButtons.Count; i++)
            {
                spriteBatch.DrawString(Font, (String)this.KeybindButtons[i], new Vector2(MenuConfig.MenuButtonsX, (int)KeybindButtonYCoords[i]), Color.White);
            }
        }

        private int ButtonWidth(String button)
        {
            return (int)this.Font.MeasureString(button).X;
        }

        private int ButtonHeight(String button)
        {
            return (int)this.Font.MeasureString(button).Y;
        }
    }
}