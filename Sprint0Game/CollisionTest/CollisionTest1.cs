using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sprint0Game;


namespace Sprint0Game
{
    [TestClass]
    public class CollisionTest1
    {
        Game game = new Game();
        ILevel level;
        StreamWriter writer = new StreamWriter("TestResults.txt");

        //Test with Mario and Block Object
        //Test from the Left side
        [TestMethod]
        public void MarioLeftSideBlockTest()
        {
            //Make Mario, Block, and Movement Command
            IMario mario = new Mario(new Vector2(250,500));
            IObject block = new Block(new Vector2(500,500));
            level = new Level(game);
            level.Mario = mario;
            ICommand moveRight = new RightCommand(level); 

            //Move Mario Right until he has collided with the Block's space
            Rectangle marioPosition = mario.DrawnRectangle;
            while ((marioPosition.X + mario.DrawnRectangle.Width) < 500)
            {
                moveRight.Execute();
                level.Update();
                marioPosition = mario.DrawnRectangle;
            }

            //Check test results, Mario should stop
            if ((marioPosition.X + mario.DrawnRectangle.Width) > 500)
            {
                writer.WriteLine("Mario Left Side Block Test: Unsuccessful\n");
            }

            else
            {
                writer.WriteLine("Mario Left Side Block Test: Successful\n");
            }
        }

        //Test from the Right side
        [TestMethod]
        public void MarioRightSideBlockTest()
        {
            //Make Mario, Block, and Movement Command
            IMario mario = new Mario(new Vector2(750, 500));
            IObject block = new Block(new Vector2(500, 500));
            level = new Level(game);
            level.Mario = mario;
            ICommand moveLeft = new LeftCommand(level);

            //Move Mario Left until he has collided with the Block's space
            Rectangle marioPosition = mario.DrawnRectangle;
            while ((marioPosition.X - block.DrawnRectangle.X) > 500)
            {
                moveLeft.Execute();
                level.Update();
                marioPosition = mario.DrawnRectangle;
            }

            //Check test results, Mario should stop
            if ((marioPosition.X - block.DrawnRectangle.X) < 500)
            {
                writer.WriteLine("Mario Right Side Block Test: Unsuccessful\n");
            }

            else
            {
                writer.WriteLine("Mario Right Side Block Test: Successful\n");
            }
        }

        //Test from Above
        [TestMethod]
        public void MarioAboveBlockTest()
        {
            //Make Mario, Block, and Movement Command
            IMario mario = new Mario(new Vector2(500, 250));
            IObject block = new Block(new Vector2(500, 500));
            level = new Level(game);
            level.Mario = mario;
            ICommand moveDown = new DownCommand(level);

            //Move Mario Down until he has collided with the Block's space
            Rectangle marioPosition = mario.DrawnRectangle;
            while ((marioPosition.Y + mario.DrawnRectangle.Height) < 500)
            {
                moveDown.Execute();
                level.Update();
                marioPosition = mario.DrawnRectangle;
            }

            //Check test results, Mario should stop
            if ((marioPosition.Y + mario.DrawnRectangle.Height) > 500)
            {
                writer.WriteLine("Mario Above Block Test: Unsuccessful\n");
            }

            else
            {
                writer.WriteLine("Mario Above Block Test: Successful\n");
            }
        }

        //Test from Below
        [TestMethod]
        public void MarioBelowBlockTest()
        {
            //Make Mario, Block, and Movement Command
            IMario mario = new Mario(new Vector2(500, 750));
            IObject block = new Block(new Vector2(500, 500));
            level = new Level(game);
            level.Mario = mario;
            ICommand moveUp = new UpCommand(level);

            //Move Mario Up until he has collided with the Block's space
            Rectangle marioPosition = mario.DrawnRectangle;
            while ((marioPosition.Y - block.DrawnRectangle.Y) > 500)
            {
                moveUp.Execute();
                level.Update();
                marioPosition = mario.DrawnRectangle;
            }

            //Check test results, Mario should stop
            if ((marioPosition.Y - block.DrawnRectangle.Y) < 500)
            {
                writer.WriteLine("Mario Below Block Test: Unsuccessful\n");
            }

            else
            {
                writer.WriteLine("Mario Below Block Test: Successful\n");
            }
        }



