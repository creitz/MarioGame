using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint0Game
{
    public static class CreatableHolder
    {
        public static ArrayList Creatables { get; private set; }
        public static ArrayList Mario { get; private set; }
        public static ArrayList Enemy { get; private set; }
        public static ArrayList Block { get; private set; }
        public static ArrayList Pipe { get; private set; }
        public static ArrayList Item { get; private set; }
        public static ArrayList Misc { get; private set; }

        public static Texture2D Green1UP {get; private set;}
        public static Texture2D Big { get; private set; }
        public static Texture2D Small { get; private set; }
        public static Texture2D Fire { get; private set; }
        public static Texture2D FireFlower { get; private set; }
        public static Texture2D Brick { get; private set; }
        public static Texture2D QuestionWithMush { get; private set; }
        public static Texture2D QuestionWithCoin { get; private set; }
        public static Texture2D QuestionWithStar { get; private set; }
        public static Texture2D QuestionWith1UP { get; private set; }
        public static Texture2D Used { get; private set; }
        public static Texture2D Stair { get; private set; }
        public static Texture2D Hidden { get; private set; }
        public static Texture2D Floor { get; private set; }
        public static Texture2D Koopa { get; private set; }
        public static Texture2D Flying { get; private set; }
        public static Texture2D Goomba { get; private set; }
        public static Texture2D Shell { get; private set; }
        public static Texture2D Castle { get; private set; }
        public static Texture2D PipeMerge { get; private set; }
        public static Texture2D PipeSide {get; private set;}
        public static Texture2D PipeSideTop { get; private set; }
        public static Texture2D PipeUp { get; private set; }
        public static Texture2D PipeUpTop { get; private set; }
        public static Texture2D Mushroom { get; private set; }
        public static Texture2D Star { get; private set; }
        public static Texture2D Flagpole { get; private set; }
      

        public static void LoadCreatables(Microsoft.Xna.Framework.Game game)
        {
            Creatables = new ArrayList();
            Mario = new ArrayList();
            Item = new ArrayList();
            Block = new ArrayList();
            Misc = new ArrayList();
            Enemy = new ArrayList();
            Pipe = new ArrayList();

            Green1UP = game.Content.Load<Texture2D>("1UP");
            Small = game.Content.Load<Texture2D>("SmallMarioG");
            Big = game.Content.Load<Texture2D>("BigMarioG");
            Fire = game.Content.Load<Texture2D>("FireMarioG");
            Brick = game.Content.Load<Texture2D>("BrickBlock");
            QuestionWithMush = game.Content.Load<Texture2D>("QuestionMush");
            QuestionWithCoin = game.Content.Load<Texture2D>("QuestionCoin");
            QuestionWith1UP = game.Content.Load<Texture2D>("Question1UP");
            QuestionWithStar = game.Content.Load<Texture2D>("QuestionStar");
            Used = game.Content.Load<Texture2D>("UsedBlock");
            Stair = game.Content.Load<Texture2D>("StairBlock");
            Hidden = game.Content.Load<Texture2D>("Hidden");
            Floor = game.Content.Load<Texture2D>("FloorBlock");
            Koopa = game.Content.Load<Texture2D>("KoopaG");
            Flying = game.Content.Load<Texture2D>("FlyingKoopa");
            Goomba = game.Content.Load<Texture2D>("GoombaG");
            Shell = game.Content.Load<Texture2D>("KoopaShell");
            Castle = game.Content.Load<Texture2D>("CastleScaled");
            PipeMerge = game.Content.Load<Texture2D>("PipeMerge");
            PipeSide = game.Content.Load<Texture2D>("PipeSide");
            PipeSideTop = game.Content.Load<Texture2D>("PipeSideTop");
            PipeUp = game.Content.Load<Texture2D>("PipeUp");
            PipeUpTop = game.Content.Load<Texture2D>("PipeUpTop");
            Mushroom = game.Content.Load<Texture2D>("MushroomG");
            Star = game.Content.Load<Texture2D>("Star");
            FireFlower = game.Content.Load<Texture2D>("FireFlower");
            Flagpole = SpriteHolder.Flag;

            Mario.Add(new Creatable(Small, "MSX"));
            Mario.Add(new Creatable(Big, "MBX"));
            Mario.Add(new Creatable(Fire, "MFX"));

            Item.Add(new Creatable(Mushroom, "IRX"));
            Item.Add(new Creatable(Green1UP, "I1U"));
            Item.Add(new Creatable(Star, "ISX"));
            Item.Add(new Creatable(FireFlower, "IFX"));
            Item.Add(new Creatable(SpriteHolder.SingleCoin, "ICX"));

            Pipe.Add(new Creatable(PipeUp, "RUB"));
            Pipe.Add(new Creatable(PipeUpTop, "RUT"));
            Pipe.Add(new Creatable(PipeSide, "RSB"));
            Pipe.Add(new Creatable(PipeSideTop, "RST"));
            Pipe.Add(new Creatable(PipeMerge, "RSM"));

            Enemy.Add(new Creatable(Goomba, "EGR"));
            Enemy.Add(new Creatable(Koopa, "EKR"));
            Enemy.Add(new Creatable(Flying, "EFR"));

            Misc.Add(new Creatable(Castle, "KXX"));
            Misc.Add(new Creatable(Shell, "PSX"));
            Misc.Add(new Creatable(Flagpole, "FXX"));

            Block.Add(new Creatable(QuestionWith1UP, "BQ1"));
            Block.Add(new Creatable(QuestionWithCoin, "BQC"));
            Block.Add(new Creatable(QuestionWithMush, "BQM"));
            Block.Add(new Creatable(QuestionWithStar, "BQS"));
            Block.Add(new Creatable(Brick, "BBX"));
            Block.Add(new Creatable(Floor, "BFX"));
            Block.Add(new Creatable(Stair, "BSX"));
            Block.Add(new Creatable(Hidden, "BHX"));
            Block.Add(new Creatable(Used, "BUX"));

            Creatables.Add(Mario);
            Creatables.Add(Block);
            Creatables.Add(Item);
            Creatables.Add(Enemy);
            Creatables.Add(Pipe);
            Creatables.Add(Misc);

           
        
        }

        

        
    }
}
