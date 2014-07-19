using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Sprint0Game
{
    public class LevelCreator
    {
        //Values to conform rows and columns to actual grid height and width
        private int width = Level1Config.GridWidth;
        private int height = Level1Config.GridHeight;
        private ILevel level;
        private int screenHeight;
        private ArrayList underGroundPipeLocations;
        private ArrayList aboveGroundPipeLocations;
        Dictionary<int, String[]> levelMap;        

        public LevelCreator(ILevel level)
        {
            this.level = level;
            this.screenHeight = level.Window.Height;
            this.levelMap = new Dictionary<int, String[]>();
            this.underGroundPipeLocations = new ArrayList();
            this.aboveGroundPipeLocations = new ArrayList();
        }

        public void GenerateLevel(string path)
        {
            try
            {
                using (StreamReader readFile = new StreamReader(path))
                {
                    string line;
                    
                    string[] row;
                    int count = 1;

                    while (!readFile.EndOfStream)
                    {
                        line = readFile.ReadLine();
                        //Check if the line is over
                        if (line != null)
                        {
                            //Split the row up into its individual values
                            row = line.Split(',');
                            levelMap.Add(count, row);
                            count++;
                        }
                    }
                    CreateObjects(levelMap);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Error: {0}", e.ToString());
            }
            for (int i = 0; i < this.underGroundPipeLocations.Count; i++)
            {
                this.level.UndergroundDict.Add((Vector2)this.aboveGroundPipeLocations[i],(Vector2)this.underGroundPipeLocations[i]);
            }
        }

        private void CreateObjects(Dictionary<int, String[]> m)
        {
            int heightCounter = 0;
            for (int i = m.Count; i > 0; i--)
            {
                int count = 0;
                string[] row = levelMap[i];
                foreach (string s in row)
                {

                    Vector2 position = new Vector2(count * width, screenHeight - (heightCounter * height));
                    if (s.Length > 0)
                    {
                        switch (s[0])
                        {
                            //Mario
                            case 'M':
                                SpawnMario(position, s);
                                break;
                            //Item
                            case 'I':
                                SpawnItem(position, s);
                                break;
                            //Enemy
                            case 'E':
                                SpawnEnemy(position, s);
                                break;
                            //Projectile
                            case 'P':
                                SpawnProjectile(position, s);
                                break;
                            //Block
                            case 'B':
                                SpawnBlock(position, s);
                                break;
                            //Pipe Normal
                            case 'R':
                                SpawnPipe(position, s, false);
                                break;
                            // Pipe Transitional
                            case 'T':
                                SpawnPipe(position, s, true);
                                break;
                            //Flag
                            case 'F':
                                SpawnFlag(position, s);
                                break;
                            //Camera
                            case 'L':
                                this.level.Camera.MinXPosition = position.X;
                                break;
                            //Castle
                            case 'K':
                                this.level.Castle = new Castle(position);
                                this.level.HasCastle = true;
                                break;
                            //Transitional Locations
                            case 'Z':
                                MakeTransLocation(s,position);
                                break;
                            //Transitional Locations
                            case 'S':
                                MakeSpawner(position,s);
                                break;
                            case 'J':
                                this.level.Camera.MaxXPosition = position.X;
                                break;
                        }
                        count++;
                    }
                }
                heightCounter++;
            }
        }

        public void SpawnMario(Vector2 Position, String s)
        {
            IMario m = new Mario(Position, this.level);
            switch (s[1])
            {
                case 'S':
                    m.CurrentState = new SmallIdleRightFacingMarioState(m);
                    this.level.Mario = m;
                    break;
                case 'B':
                    m.CurrentState = new BigIdleRightFacingMarioState(m);
                    this.level.Mario = m;
                    break;
                case 'F':
                    m.CurrentState = new FireIdleRightFacingMarioState(m);
                    this.level.Mario = m;
                    break;
            }
        }

        public void SpawnItem(Vector2 Position, String s)
        {
            IItem i;
            switch (s[1])
            {
                case 'C':
                    i = new Coin(Position, false);
                    this.level.Items.Add(i);
                    break;
                case 'S':
                    i = new Star(Position, true);
                    this.level.Items.Add(i);
                    break;
                case 'R':
                    i = new Mushroom(Position, true, MushroomType.Red);
                    this.level.Items.Add(i);
                    break;
                case 'M':
                    i = new Mushroom(Position, true, MushroomType.Metal);
                    this.level.Items.Add(i);
                    break;
                case '1':
                    i = new Mushroom(Position, true, MushroomType.Green);
                    this.level.Items.Add(i);
                    break;
                case 'F':
                    i = new FireFlower(Position, true);
                    this.level.Items.Add(i);
                    break;
            }
            
                
        }

        public void SpawnEnemy(Vector2 Position, String s)
        {
            IEnemy e;
            switch (s[1])
            {
                case 'G':
                    if (s.Length > 2 && s[2] == 'R')
                        e = new Goomba(Position, true);
                    else
                        e = new Goomba(Position, false);
                    this.level.Enemies.Add(e);
                    break;
                case 'K':
                    if (s.Length > 2 && s[2] == 'R')
                        e = new Koopa(Position, true);
                    else
                        e = new Koopa(Position, false);
                    this.level.Enemies.Add(e);
                    break;
                case 'F':
                    if (s.Length > 2 && s[2] == 'R')
                        e = new FlyingKoopa(Position, true);
                    else
                        e = new FlyingKoopa(Position, false);
                    this.level.Enemies.Add(e);
                    break;
                case 'S':
                    if (s.Length > 2 && s[2] == 'R')
                        e = new Spiny(Position, true);
                    else
                        e = new Spiny(Position, false);
                    this.level.Enemies.Add(e);
                    break;
                case 'L':
                    if (s.Length > 2 && s[2] == 'R')
                        e = new Lakitu(Position, this.level);
                    else
                        e = new Lakitu(Position, this.level);
                    this.level.Enemies.Add(e);
                    break;
            }
        }

        public void SpawnProjectile(Vector2 Position, String s)
        {
            IProjectile p;
            switch (s[1])
            {
                case 'S':
                    p = new Shell(Position);
                    this.level.Projectiles.Add(p);
                    break;
                case 'F':
                    p = new Fireball(Position, true);
                    this.level.Projectiles.Add(p);
                    break;
            }
        }

        public void SpawnBlock(Vector2 Position, String s)
        {
            Block b;
            char noCollCheck = 'U';
            switch (s[1])
            {
                case 'Q':
                    b = new Block(Position, BlockStateDescriptor.Question, PickItem(s), this.level, true);
                    this.level.Blocks.Add(b);
                    break;
                case 'H':
                    b = new Block(Position, BlockStateDescriptor.Hidden, PickItem(s), this.level, true);
                    this.level.Blocks.Add(b);
                    break;
                case 'B':
                    b = new Block(Position, BlockStateDescriptor.Brick, PickItem(s), this.level, true);
                    this.level.Blocks.Add(b);
                    break;
                case 'S':
                    b = new Block(Position, BlockStateDescriptor.Stair, PickItem(s), this.level, s[2] != noCollCheck);
                    this.level.Blocks.Add(b);
                    break;
                case 'F':
                    b = new Block(Position, BlockStateDescriptor.Floor, PickItem(s), this.level, s[2] != noCollCheck);
                    this.level.Blocks.Add(b);
                    break;
                case 'U':
                    b = new Block(Position, BlockStateDescriptor.Used, PickItem(s), this.level, true);
                    this.level.Blocks.Add(b);
                    break;
            }
        }

        public void SpawnPipe(Vector2 Position, String s, bool isTransitional)
        {
            IPipe p;
            switch (s[1])
            {
                case 'U':
                    if(s[2] == 'B')
                        p = new PipeBody(Position, isTransitional, false);
                    else
                        p = new PipeTop(Position, isTransitional, false);
                    this.level.Pipes.Add(p);
                    break;
                case 'S':
                    if (s[2] == 'B')
                        p = new PipeBody(Position, isTransitional, true);
                    else if (s[2] == 'T')
                        p = new PipeTop(Position, isTransitional, true);
                    else if (s[2] == 'M')
                        p = new PipeMerge(Position, isTransitional, true);
                    else
                        p = new PipeMerge(Position, isTransitional, false);
                    this.level.Pipes.Add(p);
                    break;
            }
        }

        public void SpawnFlag(Vector2 Position, String s)
        {
            Flag flag = new Flag(Position);
            this.level.Flags.Add(flag);
            this.level.Blocks.Add(new Block(Position, BlockStateDescriptor.Stair, PickItem(s), this.level, true));
        }

        public static ItemDescriptor PickItem(String s)
        {
            if(s.Length>2){
                switch (s[2])
                {
                    case 'C':
                        return ItemDescriptor.Coin;
                    case 'P':
                        return ItemDescriptor.RedMushroom;
                    case 'G':
                        return ItemDescriptor.GreenMushroom;
                    case 'S':
                        return ItemDescriptor.Star;
                    case 'M':
                        return ItemDescriptor.MetalMushroom;
                }
            }
            return ItemDescriptor.None;
        }

        public void MakeTransLocation(String s, Vector2 position)
        {
            switch (s[1])
            {
                case 'U':
                    this.underGroundPipeLocations.Add(position);
                    break;
                case 'A': 
                    this.aboveGroundPipeLocations.Add(position);
                    break;
            }
        }
        public void MakeSpawner(Vector2 Position, String s)
        {
            int spawnFreq = (int)Char.GetNumericValue(s[2]);
            switch (s[1])
            {
                case 'P':
                    ISpawner powerupSpawner = new PowerupSpawner(Position, this.level, spawnFreq);
                    this.level.Spawners.Add(powerupSpawner);
                    break;
                case 'E':
                    ISpawner enemySpawner = new EnemySpawner(Position, this.level, spawnFreq);
                    this.level.Spawners.Add(enemySpawner);
                    break;
            }
        }
    }
}