        //Test with Mario and Goomba Enemy
        //Test from the Left Side
        [TestMethod]
        public void MarioLeftSideGoombaTest()
        {
            //Make Mario, Goomba, and Movement Command
            IMario mario = new Mario(new Vector2(250, 500));
            //IEnemies goomba = new Goomba(new Vector2(500, 500));
            level = new Level(game);
            level.Mario = mario;
            ICommand moveRight = new RightCommand(level);

            //Move Mario Right until he has run past the Goomba
            Rectangle marioPosition = mario.DrawnRectangle;
            while (marioPosition.X < 510)
            {
                moveRight.Execute();
                level.Update();
                marioPosition = mario.DrawnRectangle;
            }

            //Check test results, Mario should die
            if (!mario.IsDead())
            {
                writer.WriteLine("Mario Left Side Goomba Test: Unsuccessful\n");
            }

            else
            {
                writer.WriteLine("Mario Left Side Goomba Test: Successful\n");
            }
        }

        //Test from the Right Side
        [TestMethod]
        public void MarioRightSideGoombaTest()
        {
            //Make Mario, Goomba, and Movement Command
            IMario mario = new Mario(new Vector2(750, 500));
            IEnemy goomba = new Goomba(new Vector2(500, 500));
            level = new Level(game);
            level.Mario = mario;
            ICommand moveLeft = new LeftCommand(level);

            //Move Mario Left until he has run past the Goomba
            Rectangle marioPosition = mario.DrawnRectangle;
            while (marioPosition.X > 490)
            {
                moveLeft.Execute();
                level.Update();
                marioPosition = mario.DrawnRectangle;
            }

            //Check test results, Mario should die
            if (!mario.IsDead())
            {
                writer.WriteLine("Mario Right Side Goomba Test: Unsuccessful\n");
            }

            else
            {
                writer.WriteLine("Mario Right Side Goomba Test: Successful\n");
            }
        }     
       
        //Test from Above
        [TestMethod]
        public void MarioAboveGoombaTest()
        {
            //Make Mario, Goomba, and Movement Command
            IMario mario = new Mario(new Vector2(500, 250));
            IEnemy goomba = new Goomba(new Vector2(500, 500));
            level = new Level(game);
            level.Mario = mario;
            ICommand moveDown = new DownCommand(level);

            //Move Mario Down until he has fallen past the Goomba
            Rectangle marioPosition = mario.DrawnRectangle;
            while (marioPosition.Y < 500 - marioPosition.Height)
            {
                moveDown.Execute();
                level.Update();
                marioPosition = mario.DrawnRectangle;
            }

            //Check test results, Mario should live
            if (mario.IsDead())
            {
                writer.WriteLine("Mario Above Goomba Test: Unsuccessful\n");
            }

            else
            {
                writer.WriteLine("Mario Above Goomba Test: Successful\n");
            }
        }

        //Test from Below
        [TestMethod]
        public void MarioBelowGoombaTest()
        {
            //Make Mario, Goomba, and Movement Command
            IMario mario = new Mario(new Vector2(500, 750));
            IEnemy goomba = new Goomba(new Vector2(500, 500));
            level = new Level(game);
            level.Mario = mario;
            ICommand moveUp = new UpCommand(level);

            //Move Mario Up until he has jumped past the Goomba
            Rectangle marioPosition = mario.DrawnRectangle;
            while (marioPosition.Y > 490)
            {
                moveUp.Execute();
                level.Update();
                marioPosition = mario.DrawnRectangle;
            }

            //Check test results, Mario should die
            if (!mario.IsDead())
            {
                writer.WriteLine("Mario Below Goomba Test: Unsuccessful\n");
            }

            else
            {
                writer.WriteLine("Mario Below Goomba Test: Successful\n");
            }
        }



        //Test with Mario and Koopa Enemy
        //Test from the Left Side
        [TestMethod]
        public void MarioLeftSideKoopaTest()
        {
            //Make Mario, Koopa, and Movement Command
            IMario mario = new Mario(new Vector2(250, 500));
            IEnemy koopa = new Koopa(new Vector2(500, 500));
            level = new Level(game);
            level.Mario = mario;
            ICommand moveRight = new RightCommand(level);

            //Move Mario Right until he has run past the Koopa
            Rectangle marioPosition = mario.DrawnRectangle;
            while (marioPosition.X < 510)
            {
                moveRight.Execute();
                level.Update();
                marioPosition = mario.DrawnRectangle;
            }

            //Check test results, Mario should die
            if (!mario.IsDead())
            {
                writer.WriteLine("Mario Left Side Koopa Test: Unsuccessful\n");
            }

            else
            {
                writer.WriteLine("Mario Left Side Koopa Test: Successful\n");
            }
        }

