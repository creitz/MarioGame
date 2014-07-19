using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Sprint0Game
{
    public interface ILevel : IDisposable
    {
        IMario Mario { get; set; }
        ArrayList Blocks { get; }
        ArrayList Enemies { get; }
        ArrayList Items { get; }
        ArrayList Projectiles { get; }
        ArrayList Pipes { get; }
        ArrayList Flags { get; }
        ArrayList Spawners { get; }
        Castle Castle { get; set; }
        Viewport Window { get; }
        Background Background { get; }
        ICamera Camera { get; }
        bool IsPaused { get; set; }
        bool IsFrozen { get; set; }
        Vector2 TempMarioPos { get; set; }
        Vector2 TempCameraPos { get; set; }
        bool HasCastle { get; set; }
        Dictionary<Vector2, Vector2> UndergroundDict { get; }

        void Reset();
        void BeatLevel();
        bool IsStopped();
        void UnloadContent();
        void Update();
        void Draw(SpriteBatch spriteBatch);
        void ShootFireballFromMario();
        void SpawnItemFromBlock(Block block);
        void HandlePipeEntrance();
        void HandlePipeExit();
        float YDistanceFromMario(IObject obj);
        float XDistanceFromMario(IObject obj);

        WinLevelAnimator WinLevelAnimator { get; set; }
        bool FlagReached { get; set; }
        double Time { get; set; }
    }
}
