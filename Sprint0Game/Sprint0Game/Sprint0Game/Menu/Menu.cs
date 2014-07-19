using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Sprint0Game
{
    public class Menu
    {
        public bool PreviousHasBeenUnpressed { get; set; }
        public bool NextHasBeenUnpressed { get; set; }
        public bool SelectHasBeenUnpressed { get; set; }
        public bool GettingKeybind { get; set; }
        private Keybinder Keybinds;
        private Game Game;
        private MenuKeyboardController KeyboardController;
        private Dictionary<String, ICommand> ChoiceDict;
        private Dictionary<String, ICommand> KeybindChoiceDict;
        private ArrayList Buttons;
        private ArrayList ButtonXCoords;
        private ArrayList KeybindButtons;
        private ArrayList KeybindButtonYCoords;
        private SpriteFont Font;
        private Texture2D MarioImg;
        private Texture2D Background;
        private Texture2D Logo;
        private int WindowWidth, WindowHeight;
        private int CursorPosition = 0;
        private enum Page { LevelCreator, Home, Keybindings }
        private Page ShowingPage;
        private Texture2D SpeakerOn = SpriteHolder.SpeakerOn;
        private Texture2D SpeakerOff = SpriteHolder.SpeakerOff;
        private Vector2 SpeakerPos;
        private LevelGenerator Generator;
 
        public Menu(Game game, int windowWidth, int windowHeight)
        {
            this.Game = game;
            this.Keybinds = game.Keybinds;
            this.ShowingPage = Page.Home;
            this.KeyboardController = new MenuKeyboardController(this);
            this.WindowWidth = windowWidth;
            this.WindowHeight = windowHeight;
            this.Font = SpriteHolder.MenuFont;
            this.MarioImg = SpriteHolder.SmallMario;
            this.Background = SpriteHolder.Background;
            this.Logo = SpriteHolder.Game_Logo;
            this.Generator = new LevelGenerator(this.Game, this.WindowWidth, this.WindowHeight);
            InitButtons();
            InitKeybindButtons();
        }

        private void InitButtons()
        {
            this.Buttons = new ArrayList();
            this.ChoiceDict = new Dictionary<String, ICommand>();
            this.Buttons.Add("Play");
            this.ChoiceDict.Add("Play", new PlayMenuCommand(this.Game));
            this.Buttons.Add("Horde Mode");
            this.ChoiceDict.Add("Horde Mode", new PlayHordeCommand(this.Game));
            this.Buttons.Add("Music");
            this.ChoiceDict.Add("Music", new ToggleMusicCommand(this.Game));
            this.Buttons.Add("Level Creator");
            this.ChoiceDict.Add("Level Creator", new LaunchLevelCreationPageCommand(this));
            this.Buttons.Add("Keybindings");
            this.ChoiceDict.Add("Keybindings", new LaunchKeybindingsMenuCommand(this));
            this.Buttons.Add("Quit");
            this.ChoiceDict.Add("Quit", new GameQuitCommand(this.Game));

            this.ButtonXCoords = new ArrayList();
            int buttonXCoord = MenuConfig.FirstMenuButtonX;
            for (int i = 0; i < this.Buttons.Count; i++)
            {
                if (i > 0)
                {
                    buttonXCoord += ButtonWidth((String)this.Buttons[i-1]);
                    buttonXCoord += MenuConfig.MenuButtonsSpacing;
                }
                this.ButtonXCoords.Add(buttonXCoord);

                if (this.Buttons[i].Equals("Music")) 
                    SpeakerPos = new Vector2(buttonXCoord + ButtonWidth((String)this.Buttons[i]) + MenuConfig.SpeakerBuffer, MenuConfig.SpeakerSymbolY);
            }
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
                    buttonYCoord += ButtonHeight((String)this.KeybindButtons[i-1]);
                    buttonYCoord += MenuConfig.KeybindMenuButtonsSpacing;
                }
                this.KeybindButtonYCoords.Add(buttonYCoord);
        }
        }

        public void Update()
        {
            this.KeyboardController.Update();
            if (GettingKeybind && ShowingPage == Page.Keybindings)
            {
                ICommand command;
                if (this.KeybindChoiceDict.TryGetValue((String)this.KeybindButtons[CursorPosition], out command))
                {
                        command.Execute();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (ShowingPage)
            {
                case Page.Home:
                    spriteBatch.Draw(Background, new Rectangle(0, 0, this.WindowWidth,
                        this.WindowHeight), Color.White);
                    Rectangle logoSourceRect = new Rectangle(0, 0, this.Logo.Width, this.Logo.Height);
                    Rectangle logoDrawnRect = new Rectangle((int)MenuConfig.LogoPos.X, (int)MenuConfig.LogoPos.Y, logoSourceRect.Width,
                        logoSourceRect.Height);

                    spriteBatch.Draw(this.Logo, logoDrawnRect, logoSourceRect, Color.White);
                    DrawButtons(spriteBatch);
                    DrawCursor(spriteBatch);
                    break;
                case Page.LevelCreator:
                    spriteBatch.Draw(Background, new Rectangle(0, 0, this.WindowWidth, this.WindowHeight), Color.White);
                    Generator.Update();
                    Generator.Draw(spriteBatch);
                    break;
                case Page.Keybindings:
                    spriteBatch.Draw(Background, new Rectangle(0, 0, this.WindowWidth,
                        this.WindowHeight), Color.White);
                    DrawKeybindButtons(spriteBatch);
                    DrawKeybinds(spriteBatch);
                    DrawKeybindCursor(spriteBatch);
                    break;
            }
        }

        private void DrawCursor(SpriteBatch spriteBatch)
        { 
            int width = SpriteHolder.SmallMarioWidth;
            int height = this.MarioImg.Height;
            int frame = SpriteHolder.IdleMarioFrame;
            Rectangle sourceRect = new Rectangle(frame * width, 0, width, height);
            Rectangle destRect = new Rectangle((int)this.ButtonXCoords[this.CursorPosition] - (int)(1.5*width), 
                MenuConfig.CursorY, width, height);
            spriteBatch.Draw(this.MarioImg, destRect, sourceRect, Color.White);
        }

        private void DrawButtons(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < this.Buttons.Count; i++)
            {
                spriteBatch.DrawString(Font, (String)this.Buttons[i], new Vector2((int)ButtonXCoords[i],
                    MenuConfig.MenuButtonsY), Color.White);
                if (this.Buttons[i].Equals("Music"))
                {
                    Texture2D speaker = this.Game.MusicShouldPlay ? SpeakerOn : SpeakerOff;
                    Rectangle sourceRectangle = new Rectangle(0, 0, speaker.Width, speaker.Height);
                    Rectangle drawnRectangle = new Rectangle((int)this.SpeakerPos.X, (int)this.SpeakerPos.Y, speaker.Width, speaker.Height);
                    spriteBatch.Draw(speaker, drawnRectangle, sourceRectangle, Color.White);
                }
            }
        }

        private void DrawKeybindCursor(SpriteBatch spriteBatch)
        {
            int width = SpriteHolder.SmallMarioWidth;
            int height = this.MarioImg.Height;
            int frame = SpriteHolder.IdleMarioFrame;
            Rectangle sourceRect = new Rectangle(frame * width, 0, width, height);
            Rectangle destRect = new Rectangle(MenuConfig.CursorX, (int)this.KeybindButtonYCoords[this.CursorPosition] + MenuConfig.MarioKeybindYDiff, width, height);
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

        public void Choose()
        {
            ICommand command;
            switch (ShowingPage)
            {
                case Page.Home:
                    if (this.ChoiceDict.TryGetValue((String)this.Buttons[CursorPosition], out command))
                    {
                        command.Execute();
                    }
                    break;
                case Page.Keybindings:
                    if (this.KeybindChoiceDict.TryGetValue((String)this.KeybindButtons[CursorPosition], out command))
                    {
                        if (command is LaunchMainMenuCommand)
                        {
                            command.Execute();
                        }
                        else
                        {
                            if (GettingKeybind)
                            {
                                GettingKeybind = false;
                            }
                            else
                            {
                                GettingKeybind = true;
                            }
                        }
                    }
                    break;
                case Page.LevelCreator:
                    Generator = new LevelGenerator(this.Game, this.WindowWidth, this.WindowWidth);
                    break;
            }
        }

        public void MoveCursorToNext()
        {
            if (!GettingKeybind)
            {
                switch (ShowingPage)
                {
                    case Page.Home:
                        this.CursorPosition = (this.CursorPosition + 1) % Buttons.Count;
                        break;
                    case Page.Keybindings:
                        this.CursorPosition = (this.CursorPosition + 1) % KeybindButtons.Count;
                        break;
                    case Page.LevelCreator:
                        break;
                }
            }
        }

        public void MoveCursorToPrevious()
        {
            if (!GettingKeybind)
            {
            if (this.CursorPosition == 0)
                this.CursorPosition = this.Buttons.Count - 1;
            else 
                this.CursorPosition--;
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

        public void ShowLevelCreationPage()
        {
            this.ShowingPage = Page.LevelCreator;
        }

        public void ShowHomePage()
        {
            this.CursorPosition = 0;
            this.ShowingPage = Page.Home;
        }

        public void ShowKeybindingsPage()
        {
            this.CursorPosition = 0;
            this.ShowingPage = Page.Keybindings;
        }
    }
}