        //Test from the Right Side
        [TestMethod]
        public void MarioRightSideKoopaTest()
        {
            //Make Mario, Koopa, and Movement Command
            IMario mario = new Mario(new Vector2(750, 500));
            IEnemy koopa = new Koopa(new Vector2(500, 500));
            level = new Level(game);
            level.Mario = mario;
            ICommand moveLeft = new LeftCommand(level);

            //Move Mario Left until he has run past the Koopa
            Rectangle marioPosition = mario.DrawnRectangle;
            while (marioPosition.X > 490)
            {
                moveLeft.Execute();
                level.Update();
                marioPosition = mario.DrawnRectangle;
            }

            //Check test results, Mario should die
            if (!mario.IsDead())
            {
                writer.WriteLine("Mario Right Side Koopa Test: Unsuccessful\n");
            }

            else
            {
                writer.WriteLine("Mario Right Side Koopa Test: Successful\n");
            }
        }

        //Test from Above
        [TestMethod]
        public void MarioAboveKoopaTest()
        {
            //Make Mario, Koopa, and Movement Command
            IMario mario = new Mario(new Vector2(500, 250));
            IEnemy koopa = new Koopa(new Vector2(500, 500));
            level = new Level(game);
            level.Mario = mario;
            ICommand moveDown = new DownCommand(level);

            //Move Mario Down until he has fallen past the Koopa
            Rectangle marioPosition = mario.DrawnRectangle;
            while (marioPosition.Y < 500 - marioPosition.Height)
            {
                moveDown.Execute();
                level.Update();
                marioPosition = mario.DrawnRectangle;
            }

            //Check test results, Mario should live
            if (mario.IsDead())
            {
                writer.WriteLine("Mario Above Koopa Test: Unsuccessful\n");
            }

            else
            {
                writer.WriteLine("Mario Above Koopa Test: Successful\n");
            }
        }

        //Test from Below
        [TestMethod]
        public void MarioBelowKoopaTest()
        {
            //Make Mario, Koopa, and Movement Command
            IMario mario = new Mario(new Vector2(500, 750));
            //IEnemies koopa = new Koopa(new Vector2(500, 500));
            level = new Level(game);
            level.Mario = mario;
            ICommand moveUp = new UpCommand(level);

            //Move Mario Up until he has jumped past the Koopa
            Rectangle marioPosition = mario.DrawnRectangle;
            while (marioPosition.Y > 490)
            {
                moveUp.Execute();
                level.Update();
                marioPosition = mario.DrawnRectangle;
            }

            //Check test results, Mario should die
            if (!mario.IsDead())
            {
                writer.WriteLine("Mario Below Koopa Test: Unsuccessful\n");
            }

            else
            {
                writer.WriteLine("Mario Below Koopa Test: Successful\n");
            }
        }



        //Test with Mario and Mushroom
        [TestMethod]
        public void MarioMushroomTest()
        {
            //Make Mario, Mushroom, and Movement Command
            IMario mario = new Mario(new Vector2(250, 500));
            IItem mushroom = new GreenMushroom(new Vector2(500, 500));
            level = new Level(game);
            level.Mario = mario;
            ICommand moveRight = new RightCommand(level);

            //Move Mario Right until he has collided with the Mushroom
            Rectangle marioPosition = mario.DrawnRectangle;
            while (marioPosition.X < 510)
            {
                moveRight.Execute();
                level.Update();
                marioPosition = mario.DrawnRectangle;
            }

            //Check test results, Mario should be big
            if (!mario.IsBig())
            {
                writer.WriteLine("Mario Mushroom Test: Unsuccessful\n");
            }

            else
            {
                writer.WriteLine("Mario Mushroom Test: Successful\n");
            }
        }



        //Test with Mario and the Fire Flower
        [TestMethod]
        public void MarioFlowerTest()
        {
            //Make Mario, Flower, and Movement Command
            IMario mario = new Mario(new Vector2(250, 500));
            IItem flower = new FireFlower(new Vector2(500, 500));
            level = new Level(game);
            level.Mario = mario;
            ICommand moveRight = new RightCommand(level);

            //Move Mario Right until he has collided with the Flower
            Rectangle marioPosition = mario.DrawnRectangle;
            while (marioPosition.X < 510)
            {
                moveRight.Execute();
                level.Update();
                marioPosition = mario.DrawnRectangle;
            }

            //Check test results, Mario should be Fire type
            if (!mario.IsFire())
            {
                writer.WriteLine("Mario Flower Test: Unsuccessful\n");
            }

            else
            {
                writer.WriteLine("Mario Flower Test: Successful\n");
            }
        }



