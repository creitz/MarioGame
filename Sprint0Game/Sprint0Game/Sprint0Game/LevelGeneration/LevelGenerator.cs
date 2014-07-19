using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint0Game
{
    public class LevelGenerator
    {
        private String LevelName { get; set; }
        private Creatable[,] LevelMap;
        private Game Game;
        private ArrayList Creatables, Buttons;
        private SpriteFont Font;
        private Rectangle MouseBox;
        private Texture2D MouseTexture;
        private Dictionary<Button, Creatable> MarioButtons, ItemButtons, BlockButtons, PipeButtons, MiscButtons, EnemyButtons;
        private Button BackButton, SaveButton;
        private Point MouseLocation;
        private Creatable heldCreatable;
        private bool holdingCreatable, MarioPlaced, cursorIsVisible, FlagPlaced, CastlePlaced;
        private int WindowWidth, WindowHeight;
        

        public LevelGenerator(Game game, int windowWidth, int windowHeight)
        {

            this.Game = game;
            this.Game.IsMouseVisible = true;
            //this.Game.HUD.visible = false;
            CreatableHolder.LoadCreatables(this.Game);
            
            this.Font = SpriteHolder.MenuFont;
            this.WindowWidth = windowWidth;
            this.WindowHeight = windowHeight;
            this.LevelName = "Level_" + (this.Game.Levels.Count - 1);
            this.MouseTexture = SpriteHolder.EmptyTexture;

            this.LevelMap = new Creatable[this.WindowWidth / 16, this.WindowHeight / 16];

            this.BackButton = new Button(new Vector2(0, 0), "Back", Font, true);
            this.SaveButton = new Button(new Vector2(this.WindowWidth - SpriteHolder.DialogBox.Width, 0), "Save", Font, true);
            this.Buttons = new ArrayList();
            this.MarioButtons = new Dictionary<Button, Creatable>();
            this.ItemButtons = new Dictionary<Button, Creatable>();
            this.BlockButtons = new Dictionary<Button, Creatable>();
            this.PipeButtons = new Dictionary<Button, Creatable>();
            this.EnemyButtons = new Dictionary<Button, Creatable>();
            this.MiscButtons = new Dictionary<Button, Creatable>();
            
            this.cursorIsVisible = true;
            this.holdingCreatable = false;
            this.MarioPlaced = false;
            this.FlagPlaced = false;
            this.CastlePlaced = false;

            this.InitializeMenus();
        }

        public void Update()
        {
            //mouse positioning
            MouseState m = Mouse.GetState();
            int mouseX = (int)m.X / 16;
            int mouseY = (int)m.Y / 16;
            MouseLocation = new Point(m.X, m.Y);
            MouseBox = new Rectangle(mouseX * 16, mouseY * 16, MouseTexture.Width, MouseTexture.Height);

            BackButton.Update(m);

            if (BackButton.Pressed)
            {
                BackButton.Press();
                this.Game.Menu.ShowHomePage();
            }

            SaveButton.Update(m);
            if (SaveButton.Pressed)
            {
                if(MarioPlaced && CastlePlaced && FlagPlaced)
                {
                    LevelEncoder.Encode(this.LevelName, this.LevelMap, this.Game);
                    SaveButton.Press();
                    this.Game.Menu.ShowHomePage();
                }
            }

            foreach (Button b in Buttons)
            {
                b.Update(m);
                if (b.Pressed)
                    ModifySubmenuVisibility(b.Text, true);
                else
                    ModifySubmenuVisibility(b.Text, false);
            }

            UpdateButtonSet(MarioButtons, m);
            UpdateButtonSet(ItemButtons,m);
            UpdateButtonSet(BlockButtons,m);
            UpdateButtonSet(PipeButtons,m);
            UpdateButtonSet(MiscButtons,m);
            UpdateButtonSet(EnemyButtons,m);
            

            if (CheckIfPlaceable(MouseLocation) && holdingCreatable)
                cursorIsVisible = true;
            else
                cursorIsVisible = false;

            if (m.LeftButton == ButtonState.Pressed && cursorIsVisible)
            {
                if (m.X <= WindowWidth && m.Y <= WindowHeight && m.X >= 0 && m.Y >= 0)
                {
                    if (!MarioPlaced && MarioButtons.Values.Contains(heldCreatable))
                    {
                        LevelMap[mouseX, mouseY] = heldCreatable;
                        MarioPlaced = true;
                    }
                    else if (!FlagPlaced && heldCreatable.EncodingString == "FXX")
                    {
                        LevelMap[mouseX, mouseY] = heldCreatable;
                        FlagPlaced = true;
                    }
                    else if (!CastlePlaced && heldCreatable.EncodingString == "KXX")
                    {
                        LevelMap[mouseX, mouseY] = heldCreatable;
                        CastlePlaced = true;
                    }
                    else
                        LevelMap[mouseX, mouseY] = heldCreatable;
                }                
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.BackButton.Draw(spriteBatch);
            this.SaveButton.Draw(spriteBatch);

            foreach (Button b in Buttons)
            {
                b.Draw(spriteBatch);
            }

            spriteBatch.DrawString(Font, LevelName, new Vector2(this.WindowWidth / 2 - Font.MeasureString(LevelName).X / 2, 10), Color.Black);

            foreach (Button b in MarioButtons.Keys)
                b.Draw(spriteBatch);
            foreach (Button b in ItemButtons.Keys)
                b.Draw(spriteBatch);
            foreach (Button b in EnemyButtons.Keys)
                b.Draw(spriteBatch);
            foreach (Button b in BlockButtons.Keys)
                b.Draw(spriteBatch);
            foreach (Button b in PipeButtons.Keys)
                b.Draw(spriteBatch);
            foreach (Button b in MiscButtons.Keys)
                b.Draw(spriteBatch);

            if (holdingCreatable && cursorIsVisible)
            {
                Rectangle SourceMouseBox = new Rectangle(0, 0, this.MouseTexture.Width, this.MouseTexture.Height);
                spriteBatch.Draw(MouseTexture, MouseBox, SourceMouseBox, Color.White);
            }

            this.DrawLevel(spriteBatch);
        }

        private void ModifySubmenuVisibility(String s, bool showing)
        {
            ICollection ButtonsToShow;
            switch (s)
            {
                case "Mario":
                    ButtonsToShow = MarioButtons.Keys;
                    break;
                case "Block":
                    ButtonsToShow = BlockButtons.Keys;
                    break;
                case "Item":
                    ButtonsToShow = ItemButtons.Keys;
                    break;
                case "Pipe":
                    ButtonsToShow = PipeButtons.Keys;
                    break;
                case "Enemy":
                    ButtonsToShow = EnemyButtons.Keys;
                    break;
                default:
                    ButtonsToShow = MiscButtons.Keys;
                    break;
            }
            foreach (Button b in ButtonsToShow)
            {
                b.Visible = showing;
            }
        }

        private void UpdateCursor(Button b, Dictionary<Button, Creatable> currentButtonSet)
        {
            if (holdingCreatable && !b.Pressed && currentButtonSet[b] == heldCreatable)
            {
                holdingCreatable = false;
            }
            else if (!holdingCreatable && b.Pressed)
            {
                holdingCreatable = true;
                heldCreatable = currentButtonSet[b];
                MouseTexture = currentButtonSet[b].Sprite;
            }
        }

        private bool CheckIfPlaceable(Point loc)
        {
            if (MarioButtons.ContainsValue(heldCreatable) && MarioPlaced)
                return false;
            else if (OnButtonSet(Buttons, loc) || OnButtonSet(MarioButtons.Keys, loc) || OnButtonSet(BlockButtons.Keys, loc) ||
                        OnButtonSet(ItemButtons.Keys, loc) || OnButtonSet(EnemyButtons.Keys, loc) ||
                        OnButtonSet(MiscButtons.Keys, loc) || OnButtonSet(PipeButtons.Keys, loc))
                return false;
            return true;
        }

        private static bool OnButtonSet(ICollection ButtonSet, Point Loc)
        {
            foreach (Button b in ButtonSet)
            {
                if (b.Bounds.Contains(Loc) && b.Visible)
                {
                    return true;
                }
            }
            return false;
        }

        private void DrawLevel(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < LevelMap.GetLength(0); i++)
            {
                for (int j = 0; j < LevelMap.GetLength(1); j++)
                {
                    if (!(LevelMap[i, j] == null))
                    {
                        Texture2D Sprite = LevelMap[i, j].Sprite;
                        int width = Sprite.Width;
                        int height = Sprite.Height;
                        int xPos = 16 * i;
                        int yPos = 16 * j;

                        Rectangle DrawnRectangle = new Rectangle(xPos, yPos, width, height);
                        spriteBatch.Draw(Sprite, DrawnRectangle, Color.White);
                    }

                }
            }

        }

        private void InitializeMenus()
        {
            int count = 0;
            this.Creatables = new ArrayList() { "Mario", "Block", "Item", "Enemy", "Pipe", "Misc" };
            foreach (String c in Creatables)
            {
                Vector2 Location = new Vector2((count * WindowWidth / Creatables.Count) + 8, 50);
                Button b = new Button(Location, c, Font, true);
                this.Buttons.Add(b);
                count++;
            }

            int SetCount = 0;
            int numCreatables = CreatableHolder.Creatables.Count;
            foreach (ArrayList CreatableSet in CreatableHolder.Creatables)
            {
                int OptionCount = 0;
                int verticalOffset = 50;
                int horizontalOffset = 0;
                foreach (Creatable Option in CreatableSet)
                {

                    float Xpos = (SetCount * (WindowWidth / numCreatables) + 12) + horizontalOffset;
                    float Ypos = 50 + verticalOffset;
                    Vector2 Position = new Vector2(Xpos, Ypos);
                    Button b = Option.MakeButton(Position);
                    if (CreatableSet == CreatableHolder.Mario)
                        MarioButtons.Add(b, Option);
                    else if (CreatableSet == CreatableHolder.Block)
                        BlockButtons.Add(b, Option);
                    else if (CreatableSet == CreatableHolder.Item)
                        ItemButtons.Add(b, Option);
                    else if (CreatableSet == CreatableHolder.Pipe)
                        PipeButtons.Add(b, Option);
                    else if (CreatableSet == CreatableHolder.Enemy)
                        EnemyButtons.Add(b, Option);
                    else
                        MiscButtons.Add(b, Option);
                    horizontalOffset += 36;
                    OptionCount++;
                    if (OptionCount == 3)
                    {
                        OptionCount = 0;
                        horizontalOffset = 0;
                        verticalOffset += 50;
                    }
                }
                SetCount++;
        }
    }
        private void UpdateButtonSet(Dictionary<Button, Creatable> ButtonSet, MouseState m)
        {
            foreach (Button b in ButtonSet.Keys)
            {
                b.Update(m);
                UpdateCursor(b, ButtonSet);
            }
        }
}
}
