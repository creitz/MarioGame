
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sprint0Game
{
    public class EnemySpawner : ISpawner
    {
        private Vector2 CurrentPosition;
        private ILevel Level;
        private int SpawnFrequency;
        private int EnemiesPerSpawn;
        private int UpdateCounter;
        private int NumberOfEnemiesSpawned;
        private int UpdatesSinceLastSpawn;
        private Random RandomNumber;

        public EnemySpawner(Vector2 position, ILevel level, int spawnFrequency)
        {
            this.CurrentPosition = position;
            this.Level = level;
            this.SpawnFrequency = spawnFrequency*EnemySpawnerConfig.SpawnFrequencyMultiplier;
            this.EnemiesPerSpawn = EnemySpawnerConfig.StartingEnemiesPerSpawn;
            this.RandomNumber = new Random();
        }

        public void Update()
        {
            this.UpdateCounter++;
            if (this.UpdateCounter > this.SpawnFrequency)
            {
                SpawnEnemies();
            }
        }

        private void SpawnEnemies()
        {
            this.UpdatesSinceLastSpawn++;
            if (this.NumberOfEnemiesSpawned < this.EnemiesPerSpawn && this.UpdatesSinceLastSpawn > EnemySpawnerConfig.UpdatesBetweenSpawns)
            {
                this.UpdatesSinceLastSpawn = 0;
                IEnemy spawnEnemy = CreateSpawnEnemy();
                this.Level.Enemies.Add(spawnEnemy);
                this.NumberOfEnemiesSpawned++;
            }
            else if (this.NumberOfEnemiesSpawned >= this.EnemiesPerSpawn)
            {
                this.NumberOfEnemiesSpawned = 0;
                this.UpdateCounter = 0;
                if (this.EnemiesPerSpawn < EnemySpawnerConfig.MaxEnemiesPerSpawn)
                    this.EnemiesPerSpawn++;
            }
        }

        private IEnemy CreateSpawnEnemy()
        {
            IEnemy spawnEnemy=new Goomba(this.CurrentPosition,true);
            EnemyDescriptor randomPowerup = (EnemyDescriptor)this.RandomNumber.Next(0, EnemySpawnerConfig.NumberOfUniqueEnemies);
            bool rightFacing = this.RandomNumber.Next(0, 2) == 1;
            switch (randomPowerup)
            {
                case EnemyDescriptor.Goomba:
                    spawnEnemy = new Goomba(this.CurrentPosition, rightFacing);
                    break;
                case EnemyDescriptor.Koopa:
                    spawnEnemy = new Koopa(this.CurrentPosition, rightFacing);
                    break;
            }
            return spawnEnemy;
        }

       
    }
}
