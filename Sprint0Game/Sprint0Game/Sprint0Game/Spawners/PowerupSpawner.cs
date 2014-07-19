
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sprint0Game
{
    public class PowerupSpawner : ISpawner
    {
        private Vector2 CurrentPosition;
        private ILevel Level;
        private int SpawnFrequency;
        private int UpdateCounter;
        private Random RandomNumber;

        public PowerupSpawner(Vector2 position, ILevel level, int spawnFrequency)
        {
            this.CurrentPosition = position;
            this.Level = level;
            this.SpawnFrequency = spawnFrequency*PowerupSpawnerConfig.SpawnFrequencyMultiplier;
            this.RandomNumber = new Random();
        }

        public void Update()
        {
            this.UpdateCounter++;
            if (this.UpdateCounter > this.SpawnFrequency)
            {
                this.UpdateCounter = 0;
                SpawnItem();
            }
        }

        private void SpawnItem()
        {
            IItem spawnItem = CreateSpawnItem();
            this.Level.Items.Add(spawnItem);
        }

        private IItem CreateSpawnItem()
        {
            IItem spawnItem = new Mushroom(this.CurrentPosition, true, MushroomType.Green);
            PowerupDescriptor randomPowerup = (PowerupDescriptor)this.RandomNumber.Next(0, Enum.GetNames(typeof(PowerupDescriptor)).Length);
            switch (randomPowerup)
            {
                case PowerupDescriptor.Star:
                    spawnItem = new Star(this.CurrentPosition, true);
                    break;
                case PowerupDescriptor.GreenMushroom:
                    spawnItem = new Mushroom(this.CurrentPosition, true, MushroomType.Green);
                    break;
                case PowerupDescriptor.MetalMushroom:
                    spawnItem = new Mushroom(this.CurrentPosition, true, MushroomType.Metal);
                    break;
                case PowerupDescriptor.RedMushroom:
                    if (this.Level.Mario.IsBig())
                        spawnItem = new Mushroom(this.CurrentPosition, true, MushroomType.Red);
                    else
                        spawnItem = new FireFlower(this.CurrentPosition, true);
                    break;
            }
            return spawnItem;
        }
    }
}
