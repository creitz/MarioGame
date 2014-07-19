using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using System.Collections;

namespace Sprint0Game
{
    public class LevelUpdater
    {
        public ArrayList Enemies { get; private set; } 
        public ArrayList Items  { get; private set; } 
        public ArrayList Projectiles  { get; private set; } 
        public ArrayList Blocks  { get; private set; } 
        public ArrayList Pipes  { get; private set; }
        public ArrayList Flags { get; private set; }
        public ArrayList Spawners { get; private set; }
        public bool IsPaused
        {
            get { return this.Paused; }
            set
            {
                if (this.PauseCommandTimer == 0)
                {
                    this.Paused = value;
                    this.PauseCommandTimer = Level1Config.PauseWaitTimer;
                }
            }
        }
        private ILevel Level;
        private int MarioTransitionFromItemTimer;
        private int PauseCommandTimer = 0;
        private bool Paused;
        private ArrayList Controllers;

        public LevelUpdater(ILevel level, Game game)
        {
            this.Level = level;
            this.Controllers = new ArrayList();
            this.Controllers.Add(new KeyboardController(game, level));
            this.Controllers.Add(new GamePadController(game));
        }

        public void LoadContent()
        {
            this.Projectiles = new ArrayList();
            this.Items = new ArrayList();
            this.Blocks = new ArrayList();
            this.Enemies = new ArrayList();
            this.Pipes = new ArrayList();
            this.Flags = new ArrayList();
            this.Spawners = new ArrayList();
        }

        private bool ShouldResetLevel()
        {
            if (this.Level.Mario.IsDead())
                return true;
            else if (this.Level.Mario.CurrentPosition.Y > (this.Level.Window.Bounds.Height + Level1Config.MarioFallingDeathYThreshold))
            {
                this.Level.Mario.CurrentState = new DeadMarioState();
                GameStats.Lives--;
                return true;
            }
            return false;
        }

        public void WinLevel(Flag flag)
        {
            this.Level.WinLevelAnimator = new WinLevelAnimator(this.Level, flag);
        }

        private void UpdateFrozen()
        {
            if (this.Level.Mario.IsTransitioningFromItem)
            {
                this.Level.Mario.IsTransitioningFromItem = false;
                this.MarioTransitionFromItemTimer = MarioConfig.TransitioningFromItemDuration;
                this.Level.IsFrozen = true;
            }
            if (this.MarioTransitionFromItemTimer > 0)
            {
                this.MarioTransitionFromItemTimer--;
                this.Level.Mario.Flash(this.MarioTransitionFromItemTimer);
            }
            if (this.MarioTransitionFromItemTimer > 0)
                this.Level.IsFrozen = true;
            else
                this.Level.IsFrozen = false;
        }

        public void UpdatePauseCommandTimer()
        {
            this.PauseCommandTimer--;
            if (this.PauseCommandTimer < 0)
                this.PauseCommandTimer = 0;
        }

        private void UpdateLevelTime()
        {
            if(!(this.Level is HordeLevel))
            this.Level.Time -= 0.015;
        }

        public void Update()
        {
            UpdatePauseCommandTimer();
            UpdateFrozen();
            if(this.Level.FlagReached)
                this.Level.WinLevelAnimator.Update();
            
            if (this.Level.Mario.IsDying())
            {
                this.Level.Mario.Update();
            }
            else if (!(this.Paused || this.Level.IsFrozen))
            {
                UpdateLevelTime();
                this.Level.Camera.Update();

                UpdateItems();
                UpdateEnemies();
                UpdateProjectiles();
                UpdateBlocks();
                UpdatePipes();
                UpdateFlags();
                UpdateSpawners();
                UpdateIfNecessary(this.Level.Mario);

                ArrayList marioList = new ArrayList(); //temp mario array list so only arrays are added to dynamicObjLists
                if (!this.Level.Mario.IsDead())
                    marioList.Add(this.Level.Mario);

                ArrayList dynamicObjLists = new ArrayList();
                dynamicObjLists.Add(marioList);
                dynamicObjLists.Add(this.Level.Items);
                dynamicObjLists.Add(this.Level.Enemies);
                dynamicObjLists.Add(this.Level.Projectiles);

                ArrayList staticObjLists = new ArrayList();
                staticObjLists.Add(CollideBlocksList());
                staticObjLists.Add(this.Level.Pipes);
                staticObjLists.Add(this.Level.Flags);

                CollisionDetector.DetectCollisions(dynamicObjLists, staticObjLists);
            }

            foreach (IController controller in this.Controllers)
            {
                controller.Update();
            }

            if (this.ShouldResetLevel())
                this.Level.Reset();
        }