        //Test with Mario and a Coin
        [TestMethod]
        public void MarioCoinTest()
        {
            //Make Mario, Coin, and Movement Command
            IMario mario = new Mario(new Vector2(250, 500));
            IItem coin = new Coin(new Vector2(500, 500));
            level = new Level(game);
            level.Mario = mario;
            ICommand moveRight = new RightCommand(level);

            //Move Mario Right until he has collided with the Coin
            Vector2 marioPosition = mario.CurrentPosition;
            while (marioPosition.X < 510)
            {
                moveRight.Execute();
                level.Update();
                marioPosition = mario.CurrentPosition;
            }

            //Check test results, no expected change for Mario

            //Output successful test
            writer.WriteLine("Mario Coin Test: Successful\n");
        }



        //Test with Mario and the Koopa Shell
        //Test from the Left Side
        [TestMethod]
        public void MarioLeftSideShellTest()
        {
            //Make Mario, Shell, and Movement Command
            IMario mario = new Mario(new Vector2(250, 500));
            IProjectile shell = new Shell(new Vector2(500, 500));
            level = new Level(game);
            level.Mario = mario;
            ICommand moveRight = new RightCommand(level);

            //Move Mario Right until he has collided with the Shell
            Rectangle marioPosition = mario.DrawnRectangle;
            while (marioPosition.X < 510)
            {
                moveRight.Execute();
                level.Update();
                marioPosition = mario.DrawnRectangle;
            }

            //Check test results, Mario should die
            if (!mario.IsDead())
            {
                writer.WriteLine("Mario Left Side Shell Test: Unsuccessful\n");
            }

            else
            {
                writer.WriteLine("Mario Left Side Shell Test: Successful\n");
            }
        }

        //Test from the Right Side
        [TestMethod]
        public void MarioRightSideShellTest()
        {
            //Make Mario, Shell, and Movement Command
            IMario mario = new Mario(new Vector2(750, 500));
            IProjectile shell = new Shell(new Vector2(500, 500));
            level = new Level(game);
            level.Mario = mario;
            ICommand moveLeft = new LeftCommand(level);

            //Move Mario Left until he has collided with the Shell
            Rectangle marioPosition = mario.DrawnRectangle;
            while (marioPosition.X > 490)
            {
                moveLeft.Execute();
                level.Update();
                marioPosition = mario.DrawnRectangle;
            }

            //Check test results, Mario should die
            if (!mario.IsDead())
            {
                writer.WriteLine("Mario Right Side Shell Test: Unsuccessful\n");
            }

            else
            {
                writer.WriteLine("Mario Right Side Shell Test: Successful\n");
            }
        }

        //Test from Above
        [TestMethod]
        public void MarioAboveShellTest()
        {
            //Make Mario, Shell, and Movement Command
            IMario mario = new Mario(new Vector2(500, 250));
            IProjectile shell = new Shell(new Vector2(500, 500));
            level = new Level(game);
            level.Mario = mario;
            ICommand moveDown = new DownCommand(level);

            //Move Mario Down until he has collided with the Shell
            Rectangle marioPosition = mario.DrawnRectangle;
            while (marioPosition.Y < 510)
            {
                moveDown.Execute();
                level.Update();
                marioPosition = mario.DrawnRectangle;
            }

            //Check test results, Mario should live
            if (mario.IsDead())
            {
                writer.WriteLine("Mario Above Shell Test: Unsuccessful\n");
            }

            else
            {
                writer.WriteLine("Mario Above Shell Test: Successful\n");
            }
        }

        //Test from Below
        [TestMethod]
        public void MarioBelowShellTest()
        {
            //Make Mario, Shell, and Movement Command
            IMario mario = new Mario(new Vector2(500, 750));
            IProjectile shell = new Shell(new Vector2(500, 500));
            level = new Level(game);
            level.Mario = mario;
            ICommand moveUp = new UpCommand(level);

            //Move Mario Up until he has collided with the Shell
            Rectangle marioPosition = mario.DrawnRectangle;
            while (marioPosition.Y > 490)
            {
                moveUp.Execute();
                level.Update();
                marioPosition = mario.DrawnRectangle;
            }

            //Check test results, Mario should die
            if (!mario.IsDead())
            {
                writer.WriteLine("Mario Below Shell Test: Unsuccessful\n");
            }

            else
            {
                writer.WriteLine("Mario Below Shell Test: Successful\n");
            }
        }
    }
}
