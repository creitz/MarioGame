using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Collections;

namespace Sprint0Game
{
    public class Keybinder
    {
        public IKeybind LeftKeybind { get; private set; }
        public IKeybind RightKeybind { get; private set; }
        public IKeybind JumpKeybind { get; private set; }
        public IKeybind CrouchKeybind { get; private set; }
        public IKeybind AttackKeybind { get; private set; }
        public IKeybind PauseKeybind { get; private set; }
        public IKeybind QuitKeybind { get; private set; }
        private string DummyKey = "dummyKey";

        public Keybinder()
        {
            //default keybinds
            LeftKeybind = new Keybind(Keys.Left);
            RightKeybind = new Keybind(Keys.Right);
            JumpKeybind = new Keybind(Keys.Z);
            CrouchKeybind = new Keybind(Keys.Down);
            AttackKeybind = new Keybind(Keys.X);
            PauseKeybind = new Keybind(Keys.P);
            QuitKeybind = new Keybind(Keys.Q);
        }

        public string keyToString(Keys keybind)
        {
            //The credit for this code goes to Roy Triesscheijn, who posted it online at http://roy-t.nl/index.php/2010/02/11/code-snippet-converting-keyboard-input-to-text-in-xna/
            //I've slightly modified the code for purposes relating to this game project but all credit should go to him.

            //Alphabet keys
            string trial = CheckAlphabeticKeys(keybind);
            if (!trial.Equals(DummyKey))
                return trial;

            //Decimal keys
            trial = CheckDKeys(keybind);
            if (!trial.Equals(DummyKey))
                return trial;

            //Decimal numpad keys
            trial = CheckNumPadKeys(keybind);
            if (!trial.Equals(DummyKey))
                return trial;

            //Special keys
            trial = CheckSpecialKeys(keybind);
            if (!trial.Equals(DummyKey))
                return trial;

            //Arrow keys
            trial = CheckArrowKeys(keybind);
            if (!trial.Equals(DummyKey))
                return trial;

            return "Unbound";
        }

        private string CheckAlphabeticKeys(Keys keybind)
        {
            string key;
            switch (keybind)
            {
                case Keys.A: { key = "A"; } return key;
                case Keys.B: { key = "B"; } return key;
                case Keys.C: { key = "C"; } return key;
                case Keys.D: { key = "D"; } return key;
                case Keys.E: { key = "E"; } return key;
                case Keys.F: { key = "F"; } return key;
                case Keys.G: { key = "G"; } return key;
                case Keys.H: { key = "H"; } return key;
                case Keys.I: { key = "I"; } return key;
                case Keys.J: { key = "J"; } return key;
                case Keys.K: { key = "K"; } return key;
                case Keys.L: { key = "L"; } return key;
                case Keys.M: { key = "M"; } return key;
                case Keys.N: { key = "N"; } return key;
                case Keys.O: { key = "O"; } return key;
                case Keys.P: { key = "P"; } return key;
                case Keys.Q: { key = "Q"; } return key;
                case Keys.R: { key = "R"; } return key;
                case Keys.S: { key = "S"; } return key;
                case Keys.T: { key = "T"; } return key;
                case Keys.U: { key = "U"; } return key;
                case Keys.V: { key = "V"; } return key;
                case Keys.W: { key = "W"; } return key;
                case Keys.X: { key = "X"; } return key;
                case Keys.Y: { key = "Y"; } return key;
                case Keys.Z: { key = "Z"; } return key;
            }
            return DummyKey;
        }

        private string CheckDKeys(Keys keybind)
        {
            string key;
            switch (keybind)
            {
                case Keys.D0: { key = "0"; } return key;
                case Keys.D1: { key = "1"; } return key;
                case Keys.D2: { key = "2"; } return key;
                case Keys.D3: { key = "3"; } return key;
                case Keys.D4: { key = "4"; } return key;
                case Keys.D5: { key = "5"; } return key;
                case Keys.D6: { key = "6"; } return key;
                case Keys.D7: { key = "7"; } return key;
                case Keys.D8: { key = "8"; } return key;
                case Keys.D9: { key = "9"; } return key;
            }
            return DummyKey;
        }

        private string CheckNumPadKeys(Keys keybind)
        {
            string key;
            switch (keybind)
            {
                case Keys.NumPad0: key = "0"; return key;
                case Keys.NumPad1: key = "1"; return key;
                case Keys.NumPad2: key = "2"; return key;
                case Keys.NumPad3: key = "3"; return key;
                case Keys.NumPad4: key = "4"; return key;
                case Keys.NumPad5: key = "5"; return key;
                case Keys.NumPad6: key = "6"; return key;
                case Keys.NumPad7: key = "7"; return key;
                case Keys.NumPad8: key = "8"; return key;
                case Keys.NumPad9: key = "9"; return key;
            }
            return DummyKey;
        }

        private string CheckSpecialKeys(Keys keybind)
        {
            string key;
            switch (keybind)
            {
                case Keys.OemTilde: { key = "`"; } return key;
                case Keys.OemSemicolon: { key = ";"; } return key;
                case Keys.OemQuotes: { key = "\'"; } return key;
                case Keys.OemQuestion: { key = "/"; } return key;
                case Keys.OemPlus: { key = "="; } return key;
                case Keys.OemPipe: { key = "\\"; } return key;
                case Keys.OemPeriod: { key = "."; } return key;
                case Keys.OemOpenBrackets: { key = "["; } return key;
                case Keys.OemCloseBrackets: { key = "]"; } return key;
                case Keys.OemMinus: { key = "-"; } return key;
                case Keys.OemComma: { key = ","; } return key;
                case Keys.Space: key = "Space"; return key;
            }
            return DummyKey;
        }

        private string CheckArrowKeys(Keys keybind)
        {
            string key;
            switch (keybind)
            {
                case Keys.Up: { key = "Up"; } return key;
                case Keys.Down: { key = "Down"; } return key;
                case Keys.Left: { key = "Left"; } return key;
                case Keys.Right: { key = "Right"; } return key;
            }
            return DummyKey;
        }
    }
}