        private ArrayList CollideBlocksList()
        {
            ArrayList list = new ArrayList();
            foreach (Block block in this.Blocks)
            {
                if (block.ShouldCheckCollisions && block.HasBeenReached)
                    list.Add(block);
            }
            return list;
        }

        private void RemoveRemovedFromList(ArrayList arr)
        {
            ArrayList toBeRemoved = new ArrayList();
            ArrayList newProjectiles = new ArrayList();
            ArrayList newEnemies = new ArrayList();

            foreach (IObject obj in arr)
            {
                if (obj.ShouldBeRemoved)
                {
                    if (((obj as Koopa) != null) && (obj as Koopa).WillBecomeShell)
                    {
                        newProjectiles.Add(new Shell(obj.CurrentPosition));
                    }
                    else if (((obj as FlyingKoopa) != null) && (obj as FlyingKoopa).WillBecomeKoopa)
                    {
                        newEnemies.Add(new Koopa(obj.CurrentPosition, (obj as FlyingKoopa).IsRightFacing));
                    }
                    else if (((obj as SpinyEgg) != null) && (obj as SpinyEgg).WillBecomeSpiny)
                    {
                        newEnemies.Add(new Spiny(obj.CurrentPosition, true));
                    }
                    toBeRemoved.Add(obj);
                }
            }

            foreach (IObject obj in toBeRemoved)
            {
                arr.Remove(obj);
            }

            foreach (IProjectile proj in newProjectiles)
            {
                this.Level.Projectiles.Add(proj);
            }

            foreach (IEnemy enemy in newEnemies)
            {
                this.Level.Enemies.Add(enemy);
            }

        }

        private bool HasBeenReached(IObject obj)
        {
            return obj.CurrentPosition.X < this.Level.Camera.LargestAchievedXPosition + this.Level.Window.Bounds.Width;
        }

        private void UpdateIfNecessary(IObject obj)
        {
            obj.HasBeenReached = HasBeenReached(obj);
            if (obj.HasBeenReached)
            {
                obj.Update();
                if (obj.CurrentVelocity.Y > PhysicsConfig.TerminalVelocity)
                    obj.CurrentVelocity = new Vector2(obj.CurrentVelocity.X, PhysicsConfig.TerminalVelocity);
            }
        }
   
        private void UpdateEnemies()
        {
            RemoveRemovedFromList(this.Enemies);
            foreach (IEnemy enemy in this.Level.Enemies)
            {
                UpdateIfNecessary(enemy);
            }
        }

        private void UpdateItems()
        {
            RemoveRemovedFromList(this.Items);
            foreach (IItem item in this.Level.Items)
            {
                UpdateIfNecessary(item);
            }
        }

        private void UpdateProjectiles()
        {
            RemoveRemovedFromList(this.Projectiles);
            foreach (IProjectile projectile in this.Level.Projectiles)
            {
                UpdateIfNecessary(projectile);
            }
        }

        private void UpdateBlocks()
        {
            RemoveRemovedFromList(this.Blocks);
            foreach (Block block in this.Level.Blocks)
            {
                UpdateIfNecessary(block);
            }
        }

        private void UpdatePipes()
        {
            RemoveRemovedFromList(this.Pipes);
            foreach (IPipe pipe in this.Level.Pipes)
            {
                UpdateIfNecessary(pipe);
            }
        }

        private void UpdateFlags()
        {
            foreach (Flag flag in this.Flags)
            {
                UpdateIfNecessary(flag);
                if (flag.LevelComplete){
                    this.WinLevel(flag);
                    this.Level.FlagReached=true;
                    }
            }
        }
        private void UpdateSpawners()
        {
            foreach (ISpawner spawner in this.Spawners)
            {
                spawner.Update();
            }
        } 

        private IItem ChooseSpawnedItem(Block block)
        {
            Vector2 position = new Vector2(block.CurrentPosition.X, block.CurrentPosition.Y - block.Height);
            if (block.BlockItem == ItemDescriptor.Coin)
                return new Coin(position, true);
            else if (block.BlockItem == ItemDescriptor.GreenMushroom)
                return new Mushroom(position, true, MushroomType.Green);
            else if (block.BlockItem == ItemDescriptor.MetalMushroom)
                return new Mushroom(position, true, MushroomType.Metal);
            else if (block.BlockItem == ItemDescriptor.Star)
                return new Star(position, true);
            else if (ShouldSpawnFireFlower(block))
                return new FireFlower(position, true);
            return new Mushroom(position, true, MushroomType.Red);
        }

        private bool ShouldSpawnFireFlower(Block block)
        {
            return block.BlockItem == ItemDescriptor.RedMushroom && 
                (this.Level.Mario.IsBig() || this.Level.Mario.IsFire());
        }

        public void SpawnItemFromBlock(Block block)
        {
            this.Level.Items.Add(ChooseSpawnedItem(block));
        }
    }
}