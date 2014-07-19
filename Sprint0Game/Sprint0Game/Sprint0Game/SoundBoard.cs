using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint0Game
{
    public static class SoundBoard
    {
        public static Song BackgroundMusic { get; private set; }
        public static SoundEffect JumpSound { get; private set; }
        public static SoundEffect MarioDeath { get; private set; }
        public static SoundEffect GameOver { get; private set; }
        public static SoundEffect StageClear { get; private set; }
        public static SoundEffect OneUp { get; private set; }
        public static SoundEffect BrickSmash { get; private set; }
        public static SoundEffect Bump { get; private set; }
        public static SoundEffect Coin { get; private set; }
        public static SoundEffect DownTheFlagpole { get; private set; }
        public static SoundEffect Fireball { get; private set; }
        public static SoundEffect Pause { get; private set; }
        public static SoundEffect PowerDown { get; private set; }
        public static SoundEffect PowerUp { get; private set; }
        public static SoundEffect PowerUpAppears { get; private set; }
        public static SoundEffect Stomp { get; private set; }

        public static void LoadSounds(Microsoft.Xna.Framework.Game game)
        {
            BackgroundMusic = game.Content.Load<Song>("music");
            JumpSound = game.Content.Load<SoundEffect>("smb_jump");
            MarioDeath = game.Content.Load<SoundEffect>("smb_mariodeath");
            GameOver = game.Content.Load<SoundEffect>("smb_gameover");
            StageClear = game.Content.Load<SoundEffect>("smb_stage_clear");
            OneUp = game.Content.Load<SoundEffect>("sound_1up");
            BrickSmash = game.Content.Load<SoundEffect>("sound_breakblock");
            Bump = game.Content.Load<SoundEffect>("smb_bump");
            Coin = game.Content.Load<SoundEffect>("smb_coin");
            DownTheFlagpole = game.Content.Load<SoundEffect>("smb_flagpole");
            Fireball = game.Content.Load<SoundEffect>("smb_fireball");
            Pause = game.Content.Load<SoundEffect>("smb_pause");
            PowerDown = game.Content.Load<SoundEffect>("smb_pipe");
            PowerUp = game.Content.Load<SoundEffect>("smb_powerup");
            PowerUpAppears = game.Content.Load<SoundEffect>("smb_powerup_appears");
            Stomp = game.Content.Load<SoundEffect>("smb_stomp");
        }
    }
}
