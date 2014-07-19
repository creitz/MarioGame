
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace Sprint0Game
{
    public static class SpriteHolder
    {

        public static Texture2D Background { get; private set; }
        public static Texture2D BlackBackground { get; private set; }
        public static Texture2D Game_Logo { get; private set; }
        public static Texture2D SpeakerOn { get; private set; }
        public static Texture2D SpeakerOff { get; private set; }
        public static SpriteFont HUDFont { get; private set; }
        public static SpriteFont MenuFont { get; private set; }
        public static Texture2D Castle { get; private set; }
        public static Texture2D EmptyTexture { get; private set; }

        public static Texture2D SmallMario { get; private set; }
        public static Texture2D BigMario { get; private set; }
        public static Texture2D FireMario { get; private set; }

        public static int TotalMarioFrames { get; private set; }

        public static int IdleMarioFrame { get; private set; }
        public static int CrouchingMarioFrame { get; private set; }

        public static int TraversingMarioStartFrame { get; private set; }
        public static int TraversingMarioEndFrame { get; private set; }

        public static int SmallMarioWidth { get; private set; }
        public static int BigMarioWidth { get; private set; }

        public static int JumpingMarioFrame { get; private set; }

        public static int DeadMarioColumn { get; private set; }

        public static int StoppingMarioFrame { get; private set; }

        public static Texture2D WalkingKoopa { get; private set; }
        public static int KoopaSpriteRows { get; private set; }
        public static int KoopaSpriteFrames { get; private set; }

        public static Texture2D WalkingGoomba { get; private set; }
        public static int GoombaWidth { get; private set; }
        public static int WalkingGoombaStartFrame { get; private set; }
        public static int WalkingGoombaFrames { get; private set; }
        public static int SquashedGoombaFrame { get; private set; }

        public static Texture2D Fireball { get; private set; }
        public static int FireballFrames { get; private set; }

        public static Texture2D Koopa {get; private set;}
        public static int FlyingKoopaStartFrame{get;private set;}
        public static int FlyingKoopaFrames {get; private set;}
        public static int WalkingKoopaStartFrame{get;private set;}
        public static int WalkingKoopaFrames{get;private set;}
        public static int HidingKoopaFrame{get;private set;}
        public static int KoopaShell {get; private set;}
        public static int KoopaWidth { get; private set; }

        public static Texture2D PowerUps { get; private set; }
        public static ArrayList RedMushroomCoordinates { get; private set; }
        public static ArrayList GreenMushroomCoordinates { get; private set; }
        public static ArrayList MetalMushroomCoordinates { get; private set; }
        public static ArrayList FireFlowerStartCoordinates { get; private set; }
        public static int FireFlowerFrames { get; private set; }
        public static ArrayList StarCoordinates { get; private set; }
        public static int StarFrames { get; private set; }
        public static int PowerUpHeight { get; private set; }
        public static int PowerUpWidth { get; private set; }

        public static Texture2D Coin { get; private set; }
        public static Texture2D SingleCoin { get; private set; }
        public static int CoinRows { get; private set; }
        public static int CoinColumns { get; private set; }

        public static Texture2D Blocks { get; private set; }
        public static Texture2D DestroyedBlock { get; private set; }
        public static ArrayList QuestionBlock { get; private set; }
        public static int QuestionBlockFrames {get;private set;} 
        public static ArrayList UsedBlock { get; private set; }
        public static ArrayList BrickBlock { get; private set; }
        public static ArrayList FloorBlock { get; private set; }
        public static ArrayList StairBlock { get; private set; }
        public static ArrayList HiddenBlock { get; private set; }
        public static int BlockHeight { get; private set; }
        public static int BlockWidth { get; private set; }
        public static int BrokenBlockHeight { get; private set; }
        public static int BrokenBlockWidth { get; private set; }

        public static Texture2D UpPipes { get; private set; }
        public static Texture2D SidePipes { get; private set; }
        public static int UpPipeTotalFrames { get; private set; }
        public static int SidePipeTotalFrames { get; private set; }
        public static int PipeTopFrame { get; private set; }
        public static int PipeBodyFrame { get; private set; }
        public static int PipeMergeLeftFrame { get; private set; }
        public static int PipeMergeRightFrame { get; private set; }

        public static Texture2D Pole { get; private set; }
        public static Texture2D Flag { get; private set; }
        public static int DistanceFromTopOfPoleToFlag { get; private set; }

        public static Texture2D Pause { get; private set; }

        public static Texture2D Lakitu { get; private set; }
        public static int LakituWidth { get; private set; }
        public static int LakituStartFrame { get; private set; }
        public static int LakituFrames { get; private set; }

        public static Texture2D SpinyEgg { get; private set; }
        public static int SpinyEggWidth { get; private set; }
        public static int SpinyEggStartFrame { get; private set; }
        public static int SpinyEggFrames { get; private set; }

        public static Texture2D Spiny { get; private set; }
        public static int SpinyWidth { get; private set; }
        public static int SpinyStartFrame { get; private set; }
        public static int SpinyFrames { get; private set; }

        public static Texture2D WhiteBackground { get; private set; }
        public static Texture2D DialogBox { get; private set; }
        public static Texture2D ImageBox { get; private set; }
        
        public static void LoadSprites(Microsoft.Xna.Framework.Game game)
        {
            Game_Logo = game.Content.Load<Texture2D>("game_logo");
            Background = game.Content.Load<Texture2D>("background");
            BlackBackground = game.Content.Load<Texture2D>("BlackBackground");
            Castle = game.Content.Load<Texture2D>("Castle");
            HUDFont = game.Content.Load<SpriteFont>("HUDFont");
            MenuFont = game.Content.Load<SpriteFont>("MenuFont");
            Pause = game.Content.Load<Texture2D>("pause");
            SpeakerOn = game.Content.Load<Texture2D>("speakeron");
            SpeakerOff = game.Content.Load<Texture2D>("speakeroff");

            BigMario = game.Content.Load<Texture2D>("BigMario");
            SmallMario = game.Content.Load<Texture2D>("SmallMario");
            FireMario = game.Content.Load<Texture2D>("FireMario");
            TotalMarioFrames = 7;
            //TotalSmallMarioRows = 2;
            
            TraversingMarioEndFrame = 6;
            
            CrouchingMarioFrame = 0;
            IdleMarioFrame = 1;
            StoppingMarioFrame = 2;
            TraversingMarioStartFrame = 3;
            JumpingMarioFrame = 6;
            DeadMarioColumn = 7;
            SmallMarioWidth = 14;
            BigMarioWidth = 16;

            WalkingGoomba = game.Content.Load<Texture2D>("Goomba");
            WalkingGoombaStartFrame = 0;
            WalkingGoombaFrames = 1;
            SquashedGoombaFrame = 2;
            GoombaWidth = 16;
            
            WalkingKoopa = game.Content.Load<Texture2D>("Koopas");
            KoopaSpriteFrames = 6;
            KoopaSpriteRows = 1;

            PowerUps = game.Content.Load<Texture2D>("PowerUps");
            RedMushroomCoordinates = new ArrayList() { 1, 0 };
            GreenMushroomCoordinates = new ArrayList() { 0, 0 };
            MetalMushroomCoordinates = new ArrayList() { 2, 0 };
            FireFlowerStartCoordinates = new ArrayList() { 0, 2 };
            StarCoordinates = new ArrayList() { 0, 3 };
            FireFlowerFrames = 4;
            StarFrames = 4;
            PowerUpHeight = 16;
            PowerUpWidth = 16;

            Coin = game.Content.Load<Texture2D>("Coins");
            SingleCoin = game.Content.Load<Texture2D>("SingleCoin");
            CoinRows = 3;
            CoinColumns = 3;

            Koopa = game.Content.Load<Texture2D>("Koopas");
            FlyingKoopaStartFrame = 0;
            FlyingKoopaFrames = 2;
            WalkingKoopaStartFrame = 2;
            WalkingKoopaFrames = 2;
            HidingKoopaFrame = 4;
            KoopaShell = 5;
            KoopaWidth = 16;
            
            Fireball = game.Content.Load<Texture2D>("Fireballs");
            FireballFrames = 4;

            Blocks = game.Content.Load<Texture2D>("Blocks");
            DestroyedBlock = game.Content.Load<Texture2D>("BrokenBlockPieces");
            QuestionBlock = new ArrayList {0,6};
            QuestionBlockFrames = 3;
            UsedBlock = new ArrayList() { 0, 1 };
            FloorBlock = new ArrayList() { 0, 4 };
            BrickBlock = new ArrayList() { 0, 3 };
            HiddenBlock = new ArrayList() { 0, 0 };
            StairBlock = new ArrayList() {0, 5};
            BlockHeight = 16;
            BlockWidth = 16;
            BrokenBlockHeight = 8;
            BrokenBlockWidth = 8;

            UpPipes = game.Content.Load<Texture2D>("UpPipes");
            SidePipes = game.Content.Load<Texture2D>("SidePipes");
            UpPipeTotalFrames = 2;

            SidePipeTotalFrames = 4;
            PipeTopFrame = 0;
            PipeBodyFrame = 1;
            PipeMergeLeftFrame = 2;
            PipeMergeRightFrame = 3;

            Pole = game.Content.Load<Texture2D>("FlagPoles");
            Flag = game.Content.Load<Texture2D>("Flags");
            DistanceFromTopOfPoleToFlag = 8;

            Lakitu = game.Content.Load<Texture2D>("FlyingGuy");
            LakituWidth = 16;
            LakituStartFrame = 0;
            LakituFrames = 2;

            SpinyEgg = game.Content.Load<Texture2D>("SpikeShell");
            SpinyEggWidth = 16;
            SpinyEggStartFrame = 0;
            SpinyEggFrames = 2;

            Spiny = game.Content.Load<Texture2D>("SpikeThing");
            SpinyWidth = 16;
            SpinyStartFrame = 0;
            SpinyFrames = 2;

            WhiteBackground = game.Content.Load<Texture2D>("BackgroundWhite");
            DialogBox = game.Content.Load<Texture2D>("DialogBox");
            ImageBox = game.Content.Load<Texture2D>("ImageBox");
            EmptyTexture = game.Content.Load<Texture2D>("EmptyTexture");
        }
    }
}
